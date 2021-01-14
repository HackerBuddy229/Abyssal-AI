using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.dataWindow;

namespace AbyssalAI.Interactive.services
{
    public interface IDataSerializer<out T>
    {
        public IEnumerable<T> SerializeData(string path);
    }
}
