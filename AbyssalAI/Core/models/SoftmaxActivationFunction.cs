using System;
using System.Linq;
using AbyssalAI.Core.Interfaces;

namespace AbyssalAI.Core.models
{

    public class SoftmaxActivationFunction : IOutputActivationFunction
    {
        public float[] GetValue(float[] input) //TODO: You done fucked up Rasmus!!! FIX IT NOW!!!
        {
            var sum = 0D;
            input.ToList().ForEach(x => sum += Math.Pow(Math.E, x));

            var output =  input.Select(value => Math.Pow(Math.E, value) / sum)
                .Select(doubleActivation => (float)doubleActivation)
                .ToArray();
            
            if (output[0] == 0F)
                throw new Exception();
            return output;
        }

        public float GetDerivedValue(float activation, float expectedActivation) //TODO: probably right
        {
            return activation.Equals(expectedActivation)
                ? activation * (1 - activation)
                : -(expectedActivation) * activation;
        }
    }
}