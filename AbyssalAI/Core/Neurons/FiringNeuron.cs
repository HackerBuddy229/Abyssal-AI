using System;

namespace AbyssalAI.Core.Neurons
{
    public interface IFiringNeuron
    {
        public float GetActivation(int epoch = 0);

        //dependant on GetActivation
        public float GetCost(int epoch = 0);

    }
    
    public class FiringNeuron : PassiveNeuron, IFiringNeuron
    {

        public float GetActivation(int epoch = 0)
        {
            throw new NotImplementedException();
        }

        public float GetCost(int epoch = 0)
        {
            throw new NotImplementedException();
        }

    }
}