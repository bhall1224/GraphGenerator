namespace GraphGeneration
{
    public class Vertex
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public int Color { get; set; }
        public int Eccentricity { get; set; }

        public Vertex(int value)
        {
            Value = value;
            Label = string.Empty;
        }

        public override string ToString()
        {
            return $"[Value: {Value} Lable: {Label} Color: {Color} Eccentricity: {Eccentricity}]";
        }
    }
}