namespace AbyssalAI.Core.Interfaces
{
    public interface IActivationFunction
    {
        float GetValue(float input);
        float GetDerivedValue(float input);
    }
}