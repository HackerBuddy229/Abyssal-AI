using System;
using AbyssalAI.Core.Interfaces;

namespace AbyssalAI.Core.models
{
    public class ReluActivationFunction : IActivationFunction
    {
        public float GetValue(float input)
        {
            var output = Math.Max(input, 0);
            return output;
        }

        public float GetDerivedValue(float input) => input > 0 ? 1 : 0; //TODO: verify that this works when x = 0
    }
}