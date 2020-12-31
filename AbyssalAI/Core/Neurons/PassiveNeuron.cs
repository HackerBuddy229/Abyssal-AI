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
        public Func<float, float> ActivationMethod = activation => activation > 0 ? activation : 0;

        public Func<float, float, float> BiasDerivative = (cost, activation) => activation > 0 ? cost : 0;
        public Func<float, float, float, float> WeightDerivative = (cost, activation, previousLayerActivation) 
            => activation > 0 ? cost*previousLayerActivation : 0;

        public float[] Weights { get; set; }
        public float Bias { get; set; }
    }


}