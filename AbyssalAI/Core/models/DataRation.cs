using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.dataWindow;
using AbyssalAI.Core.Interfaces;

namespace AbyssalAI.Core.models
{
    public class DataRation : IDataRation
    {
        public IDataWindow[] TrainingData { get; set; }
        public IDataWindow[] VerificationData { get; set; }
    }
}
