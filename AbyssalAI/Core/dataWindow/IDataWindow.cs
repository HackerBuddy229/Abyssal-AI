namespace AbyssalAI.Core.dataWindow
{
    public interface IDataWindow
    {
        public float[] InputLayer { get; set; }
        public float[] OutputLayer { get; set; }

        public DataAllocation Allocation { get; set; }
    }
}
