using System;
using AbyssalAI.Core.Interfaces;

namespace AbyssalAI.Core.models
{
    public class SigmoidActivationFunction : IActivationFunction
    {
        public float GetValue(float input)
        {
            var decimalResult = Math.Pow(Math.E, input) / (Math.Pow(Math.E, input) + 1);
            var result = (float) Math.Round(decimalResult, 9, MidpointRounding.AwayFromZero);
            return result;
        }

        public float GetDerivedValue(float input)
        {
            var decimalResult = Math.Pow(Math.E, input) / Math.Pow(Math.Pow(Math.E, input) + 1, 2);
            var result = (float) Math.Round(decimalResult, 9, MidpointRounding.AwayFromZero);
            return result;
        }
    }
}