using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbyssalAI.Core.Interfaces;

namespace AbyssalAI.Core.helpers
{
    public class Initialiser : IInitialiser<float>
    {
        private readonly Random _random;

        public Initialiser()
        {
            _random = new Random();
        }

        public float GenerateNewValue() //check math
        {
            var output = (float) _random.NextDouble();
            var boolean = _random.Next(1, 2) != 1;
            output = boolean ? output + 1 : output;

            return output;
        }
    }
}
