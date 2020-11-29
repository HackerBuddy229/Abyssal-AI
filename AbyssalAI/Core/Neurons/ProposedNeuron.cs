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
        private readonly int _previousLayerDensity;
        private readonly int _epochDataIndex;

        public ProposedNeuron(int previousLayerDensity, int epochDataIndex)
        {
            _previousLayerDensity = previousLayerDensity;
            _epochDataIndex = epochDataIndex;

            //init private props[]
            _epochBiasProposals = new float[_epochDataIndex];
            _epochActivationProposals = new float[_epochDataIndex];

            _epochWeightProposals = new float[_epochDataIndex,_previousLayerDensity];
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

        public float[] AvgWeightProposal //TODO: 💩Fix shit code...🤦‍
        {
            get
            {
                var averageArray = new float[_epochWeightProposals.Length-1]; //fix

                for (var outer = 0; outer < _epochDataIndex+1; outer++)
                for (var inner = 0; inner < _epochWeightProposals.Length; inner++)
                {
                    averageArray[inner] += _epochWeightProposals[outer, inner];
                }

                
                for (var i = 0; i < averageArray.Length; i++)
                {
                    averageArray[i] = averageArray[i] / _previousLayerDensity;
                }

                return averageArray;
            }
        }

        

        private readonly float[] _epochBiasProposals;

        private readonly float[,] _epochWeightProposals;

        private readonly float[] _epochActivationProposals;
    }
}
