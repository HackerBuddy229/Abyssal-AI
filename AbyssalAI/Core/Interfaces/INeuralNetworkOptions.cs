using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbyssalAI.Core.Interfaces
{
    public interface INeuralNetworkOptions
    {
        public float LearningRate { get; init; }
        
        public IActivationFunction ActivationFunction { get; init; }
        public IOutputActivationFunction OutputActivationFunction { get; init; }
        public ICostFunction CostFunction { get; init; }

        public int MaxEpochs { get; init; }

        public delegate float Activation(float z);
        public float? AccuracyGoal { get; init; }

        public int[] LayerStructure { get; init; }
        public int MaxLayerDensity { get; }
    }
}
