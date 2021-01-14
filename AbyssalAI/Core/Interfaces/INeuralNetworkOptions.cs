using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbyssalAI.Core.Interfaces
{
    public interface INeuralNetworkOptions
    {
        public float LearningRate { get; set; }

        public int MaxEpochs { get; set; }

        public delegate float Activation(float z);

        public int[] LayerStructure { get; set; }
        public int MaxLayerDensity { get; }
    }
}
