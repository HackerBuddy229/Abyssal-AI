namespace AbyssalAI.Core.Interfaces
{
    public interface ICostFunction
    {
        public float GetCost(float actualValue, float expectedValue);
    }
}