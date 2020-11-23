using System;

namespace AbyssalAI.Core.Neurons
{
    public interface IPassiveNeuron
    {
        public float[,] Weights { get; set; }
        public float Bias { get; set; }
    }

    public class PassiveNeuron : IPassiveNeuron
    {
        public float[,] Weights { get; set; }
        public float Bias { get; set; }
    }
}