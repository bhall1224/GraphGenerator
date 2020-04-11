namespace GraphGeneration
{
    public class Edge
    {
        public int Vertex1 { get; set; }
        public int Vertex2 { get; set; }
        public int Weight { get; set; }
        public int Color { get; set; }

        public Edge(int v1, int v2)
        {
            Vertex1 = v1;
            Vertex2 = v2;
        }

        public override bool Equals(object obj)
        {
            if (!GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Edge e = (Edge)obj;
                return (Vertex1 == e.Vertex1 && Vertex2 == e.Vertex2)
                    || (Vertex2 == e.Vertex1 && Vertex1 == e.Vertex2);
            }
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
    }
}
