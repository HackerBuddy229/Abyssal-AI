using System.Linq;
using AbyssalAI.Core.dataWindow;
using AbyssalAI.Core.Interfaces;
using AbyssalAI.Core.models;

namespace AbyssalAI.Core.helpers
{
    public class DataDistributor : IDataDistributor
    {
        public IDataRation CreateDefaultDataRation(IDataWindow[] dataWindows, float distribution = 2.0F/3.0F)
        {
            var trainingSampleSize = (int)(dataWindows.Length * distribution);
            var trainingData = dataWindows.Take(trainingSampleSize).ToArray();
            var verificationData = dataWindows.Skip(trainingSampleSize).ToArray();

            var output = new DataRation
            {
                TrainingData = trainingData,
                VerificationData = verificationData
            };

            return output;
        }
    }
}
