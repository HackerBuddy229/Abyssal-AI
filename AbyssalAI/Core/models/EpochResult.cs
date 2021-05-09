using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbyssalAI.Core.models
{
    public class EpochResult
    {
        public int EpochIndex { get; set; }
        public float AverageOffset { get; set; }
        public NeuronPairing AverageCost { get; set; }
        public NeuronPairing AverageOutputLayerActivation { get; set; }
        public float Accuracy { get; set; }
    }
}
