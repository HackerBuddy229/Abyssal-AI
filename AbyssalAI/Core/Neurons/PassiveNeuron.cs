using System;
using AbyssalAI.Core.Neurons.models;

namespace AbyssalAI.Core.Neurons
{
    public abstract class PassiveNeuron
    {
        protected PassiveNeuron() { }
        public Coordinate NeuronLocation { get; set; }

        public bool IsOutputLayer { get; set; }
        public bool IsInputLayer { get; set; }

        public float[,] Weights { get; set; }
        public float Bias { get; set; }
    }


}