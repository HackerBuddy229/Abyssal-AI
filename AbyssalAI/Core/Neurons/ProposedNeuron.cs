using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbyssalAI.Core.Neurons
{
    public interface IProposedNeuron
    {
        public void AddBiasProposal(float prop);
        public void AddWeightProposal(float[] prop);

        public float AvgBiasProposal { get; }
        public float AvgActivationProposal { get; }
        public float[] AvgWeightProposal { get; }
        
    }

    public class ProposedNeuron : IProposedNeuron
    {
        private readonly int _weightAmount;
        private readonly int _epochDataCount;

        public ProposedNeuron(int weightAmount, int epochDataCount)
        {
            _weightAmount = weightAmount;
            _epochDataCount = epochDataCount;

            //init private props[]
            _epochBiasProposals = new float[_epochDataCount];
            _epochActivationProposals = new float[_epochDataCount];

            _epochWeightProposals = new float[_epochDataCount,_weightAmount];
        }


        public void AddBiasProposal(float prop)
        {
            var index = _epochBiasProposals.Select((value, innerIndex) => value == 0 ? innerIndex : int.MaxValue).First();
            _epochBiasProposals[index] = prop;
        }

        public void AddWeightProposal(float[] prop)
        {
            for (var index = 0; index < _epochWeightProposals.GetLength(0); index++)
            {
                if (_epochWeightProposals[index, 0] != 0)
                    continue;

                for (var props = 0; props < prop.Length; props++)
                    _epochWeightProposals[index, props] = prop[props];
                break;
            }
        }


        public float AvgBiasProposal => _epochBiasProposals.Average();

        public float AvgActivationProposal => _epochActivationProposals.Average();

        public float[] AvgWeightProposal
        {
            get
            {
                var averageArray = new float[_weightAmount]; //fix

                for (var outer = 0; outer < _epochDataCount+1; outer++)
                for (var inner = 0; inner < _epochWeightProposals.GetLength(1); inner++)
                {
                    averageArray[inner] += _epochWeightProposals[outer, inner];
                }

                
                for (var i = 0; i < averageArray.Length; i++)
                {
                    averageArray[i] = averageArray[i] / _weightAmount;
                }

                return averageArray;
            }
        }

        

        private readonly float[] _epochBiasProposals;

        private readonly float[,] _epochWeightProposals;

        private readonly float[] _epochActivationProposals;
    }
}
