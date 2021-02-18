using System;
using System.Linq;
using AbyssalAI.Core.Interfaces;

namespace AbyssalAI.Core.models
{

    public class SoftmaxActivationFunction : IOutputActivationFunction
    {
        public float[] GetValue(float[] input)
        {
            var sum = 0d;
            input.ToList().ForEach(x => sum += Math.Pow(Math.E, x));

            return input.Select(value => value / sum)
                .Select(doubleActivation => (float) Math.Round(doubleActivation, 10, MidpointRounding.AwayFromZero))
                .ToArray();
        }

        public float GetDerivedValue(float activation, float expectedActivation) //TODO: probably right
        {
            return activation.Equals(expectedActivation)
                ? activation * (1 - activation)
                : -(expectedActivation) * activation;
        }
    }
}