using System;

namespace AbyssalAI.Core.Neurons
{
    public interface IFiringNeuron
    {
        ref IPassiveNeuron StateNeuron { get; }

        public float GetActivation();
        public float GetCost(float expected);


    }
    
    public class FiringNeuron : IFiringNeuron
    {
        private IPassiveNeuron _stateNeuron;

        public FiringNeuron(IPassiveNeuron stateNeuron)
        {
            _stateNeuron = stateNeuron;
        }

        public ref IPassiveNeuron StateNeuron => ref _stateNeuron;
        public float GetActivation()
        {
            throw new NotImplementedException();
        }

        public float GetCost(float expected)
        {
            throw new NotImplementedException();
        }
    }
}