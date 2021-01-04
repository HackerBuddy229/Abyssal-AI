using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using AbyssalAI.Core.dataWindow;
using AbyssalAI.Core.models;
using AbyssalAI.Core.Neurons;

namespace AbyssalAI.Core.Interfaces
{
    public interface INeuralNetwork {
        public INeuralNetworkOptions Options { get; }
        public FiringNeuron[,] NeuronLayers { get; }

        public NetworkTrainingResult Train(IDataWindow[] trainingData, out ConcurrentBag<EpochResult> concurrentEpochCollection);
        public IValidationResult Validate(IDataWindow[] data);
        public float[] GetActivation(float[] inputLayer);


        public bool VerifyDataWindowValidity(IDataWindow dataWindow);
        public bool VerifyDataWindowValidity(IDataWindow[] dataWindow);

    }
}