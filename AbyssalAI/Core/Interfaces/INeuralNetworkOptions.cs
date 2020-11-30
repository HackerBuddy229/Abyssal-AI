using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbyssalAI.Core.Interfaces
{
    public interface INeuralNetworkOptions
    {
        public float LearningRate { get; }

        public int MaxEpochs { get; }

        public delegate float Activation(float z);

        public int[] LayerStructure { get; }
        public int MaxLayerDensity { get; }
    }
}
