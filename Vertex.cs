namespace GraphGeneration
{
    public class Vertex
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public int Color { get; set; }

        public Vertex(int value)
        {
            Value = value;
        }
    }
}