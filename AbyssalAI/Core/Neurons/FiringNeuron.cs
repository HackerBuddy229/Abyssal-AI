using System;
using AbyssalAI.Core.helpers;
using AbyssalAI.Core.Interfaces;
using AbyssalAI.Core.models;

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

        public FiringNeuron(Coordinate location, 
            int amountOfWeights, 
            IInitialiser<float> weightInitialiser = null,
            IInitialiser<float> biasInitialiser = null)
        {
            NeuronLocation = location;

            InitialiseWeights(weightInitialiser ?? new Initialiser(), amountOfWeights);
            InitialiseBias(biasInitialiser ?? new Initialiser());
        }

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

        
        public float GetBiasAdjust(float learningRate, float[,] activationArray, float cost)
        {
            throw new NotImplementedException();
        }

        
        public float[] GetWeightAdjust(float learningRate, float[,] activationArray, float cost)
        {
            throw new NotImplementedException();
        }

        public void Adjust(IProposedNeuron prop)
        {
            throw new NotImplementedException();
        }

        private void InitialiseWeights(IInitialiser<float> initialiser, int amountOfWeights)
        {
            Weights = new float[amountOfWeights];
            for (var weight = 0; weight < amountOfWeights; weight++)
                Weights[weight] = initialiser.GenerateNewValue();
        }

        private void InitialiseBias(IInitialiser<float> initialiser)
        {
            Bias = initialiser.GenerateNewValue();
        }
    }
}