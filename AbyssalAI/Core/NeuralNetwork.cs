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
        public float[,] FillActivationArray(float[,] initActivationArray) {
            //foreach layer after index 0
            //foreach neuron
            for (var layer = 1; layer <= initActivationArray.GetLength(0); layer++)
            for (var neuron = 0; neuron <= initActivationArray.GetLength(1); neuron++) {
                initActivationArray[layer, neuron] = 
                NeuronLayers[layer, neuron].GetActivation(initActivationArray); // get activation
            }

            return initActivationArray; //return complete array
        }


        /// <summary>
        /// Gets the cost of a layer.
        /// </summary>
        /// <param name="activationArray">the current activation values(See; CreateActivationArray())</param>
        /// <param name="layer">the layer of which the costs consists</param>
        /// <param name="expectedLayerValues">the final expected layer of the activationArray</param>
        /// <returns>the cost of a layer in a float array</returns>
        private float[] GetLayerCost(float[,] activationArray,
            int layer, 
            float[] expectedLayerValues = null)
        {
            throw new NotImplementedException("GetLayerCost");
        }

        /// <summary>
        /// utilizes the internal methods of the firing neurons in a layer to adjust the weights and biases as well as the recommended change to the previus layers activation 
        /// </summary>
        /// <param name="costs">the cost of the layer</param>
        /// <param name="layer">the layer to be altered</param>
        /// <returns>the adjustments to be made to the layer-1 according to the layer</returns>
        private float[,] AdjustLayer(float[] costs, int layer)
        {

            
            throw new NotImplementedException("AdjustLayer");
        }






    }
}
