using System;

namespace AbyssalAI.Core.Neurons
{
    public interface IFiringNeuron
    {
        public float GetActivation(float[,] activationTable);

        
        public float GetBiasAdjust(float learningRate, float[,] activationArray, float cost);

        public float[] GetWeightAdjust(float learningRate, float[,] activationArray, float cost);

        public void Adjust(IProposedNeuron prop);
    }
    
    public class FiringNeuron : PassiveNeuron, IFiringNeuron
    {

        /// <summary>
        /// Gets the activation value of the neuron
        /// </summary>
        /// <param name="activationTable">An activation table with all activations up to L-1 filled in</param>
        /// <returns>the float value of the neuron activasion</returns>
        public float GetActivation(float[,] activationTable)
        {
            float z = 0;
            //z += foreach wheight L-1
            for (var neuron = 0; neuron < Weights.Length; neuron++) {
                z += activationTable[NeuronLocation.Layer-1, neuron] * Weights[neuron]; //verify
            }

            z += Bias; //TODO: verify
            var activation = ActivationMethod(z); //activation = p.activationMethod(z)

            return activation;
        }

        //dep
        public float GetBiasAdjust(float learningRate, float[,] activationArray, float cost)
        {
            throw new NotImplementedException();
        }

        //dep
        public float[] GetWeightAdjust(float learningRate, float[,] activationArray, float cost)
        {
            throw new NotImplementedException();
        }


        //dep
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