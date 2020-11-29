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
        /// optional pre filled dimensions depending on the window allocation 
        /// </summary>
        /// <param name="data">The data to be used if the allocation specifies dimensions to be pre filled</param>
        /// <returns>an array the same size as the neural network with optionally pre-filled dimensions</returns>
        private float[,] CreateActivationArray(IDataWindow data)
        {
            throw new NotImplementedException("CreateActivationArray");
        }


        /// <summary>
        /// Fills any voids in the current activationArray
        /// </summary>
        /// <param name="data">The current activation array</param>
        private void FillActivationArray(ref float[,] data)
        {
            throw new NotImplementedException("FillActivationArray");
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
