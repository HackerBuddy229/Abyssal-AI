using System;

namespace AbyssalAI.Core.Neurons
{
    public interface IFiringNeuron
    {
        public float GetActivation(float[,] activationTable);

        //dependant on GetActivation
        public float GetCost(float[,] activationTable, float expectedValue);

        public float[] Adjust(float learningRate, float[,] activationArray);
    }
    
    public class FiringNeuron : PassiveNeuron, IFiringNeuron
    {
        public float GetActivation(float[,] activationTable)
        {
            throw new NotImplementedException();
        }

        public float GetCost(float[,] activationTable, float expectedValue)
        {
            throw new NotImplementedException();
        }

        public float[] Adjust(float learningRate, float[,] activationArray)
        {
            throw new NotImplementedException();

            //bias

            //foreach wheight

            //foreach neuron activation

            // return neuron
        }
    }
}