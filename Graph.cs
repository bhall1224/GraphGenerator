using System;
using System.Collections.Generic;
using System.Text;

namespace GraphGeneration
{
    class Graph
    {
        private bool[,] _adjacencyMatrix;
        public readonly HashSet<Edge> EdgeSet;

        //graph parameters
        public int Order { get; set; }
        public int Size { get; set; }
        public int MaxDegree { get; set; }
        public int MinDegree { get; set; }
        public int DegreeSum { get; set; }
        
        public Graph(int order)
        {
            Order = order;
            _adjacencyMatrix = new bool[order, order];
            EdgeSet = new HashSet<Edge>();
        }

        public void AddEdge(int v1, int v2)
        {
            Edge e = new Edge(v1, v2);
            EdgeSet.Add(e);
            Size++;
            _adjacencyMatrix[v1, v2] = true;
            _adjacencyMatrix[v2, v1] = true;
        }

        public void CalculateGraphParameters()
        {
            DegreeSum = 2 * Size;
            MaxDegree = 0;
            MinDegree = Order * (Order - 1);

            int degree;

            //find max degree and connectedness
            for (int i = 0; i < Order; i++)
            {
                degree = 0;

                for (int j = 0; j < Order; j++)
                {
                    if (_adjacencyMatrix[i, j] && i != j)
                    {
                        degree++;
                    }
                }

                if (degree > MaxDegree)
                {
                    MaxDegree = degree;
                }

                if (degree < MinDegree)
                {
                    MinDegree = degree;
                }
            }
        }

        public bool IsAdjacent(int v1, int v2)
        {
            return _adjacencyMatrix[v1, v2] || _adjacencyMatrix[v2, v1];
        }

        public static Graph RandomConnectedGraph(int order)
        {
            Random random = new Random();

            int size = random.Next(order - 1, order * (order - 1));
            
            Graph graph = new Graph(order);
            
            while (graph.Size < size)
            {
                int v1 = random.Next(order);
                int v2 = random.Next(order);
                                
                graph.AddEdge(v1, v2);
            }

            graph.CalculateGraphParameters();

            //graph is not connected
            if (graph.MinDegree < order * (order - 1) / 2)
            {
                //add bridges
            }

            return graph;
        }

        public static Graph CompleteGraph(int order)
        {
            Graph g = new Graph(order);

            for (int i = 0; i < order; i++)
            {
                for (int j = i + 1; j < order; j++)
                {
                    g.AddEdge(i, j);
                }
            }

            return g;
        }

        public static Graph Copy(Graph graph)
        {
            Graph g = new Graph(graph.Order)
            {
                Size = graph.Size,
                MaxDegree = graph.MaxDegree,
                DegreeSum = graph.DegreeSum
            };

            //copy hash set contents
            foreach (Edge e in graph.EdgeSet)
            {
                g.AddEdge(e.Vertex1, e.Vertex2);
            }

            return g;
        }        
    }
}
