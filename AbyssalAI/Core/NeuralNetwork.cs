using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.dataWindow;
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
            var output = new FiringNeuron[Options.LayerStructure.Length,Options.MaxLayerDensity];

            for (var layer = 1; layer < Options.LayerStructure.Length; layer++)
            for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++)
                output[layer, neuron] = 
                    new FiringNeuron(new Coordinate(layer, neuron), Options.LayerStructure[layer-1]);

            NeuronLayers = output;
        }

        public INeuralNetworkOptions Options { get; }
        public FiringNeuron[,] NeuronLayers { get; set; }


        public NetworkTrainingResult Train(IDataWindow[] trainingData, out ConcurrentBag<EpochResult> concurrentEpochCollection)
        {
            concurrentEpochCollection = new ConcurrentBag<EpochResult>();

            for (var epoch = 0; epoch < Options.MaxEpochs; epoch++)
            {
                var result = Backpropagate(trainingData);
                result.EpochIndex = epoch;

                concurrentEpochCollection.Add(result);

                //check if value is achived
                if (Options.AccuracyGoal != null && result.Accuracy > Options.AccuracyGoal)
                    break;
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


        private EpochResult Backpropagate(IDataWindow[] trainingData)
        {
            var output = new EpochResult();
            var totalAmountOfData = trainingData.Length;
            var amountOfCorrect = 0;
            
            
            //create adjustment neurons 
            for (var layer = 1; layer < Options.LayerStructure.Length; layer++)
            for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++)
                _proposedNeurons[layer, neuron] =
                    new ProposedNeuron(NeuronLayers[layer, neuron].Weights.Length, trainingData.Length);
                
            // for window
            for (var window = 0; window < trainingData.Length; window++)
            {
                //get activation
                CreateActivationArray(trainingData[window]);
                FillActivationArray();

                var isSuccessful = (
                    Math.Max(_exampleActivations
                        [_exampleActivations.GetLength(0), 0], trainingData[window]
                        .OutputLayer[0])
                    -
                    Math.Min(_exampleActivations
                        [_exampleActivations.GetLength(0), 0], trainingData[window]
                        .OutputLayer[0])
                    < 0.5F);
                if (isSuccessful)
                    amountOfCorrect++;

                //get cost
                ResetExampleCostArray();
                FillExampleCostArray(trainingData[window].OutputLayer);

                //for each neuron
                for (var layer = 1; layer < Options.LayerStructure.Length; layer++)
                for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++)
                {
                    //get adjustments
                    var biasAdjustment = NeuronLayers[layer, neuron]
                        .GetBiasAdjust(Options.LearningRate, _exampleActivations, _exampleCosts[layer, neuron]);

                    var weightAdjustment = NeuronLayers[layer, neuron]
                        .GetWeightAdjust(Options.LearningRate, _exampleActivations, _exampleCosts[layer, neuron]);

                    //set adjustment

                    _proposedNeurons[layer, neuron].AddBiasProposal(biasAdjustment);
                    _proposedNeurons[layer, neuron].AddWeightProposal(weightAdjustment);
                }

            }

            //after for
            //make adjustment

            for (var layer = 1; layer < Options.LayerStructure.Length; layer++)
            for (var neuron = 0; neuron < Options.LayerStructure[layer]; neuron++)
                NeuronLayers[layer, neuron].Adjust(_proposedNeurons[layer, neuron]);

            output.Accuracy = amountOfCorrect / totalAmountOfData;
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
            var firstDimensionSize = Options.LayerStructure.Length-1;
            var secondDimensionSize =
                Options.LayerStructure.OrderBy(l => l)
                    .First();

            _exampleActivations = new float[firstDimensionSize, secondDimensionSize];

            if (data == null || !VerifyDataWindowValidity(data)) return;
            for (var i = 0; i < data.OutputLayer.Length; i++)
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
            for (var layer = 1; layer <= _exampleActivations.GetLength(0); layer++)
            for (var neuron = 0; neuron <= _exampleActivations.GetLength(1); neuron++) {
                _exampleActivations[layer, neuron] = 
                NeuronLayers[layer, neuron].GetActivation(_exampleActivations); // get activation
            }
        }

        private float[,] _exampleCosts;
        private float[,] _exampleActivations;

        private IProposedNeuron[,] _proposedNeurons; //TODO: fix to default

        private void ResetExampleCostArray() =>
            _exampleCosts = new float[Options.LayerStructure.Length,Options.MaxLayerDensity];
        

        private void FillExampleCostArray(float[] expectedOutput)
        {
            //for output layer
            for (var neuron = 0; neuron < Options.LayerStructure[^0]; neuron++)
            {
                var outputLayerIndex = Options.LayerStructure.Length - 1;


                //determine cost and set in array
                _exampleCosts[Options.LayerStructure.Length-1, neuron] =
                (float)Math.Pow(_exampleActivations[outputLayerIndex, neuron] - expectedOutput[neuron], 2);

                //determine next layer cost 
                Func<float, float, float> newSeries = (activation, weight) => activation < 0 ? 0 : weight; //extract

                var costSeries = new float[NeuronLayers[outputLayerIndex, neuron].Weights.Length];
                for (var weight = 0; weight < costSeries.Length; weight++)
                    costSeries[weight] = 
                        newSeries.Invoke(_exampleActivations[outputLayerIndex-1, weight], 
                            NeuronLayers[outputLayerIndex, neuron].Weights[weight]);


                //call RelayCostToNextLayer()
                RelayCostToNextLayer(costSeries, 1);
            }
        }

        private void RelayCostToNextLayer(float[] costSeries, int depth)
        {
            for (var neuron = 0; 0 < Options.LayerStructure[^depth]; neuron++)
            {
                var depthAsIndex = Options.LayerStructure[^depth] - depth; //TODO: check validity

                //add new partial cost to ExampleCosts
                _exampleCosts[Options.LayerStructure.Length - depth, neuron] += costSeries[neuron];

                Func<float, float, float> newSeries = (activation, weight) => activation < 0 ? 0 : weight; //extract

                //check depth
                if (depth > Options.LayerStructure.Length - 1)
                    continue;

                var newCostSeries = new float[Options.LayerStructure[^(depth + 1)]]; //check for 0 index?
                for (var series = 0; series < newCostSeries.Length; series++)
                    newCostSeries[series] *= 
                        newSeries.Invoke(_exampleActivations[depthAsIndex-1, series],
                            NeuronLayers[depthAsIndex, neuron].Weights[series]);

                //recursive call next layer
                RelayCostToNextLayer(newCostSeries, depth + 1);
            }
        }



    }
}
