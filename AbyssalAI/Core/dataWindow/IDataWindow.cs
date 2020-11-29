namespace AbyssalAI.Core.dataWindow
{
    public interface IDataWindow
    {
        public float[] InputLayer { get; }
        public float[] OutputLayer { get; }

        public DataAllocation Allocation { get; }
    }
}
