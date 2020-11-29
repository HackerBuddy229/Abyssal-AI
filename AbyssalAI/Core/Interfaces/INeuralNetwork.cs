using System;
using AbyssalAI.Core.dataWindow;
using AbyssalAI.Core.models;
using AbyssalAI.Core.Neurons;

namespace AbyssalAI.Core.Interfaces
{
    public interface INeuralNetwork {
        public INeuralNetworkOptions Options { get; }
        public FiringNeuron[,] NeuronLayers { get; set; }
        public NetworkTrainingResult Train(IDataWindow[] trainingData);

        public bool VerifyDataWindowValidity(IDataWindow dataWindow);
    }
}