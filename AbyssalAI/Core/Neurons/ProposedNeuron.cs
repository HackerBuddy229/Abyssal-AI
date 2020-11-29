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
        public void AddActivationProposal(float prop);

        public float AvgBiasProposal { get; }
        public float AvgActivationProposal { get; }
        public float[] AvgWeightProposal { get; }
        
    }

    public class ProposedNeuron : IProposedNeuron
    {
        private readonly int _weightAmount;
        private readonly int _epochDataIndex;

        public ProposedNeuron(int weightAmount, int epochDataIndex)
        {
            _weightAmount = weightAmount;
            _epochDataIndex = epochDataIndex;

            //init private props[]
            _epochBiasProposals = new float[_epochDataIndex];
            _epochActivationProposals = new float[_epochDataIndex];

            _epochWeightProposals = new float[_epochDataIndex,_weightAmount];
        }


        public void AddBiasProposal(float prop)
        {
            throw new NotImplementedException();
        }

        public void AddWeightProposal(float[] prop)
        {
            throw new NotImplementedException();
        }

        public void AddActivationProposal(float prop)
        {
            throw new NotImplementedException();
        }

        public float AvgBiasProposal => _epochBiasProposals.Average();

        public float AvgActivationProposal => _epochActivationProposals.Average();

        public float[] AvgWeightProposal
        {
            get
            {
                var averageArray = new float[_weightAmount]; //fix

                for (var outer = 0; outer < _epochDataIndex+1; outer++)
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
