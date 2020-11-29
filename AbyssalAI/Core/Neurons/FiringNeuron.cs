using System;

namespace AbyssalAI.Core.Neurons
{
    public interface IFiringNeuron
    {
        public float GetActivation(float[,] activationTable);

        //dependant on GetActivation
        public float GetCost(float[,] activationTable, float expectedValue);

        
        public float GetBiasAdjust(float learningRate, float[,] activationArray);

        public float[] GetWeightAdjust(float learningRate, float[,] activationArray);
        public float[] GetActivationAdjust(float learningRate, float[,] activationArray); //💩Fix to actually work
        

        public void Adjust(IProposedNeuron prop);
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

        public float GetBiasAdjust(float learningRate, float[,] activationArray)
        {
            throw new NotImplementedException();
        }

        public float[] GetWeightAdjust(float learningRate, float[,] activationArray)
        {
            throw new NotImplementedException();
        }

        public float[] GetActivationAdjust(float learningRate, float[,] activationArray)
        {
            throw new NotImplementedException();
        }

        public void Adjust(IProposedNeuron prop)
        {
            throw new NotImplementedException();
        }
    }
}