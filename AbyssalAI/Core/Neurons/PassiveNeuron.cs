using System;
using AbyssalAI.Core.models;

namespace AbyssalAI.Core.Neurons
{
    public abstract class PassiveNeuron
    {
        public Coordinate NeuronLocation { get; set; }

        /// <summary>
        /// The activation method for the neuron; (Default to a leaky RElu)
        /// </summary>
        public Func<float, float> ActivationMethod = zValue => zValue > 0 ? zValue : 0;

        public float[,] Weights { get; set; }
        public float Bias { get; set; }
    }


}