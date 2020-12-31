using System;
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

        public float GenerateNewValue() //check that its not 0
        {
            var output = (float) _random.NextDouble();
            var boolean = _random.Next(1, 2) != 1;
            return boolean ? output + 1 : output;
        }
    }
}
