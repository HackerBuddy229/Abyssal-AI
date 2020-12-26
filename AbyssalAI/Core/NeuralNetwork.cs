using System;
using System.Collections.Generic;
using System.Linq;
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
        }


        public INeuralNetworkOptions Options { get; }
        public FiringNeuron[,] NeuronLayers { get; set; }


        public NetworkTrainingResult Train(IDataWindow[] trainingData)
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
            throw new NotImplementedException("Backpropagate");
            //current cost init null
            //foreach window
            //foreach layer

            //GetLayerCosts()
            //current cost = AdjustLayer

        }

        /// <summary>
        /// Creates an array with dimensions equal to that of the neural network with
        /// pre filled dimensions
        /// </summary>
        /// <param name="data">The data to be used to pre fill the input dimension</param>
        /// <returns>an array the same size as the neural network with pre-filled dimensions</returns>
        private float[,] CreateActivationArray(IDataWindow data)
        {
            var firstDimensionSize = Options.LayerStructure.Length-1;
            var secondDimensionSize =
                Options.LayerStructure.OrderBy(l => l)
                    .First();

            var activationArray = new float[firstDimensionSize, secondDimensionSize];

            if (data == null || !VerifyDataWindowValidity(data)) return null;
            for (var i = 0; i < data.OutputLayer.Length; i++)
            {
                activationArray[0, i] = data.InputLayer[i];
            }

            return activationArray;
            //create array by dimensions
            //if data != null 
            //  fill first layer

        }


        /// <summary>
        /// Fills the activation array by calling on the neurons they represent
        /// </summary>
        /// <param name="initActivationArray">the partailly filled array by example data</param>
        /// <returns>the activation of all neurons in an array</returns>
        private float[,] FillActivationArray(float[,] initActivationArray) { //TODO: add public adapter
            //foreach layer after index 0
            //foreach neuron
            for (var layer = 1; layer <= initActivationArray.GetLength(0); layer++)
            for (var neuron = 0; neuron <= initActivationArray.GetLength(1); neuron++) {
                initActivationArray[layer, neuron] = 
                NeuronLayers[layer, neuron].GetActivation(initActivationArray); // get activation
            }

            return initActivationArray; //return complete array
        }

        private float[,] _exampleCosts;

        private void ResetExampleCostArray(float[,] activationArray)
        {
            throw new NotImplementedException(nameof(ResetExampleCostArray));
        }

        private void FillExampleCostArray(float[] outputActivations)
        {
            throw new NotImplementedException(nameof(FillExampleCostArray));
        }

        private void RelayCostToNextLayer(float[] costSeries, int depth)
        {
            //check depth
            if (depth > Options.LayerStructure.Length - 1)
                return;

            for (var neuron = 0; 0 < Options.LayerStructure[^depth]; neuron++)
            {
                //add new partial cost to ExampleCosts

                //recursive call next layer
            }
        }



    }
}
