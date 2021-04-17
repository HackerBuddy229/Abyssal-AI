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

            _epochWeightProposals = new float[_epochDataCount,_weightAmount];
        }


        public void AddBiasProposal(float prop)
        {
            if (float.IsNaN(prop)) //TODO: Remove
                throw new Exception();
            _epochBiasProposals[_epochBiasProposalIndex] = prop;
            _epochBiasProposalIndex++;
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


        public float AvgBiasProposal
        {
            get
            {
                var average = _epochBiasProposals.Average();
                if (float.IsNaN(average)) //TODO: Remove
                    Console.WriteLine("Error");
                return average;
            }
        }

        public float[] AvgWeightProposal
        {
            get
            {
                var output = new float[_weightAmount]; //fix

                for (var outer = 0; outer < _epochDataCount; outer++)
                for (var inner = 0; inner < _epochWeightProposals.GetLength(1); inner++)
                {
                    output[inner] += _epochWeightProposals[outer, inner];
                }

                
                for (var i = 0; i < output.Length; i++)
                {
                    output[i] = output[i] / _weightAmount;
                    if (float.IsNaN(output[i])) //TODO: Remove
                        Console.WriteLine("Error");
                }
                
                
                return output;
            }
        }

        

        private readonly float[] _epochBiasProposals;
        private int _epochBiasProposalIndex = 0;

        private readonly float[,] _epochWeightProposals;
    }
}
