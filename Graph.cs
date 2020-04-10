using System;
using System.Collections.Generic;
using System.Text;

namespace MapGeneration
{
    class Graph
    {
        //graph parameters
        public int NumOfEdges { get; set; }
        public int MaxDegree { get; set; }
        public int DegreeSum { get; set; }

        private HashSet<Edge> _edgeSet = new HashSet<Edge>();

        public void addEdge(int v1, int v2)
        {
            Edge e = new Edge(v1, v2);
            _edgeSet.Add(e);
            NumOfEdges++;
        }

        public bool IsAdjacent(int v1, int v2)
        {
            return (_edgeSet.Contains(new Edge(v1, v2)) || _edgeSet.Contains(new Edge(v2, v1)));
        }

        public static Graph RandomGraph(int order, int size)
        {
            if (size < 0 || size > (order * (order - 1) / 2))
                throw new Exception();
            
            Graph graph = new Graph();
            Random random = new Random();

            graph.DegreeSum = 2 * size;
            
            while (graph.NumOfEdges < size)
            {
                int v1 = random.Next(order);
                int v2 = random.Next(order);
                                
                graph.addEdge(v1, v2);
            }

            //find max degree and connectedness

            return graph;
        }

        public static Graph EmptyGraph(int size)
        {
            return new Graph();
        }

        public static Graph Copy(Graph graph)
        {
            Graph g = new Graph
            {
                NumOfEdges = graph.NumOfEdges,
                MaxDegree = graph.MaxDegree,
                DegreeSum = graph.DegreeSum
            };

            //copy hash set contents
            foreach (Edge e in graph._edgeSet)
            {
                g._edgeSet.Add(e);
            }
            return g;
        }

        private class Edge
        {
            public int Vertex1 { get; set; }
            public int Vertex2 { get; set; }

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
                    return (Vertex1 == e.Vertex1 && Vertex2 == e.Vertex2);
                }
            }
        }
    }
}
