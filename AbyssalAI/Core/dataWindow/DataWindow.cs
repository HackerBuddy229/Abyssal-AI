namespace AbyssalAI.Core.dataWindow
{
    public class DataWindow : IDataWindow
    {
        public DataWindow(float[] inputLayer, float[] outputLayer, DataAllocation allocation)
        {
            InputLayer = inputLayer;
            OutputLayer = outputLayer;
            Allocation = allocation;
        }

        public float[] InputLayer { get; }
        public float[] OutputLayer { get; }
        public DataAllocation Allocation { get; }
    }

    
}
