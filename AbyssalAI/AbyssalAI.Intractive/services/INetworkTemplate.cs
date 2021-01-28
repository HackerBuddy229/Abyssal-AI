using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.dataWindow;
using AbyssalAI.Core.Interfaces;

namespace AbyssalAI.Interactive.services
{
    public interface INetworkTemplate
    {
        public string Title { get; set; }
        public string DataUri { get; set; }
        public float LearningRate { get; set; }
        public int MaxEpochs { get; set; }

        public int[] NetworkStructure { get; set; }
        public IDataRation GetDataWindow();

    }
}
