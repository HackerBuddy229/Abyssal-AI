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

        public DataWindow() { }

        public float[] InputLayer { get; set; }
        public float[] OutputLayer { get; set; }
        public DataAllocation Allocation { get; set; }
    }

    
}
