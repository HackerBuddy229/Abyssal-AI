using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.dataWindow;

namespace AbyssalAI.Core.Interfaces
{
    public interface IDataDistributor
    {
        public IDataRation CreateDefaultDataRation(IDataWindow[] dataWindows, float distribution = 2.0F / 3.0F);
    }
}
