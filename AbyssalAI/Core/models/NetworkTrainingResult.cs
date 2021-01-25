using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbyssalAI.Core.models
{
    public class NetworkTrainingResult
    {
        public IList<EpochResult> EpochResults { get; init; } = new List<EpochResult>();

        public static string GetRepresentativeText(NetworkTrainingResult result)
        {
            throw new NotImplementedException();
        }
    }
}
