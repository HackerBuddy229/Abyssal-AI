namespace AbyssalAI.Core.Interfaces
{
    public interface ICostFunction
    {
        public float GetCost(float actualValue, float expectedValue);
        public float GetDerivedValue(float actualValue, float expectedValue);
    }
}