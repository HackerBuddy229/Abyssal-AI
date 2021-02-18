using System;
using System.Collections.Generic;
using System.Linq;
using AbyssalAI.Core.helpers;
using AbyssalAI.Core.Interfaces;
using AbyssalAI.Core.models;

namespace AbyssalAI.Core.Neurons
{
    public interface IFiringNeuron
    {
        public float GetActivation(float[,] activationTable);
        public float GetInputValue(float[,] activationTable);
        
        public float GetBiasAdjust(float[,] activationArray, float cost);

        public float[] GetWeightAdjust(float[,] activationArray, float cost);

        public void Adjust(IProposedNeuron prop, float learningRate);
    }
    
    public class FiringNeuron : PassiveNeuron, IFiringNeuron
    {
        private readonly IActivationFunction _activationFunction;

        public FiringNeuron(Coordinate location, 
            int amountOfWeights,
            IActivationFunction activationFunction,
            IInitialiser<float> weightInitialiser = null,
            IInitialiser<float> biasInitialiser = null)
        {
            _activationFunction = activationFunction;
            NeuronLocation = location;

            InitialiseWeights(weightInitialiser ?? new Initialiser(), amountOfWeights);
            InitialiseBias(biasInitialiser ?? new Initialiser());
        }

        /// <summary>
        /// Gets the activation value of the neuron
        /// </summary>
        /// <param name="activationTable">An activation table with all activations up to L-1 filled in</param>
        /// <returns>the float value of the neuron activation</returns>
        public float GetActivation(float[,] activationTable)
        {
            var z = GetInputValue(activationTable);
            var activation = _activationFunction.GetValue(z); //activation = p.activationMethod(z)

            return activation;
        }

        public float GetInputValue(float[,] activationTable)
        {
            var z = 0F;
            
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var neuron = 0; neuron < Weights.Length; neuron++)
                z += activationTable[NeuronLocation.Layer - 1, neuron] * Weights[neuron];
            
            //z += foreach weight L-1

            z += Bias;
            return z;
        }


        public float GetBiasAdjust(float[,] activationArray, float cost)
        {
            var activation = activationArray[NeuronLocation.Layer, NeuronLocation.Neuron];
            var derivative = cost*_activationFunction.GetDerivedValue(activation);
            
            
            return derivative;
        }

        
        public float[] GetWeightAdjust(float[,] activationArray, float cost)
        {
            var currentActivation = activationArray[NeuronLocation.Layer, NeuronLocation.Neuron];
            
            //activations
            var preLayerActivation = new List<float>(Weights.Length);
            for (var weight = 0; weight < Weights.Length; weight++)
            {
                var activation = activationArray[NeuronLocation.Layer - 1, weight];
                preLayerActivation.Add(activation);
            }

            //derivation
            var derivation = new List<float>(Weights.Length);
            for (var derivative = 0; derivative < Weights.Length; derivative++)
            {
                var weightCost = cost * 
                                 _activationFunction.GetDerivedValue(currentActivation) *
                                 preLayerActivation[derivative];
                
                derivation.Add(weightCost); 
            }

            
                    

            

            
                
            return derivation.ToArray();
        }

        public void Adjust(IProposedNeuron prop, float learningRate)
        {
            Bias *= prop.AvgBiasProposal * learningRate;
            for (var weight = 0; weight < Weights.Length; weight++)
                Weights[weight] *= prop.AvgWeightProposal[weight] * learningRate;
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