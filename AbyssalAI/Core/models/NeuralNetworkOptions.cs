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
        public float LearningRate { get; set; } = 0.001F; //TODO: set proper default learningRate

        /// <summary>
        /// Max epochs for the learning algorithm to go through; (Default: 10 000)
        /// </summary>
        public int MaxEpochs { get; set; } = 10000;


        public float? AccuracyGoal { get; set; } = 0.8F;

        /// <summary>
        /// Structure of neural network; (NO DEFAULT)
        /// </summary>
        public int[] LayerStructure { get; set; } = null;

        public int MaxLayerDensity => LayerStructure.OrderBy(x => x).First();

        /// <summary>
        /// The activation method to use
        /// </summary>
        /// <param name="z">(weight*activation)+B</param>
        /// <returns>the activation of the neuron</returns>
        public delegate float ActivationMethod(float z);
    }
}
