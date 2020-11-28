using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbyssalAI.Core.dataWindow
{
    public interface IDataWindow
    {
        public float[] InputLayer { get; set; }
        public float[] OutputLayer { get; set; }

        public DataAllocation Allocation { get; }
    }
}
