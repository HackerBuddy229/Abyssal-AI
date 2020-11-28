using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.Interfaces;

namespace AbyssalAI.Core.models
{
    public class NeuralNetworkOptions : INeuralNetworkOptions
    {

        /// <summary>
        /// Learning rate for neural network; Should be between 0.1F and 0.0001F (Default: 0.001F)
        /// </summary>
        public float LearningRate { get; } = 0.001F; //TODO: set proper default learningRate

        /// <summary>
        /// Max epochs for the learning algorithm to go through; (Default: 10 000)
        /// </summary>
        public int MaxEpochs { get; } = 10000;

        /// <summary>
        /// The amount of hidden layers to create; (Default: 2)
        /// </summary>
        public int HiddenLayers { get; } = 2;

        /// <summary>
        /// The amount of neurons to create for each hidden layer; (Default: 8 for each)
        /// </summary>
        public int[] HiddenLayerNeuronDensity { get; } = {8, 8};
    }
}
