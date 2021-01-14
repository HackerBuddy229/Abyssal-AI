using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.dataWindow;

namespace AbyssalAI.Core.Interfaces
{
    public interface IDataRation
    {
        public IDataWindow[] TrainingData { get; set; }
        public IDataWindow[] VerificationData { get; set; }
    }
}
