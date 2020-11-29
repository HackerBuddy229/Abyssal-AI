namespace AbyssalAI.Core.models
{
    public class Coordinate
    {
        public Coordinate(int layer, int neuron)
        {
            Layer = layer;
            Neuron = neuron;
        }

        public int Layer { get; }
        public int Neuron { get; }
    }
}
