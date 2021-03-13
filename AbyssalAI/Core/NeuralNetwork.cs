using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AbyssalAI.Core.dataWindow;
using AbyssalAI.Core.helpers;
using AbyssalAI.Core.Interfaces;
using AbyssalAI.Core.models;
using AbyssalAI.Core.Neurons;

namespace AbyssalAI.Core
{
    public class NeuralNetwork: INeuralNetwork
    {
        public NeuralNetwork(INeuralNetworkOptions options = null)
        {
            Options = options ?? new NeuralNetworkOptions();

            GenerateNetwork(); //dependant on options
        }

        private void GenerateNetwork()
        {
            //create neurons
            var output = new FiringNeuron[Options.LayerStructure.Length, Options.MaxLayerDensity];

            for (var layer = 1; layer < Options.LayerStructure.Length; layer++)
            for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++)
                output[layer, neuron] = 
                    new FiringNeuron(new Coordinate(layer, neuron), Options.LayerStructure[layer-1], new SigmoidActivationFunction());

            NeuronLayers = output;
            _proposedNeurons = new IProposedNeuron[Options.LayerStructure.Length, Options.MaxLayerDensity];
        }

        public INeuralNetworkOptions Options { get; }
        public FiringNeuron[,] NeuronLayers { get; set; }


        public NetworkTrainingResult Train(IDataWindow[] trainingData, out ConcurrentBag<EpochResult> concurrentEpochCollection)
        {
            concurrentEpochCollection = new ConcurrentBag<EpochResult>();

            for (var epoch = 0; epoch < Options.MaxEpochs; epoch++)
            {
                //train with data
                var result = Backpropagate(trainingData);
                result.EpochIndex = epoch;

                concurrentEpochCollection.Add(result);

                //check if value is achived
            }

            var trainingResult = new NetworkTrainingResult()
            {
                EpochResults = concurrentEpochCollection.OrderBy(x => x.EpochIndex).ToList()
            };

            return trainingResult;
        }

        public IValidationResult Validate(IDataWindow[] data)
        {
            throw new NotImplementedException();
        }

        public float[] GetActivation(float[] inputLayer)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Validates a DataWindows structure against the structure specified in options
        /// </summary>
        /// <param name="dataWindow">The data window to validate</param>
        /// <returns>Is valid</returns>
        public bool VerifyDataWindowValidity(IDataWindow dataWindow)
        {
            return dataWindow.InputLayer.Length == Options.LayerStructure[0]
                   && dataWindow.OutputLayer.Length == Options.LayerStructure[^1];

        }

        public bool VerifyDataWindowValidity(IDataWindow[] dataWindow)
        {
            return dataWindow.All(VerifyDataWindowValidity);
        }

        
        private EpochResult Backpropagate(IReadOnlyCollection<IDataWindow> trainingData)
        {
            var output = new EpochResult();
            var successes = new List<bool>();
            var costs = new List<float>();
            
            //create adjustment neurons 
            for (var layer = 1; layer < Options.LayerStructure.Length; layer++)
            for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++)
                _proposedNeurons[layer, neuron] =
                    new ProposedNeuron(NeuronLayers[layer, neuron].Weights.Length, trainingData.Count);
            
            // for window
            foreach (var window in trainingData)
            {
                //get activation
                CreateActivationArray(window);
                FillActivationArray();

                //calculate offset
                // var offset = _exampleActivations[_exampleActivations.GetLength(0) - 1, 0] - window.OutputLayer[0];
                // offset *= offset < 0 ? -1 : 1; //if less than 0 times with -1
                // successes.Add(offset < 0.5F);
                
                //get cost
                ResetExampleCostArray();
                FillExampleCostArray(window.OutputLayer);
                costs.Add(_exampleCosts[Options.LayerStructure.Length-1, 0] + _exampleCosts[Options.LayerStructure.Length-1, 1]);

                //for each neuron
                for (var layer = 1; layer < Options.LayerStructure.Length; layer++)
                for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++)
                {
                    //get adjustments
                    var neuronCost = _exampleCosts[layer, neuron];
                    
                    var biasAdjustment = NeuronLayers[layer, neuron]
                        .GetBiasAdjust(_exampleActivations, neuronCost);

                    var weightAdjustment = NeuronLayers[layer, neuron]
                        .GetWeightAdjust(_exampleActivations, neuronCost);

                    //set adjustment

                    _proposedNeurons[layer, neuron].AddBiasProposal(biasAdjustment);
                    _proposedNeurons[layer, neuron].AddWeightProposal(weightAdjustment);
                }
            }

            //after for
            //make adjustment

            for (var layer = 1; layer < Options.LayerStructure.Length; layer++)
            for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++)
                NeuronLayers[layer, neuron].Adjust(_proposedNeurons[layer, neuron], Options.LearningRate);

            var accuracy = (float)successes.Count(x => x) / trainingData.Count;

            //output.AverageOffset = accuracy;
            //output.AverageOffset = _exampleCosts[Options.LayerStructure.Length - 1, 0];
            var totalCost = costs.Sum();
            output.AverageOffset = totalCost / costs.Count;
            return output;
        }

        /// <summary>
        /// Creates an array with dimensions equal to that of the neural network with
        /// pre filled dimensions
        /// </summary>
        /// <param name="data">The data to be used to pre fill the input dimension</param>
        /// <returns>an array the same size as the neural network with pre-filled dimensions</returns>
        private void CreateActivationArray(IDataWindow data)
        {
            var firstDimensionSize = Options.LayerStructure.Length;
            var secondDimensionSize = Options.MaxLayerDensity;

            _exampleActivations = new float[firstDimensionSize, secondDimensionSize];

            if (data == null || !VerifyDataWindowValidity(data)) 
                throw new InvalidDataWindowException(nameof(CreateActivationArray));
            
            for (var i = 0; i < data.InputLayer.Length; i++)
            {
                _exampleActivations[0, i] = data.InputLayer[i];
            }
            //create array by dimensions
            //if data != null 
            //  fill first layer
        }


        /// <summary>
        /// Fills the activation array by calling on the neurons they represent
        /// </summary>
        private void FillActivationArray() {
            //foreach layer after index 0
            //foreach neuron
            for (var layer = 1; layer < _exampleActivations.GetLength(0)-1; layer++)
            for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++) {
                _exampleActivations[layer, neuron] = 
                NeuronLayers[layer, neuron].GetActivation(_exampleActivations); // get activation
            }

            var inputs = new float[Options.LayerStructure[^1]];
            for (var neuron = 0; neuron < Options.LayerStructure[^1]; neuron++)
                inputs[neuron] = NeuronLayers[Options.LayerStructure.Length - 1, neuron].GetInputValue(_exampleActivations);

            var activations = Options.OutputActivationFunction.GetValue(inputs);
            for (var neuron = 0; neuron < Options.LayerStructure[^1]; neuron++)
                _exampleActivations[Options.LayerStructure.Length - 1, neuron] = activations[neuron];
        }

        private float[,] _exampleCosts;
        private float[,] _exampleActivations;

        private IProposedNeuron[,] _proposedNeurons; //TODO: fix to default

        private void ResetExampleCostArray() =>
            _exampleCosts = new float[Options.LayerStructure.Length,Options.MaxLayerDensity];
        

        // private void FillExampleCostArray(float[] expectedOutput)
        // {
        //     //for output layer
        //     for (var neuron = 0; neuron < Options.LayerStructure[^1]; neuron++)
        //     {
        //         var outputLayerIndex = Options.LayerStructure.Length - 1;
        //
        //
        //         //determine cost and set in array
        //         _exampleCosts[Options.LayerStructure.Length-1, neuron] =
        //         (float)Math.Pow(_exampleActivations[outputLayerIndex, neuron] - expectedOutput[neuron], 2);
        //
        //         //determine next layer cost 
        //         Func<float, float, float> newSeries = (activation, weight) => activation < 0 ? 0 : weight; //extract
        //
        //         var costSeries = new float[NeuronLayers[outputLayerIndex, neuron].Weights.Length];
        //         for (var weight = 0; weight < costSeries.Length; weight++)
        //             costSeries[weight] = 
        //                 newSeries.Invoke(_exampleActivations[outputLayerIndex-1, weight], 
        //                     NeuronLayers[outputLayerIndex, neuron].Weights[weight]);
        //
        //
        //         //call RelayCostToNextLayer()
        //         RelayCostToNextLayer(costSeries, 1);
        //     }
        // }

        private void FillExampleCostArray(IReadOnlyList<float> expectedOutput)
        {
            var outputLayerIndex = Options.LayerStructure.Length-1;
            
            for (var layer = outputLayerIndex; layer > 0; layer--) //not input layer
            for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++)
            {
                
                if (layer == outputLayerIndex)
                {
                    var cost = 
                        Options.CostFunction.GetCost(_exampleActivations[layer, neuron], expectedOutput[neuron]);
                    _exampleCosts[outputLayerIndex, neuron] = cost; 
                    continue;
                }

                var totalCurrentNeuronCost = 0F;
                var previousLayerIndex = layer + 1;



                for (var previousLayerNeurons = 0;
                    previousLayerNeurons < Options.LayerStructure[previousLayerIndex];
                    previousLayerNeurons++)
                {
                    //initialize the derivative of the activation function
                    var derivativeOfActivation = 1F;
                    
                    //get the activation of the L + 1 Neuron 
                    var previousNeuronActivation = _exampleActivations[previousLayerIndex, previousLayerNeurons];
                    
                    //get the Activation of the neuron that is expected to be 1
                    var indexOfExpectedTrue = expectedOutput.ToList().FindIndex(x => x.Equals(1F));
                    var activationOfExpectedTrue = _exampleActivations[previousLayerIndex, indexOfExpectedTrue];
                    
                    //Returns the derivative of softmax if final layer else ReLu
                    if (previousLayerIndex == outputLayerIndex)
                    {
                        derivativeOfActivation =
                            Options.OutputActivationFunction.GetDerivedValue(previousNeuronActivation,activationOfExpectedTrue);
                    }
                    else
                    {
                        derivativeOfActivation = Options.ActivationFunction.GetDerivedValue(previousNeuronActivation);
                    }
                    
                    totalCurrentNeuronCost += _exampleCosts[previousLayerIndex, previousLayerNeurons] *
                                              derivativeOfActivation *
                                              NeuronLayers[previousLayerIndex, previousLayerNeurons].Weights[neuron]; 
                }
                    
                    
                
                _exampleCosts[layer, neuron] = totalCurrentNeuronCost;
            }
        }

        // private void RelayCostToNextLayer(float[] costSeries, int depth)
        // {
        //     //return if invalid...
        //     for (var neuron = 0; 0 < Options.LayerStructure[^depth]; neuron++)
        //     {
        //             var layerDepthAsIndex = Options.LayerStructure.Length - 1 - depth;
        //
        //             //add new partial cost to ExampleCosts
        //             _exampleCosts[Options.LayerStructure.Length - depth, neuron] += costSeries[neuron];
        //
        //             Func<float, float, float> newCostSeriesFunction =
        //                 (activation, weight) => activation < 0 ? 0 : weight; //extract
        //
        //             //check depth
        //             if (depth > Options.LayerStructure.Length - 1)
        //                 continue;
        //
        //             var newCostSeries =
        //                 new float[Options.LayerStructure[^(depth + 1)]]; //check for 0 index? //TODO: pretty up code
        //             for (var series = 0; series < newCostSeries.Length; series++)
        //                 newCostSeries[series] *=
        //                     newCostSeriesFunction.Invoke(_exampleActivations[layerDepthAsIndex - 1, series],
        //                         NeuronLayers[layerDepthAsIndex, neuron].Weights[series]);
        //
        //             //recursive call next layer
        //             depth++;
        //             costSeries = newCostSeries;
        //     }
        // }



    }
}
