using System;
using AbyssalAI.Core.Interfaces;

namespace AbyssalAI.Core.models
{
    public class BinaryCrossEntropyCostFunction : ICostFunction
    {
        public float GetCost(float actualValue, float expectedValue)
        {
            var doubleOutput = -(expectedValue * Math.Log(actualValue) + (1 - expectedValue) * Math.Log(1 - actualValue));
            return (float) Math.Round(doubleOutput, 10, MidpointRounding.AwayFromZero);
        }

        public float GetDerivedValue(float actualValue, float expectedValue) => 
            (actualValue - expectedValue) / ((1 - actualValue) * actualValue);
    }
}