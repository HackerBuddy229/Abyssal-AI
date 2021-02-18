namespace AbyssalAI.Core.Interfaces
{
    public interface IOutputActivationFunction
    {
        float[] GetValue(float[] input);

        float GetDerivedValue(float activation, float expectedActivation);
    }
}