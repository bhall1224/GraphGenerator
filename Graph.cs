using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace GraphGeneration
{
    class Graph
    {
        private bool[,] _adjacencyMatrix;
        private int[,] _distanceMatrix;

        public readonly List<Edge> EdgeSet;
        public readonly List<Vertex> VertexSet;

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
            _distanceMatrix = new int[order, order];
            EdgeSet = new List<Edge>();
            VertexSet = new List<Vertex>();

            for (int i = 0; i < order; i++)
                VertexSet.Add(new Vertex(i));
        }

        public bool AddEdge(int v1, int v2, bool directional = false)
        {
            Edge e = new Edge(v1, v2);

            if (v1 != v2 && !_adjacencyMatrix[v1, v2])
            {
                Size++;
                _adjacencyMatrix[v1, v2] = true;
                EdgeSet.Add(e);

                if (!directional)
                    _adjacencyMatrix[v2, v1] = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddDistance(int rootV, int targetV, int distance, bool directional = false)
        {
            _distanceMatrix[rootV, targetV] = distance;

            if (!directional)
                _distanceMatrix[targetV, rootV] = distance;

            VertexSet[targetV].Eccentricity = distance;
        }

        public int GetDistance(int i, int j)
        {
            return _distanceMatrix[i, j];
        }

        public void CalculateGraphEccentricities()
        {
            List<int> vDistances = new List<int>();
            int maxDistance;

            for (int i = 0; i < Order; i++)
            {
                //populates Distance matrix
                CaluclateDistancesFromRoot(i, out maxDistance);

                vDistances.Add(maxDistance);
            }

            for (int i = 0; i < Order; i++)
            {
                VertexSet[i].Eccentricity = vDistances[i];
            }
        }

        public void CalculateGraphParameters()
        {
            DegreeSum = 2 * Size;
            MaxDegree = 0;
            MinDegree = Order * (Order - 1);

            int degree;

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
            return v1 != v2 && (_adjacencyMatrix[v1, v2] || _adjacencyMatrix[v2, v1]);
        }

        public void CaluclateDistancesFromRoot(int root, out int maxDistance, bool bigraph = false)
        {
            //normal BFS data structures
            bool[] visited = new bool[Order];
            bool[] inQueue = new bool[Order];
            Queue<int> adjQueue = new Queue<int>();
            List<int> visitedList = new List<int>();

            //starting vertex
            adjQueue.Enqueue(root); 

            //v pulled from Queue
            int visitingVtx; 

            maxDistance = 0;

            while (adjQueue.Count > 0)
            {
                visitingVtx = adjQueue.Dequeue();
                visited[visitingVtx] = true;

                for (int i = 0; i < Order; i++)
                {
                    if (!visited[i] && !inQueue[i]
                        && IsAdjacent(visitingVtx, i))
                    {
                        adjQueue.Enqueue(i);
                        inQueue[i] = true;

                        if (i == root && !bigraph)
                        {
                            AddDistance(root, i, 0);
                        }
                        else if (IsAdjacent(i, root))
                        {
                            AddDistance(root, i, 1);
                        }
                        else
                        {
                            int distance = VertexSet[visitingVtx].Eccentricity + 1;
                            AddDistance(root, i, distance);

                            if (distance > maxDistance)
                                maxDistance = distance;
                        }
                    }
                }

                if (visitedList.Count + adjQueue.Count == Order)
                    return;
            }
        }

        private static bool TestConnectivityBFS(
            Graph g, 
            out List<int> disconnectedVertices, 
            out List<int> connectedVertices)
        {
            bool[] visited = new bool[g.Order];
            bool[] inQueue = new bool[g.Order];
            Queue<int> adjQueue = new Queue<int>();

            adjQueue.Enqueue(0); //starting vertex
            int visitingVtx;

            while (adjQueue.Count > 0)
            {
                visitingVtx = adjQueue.Dequeue();
                visited[visitingVtx] = true;

                for (int i = 0; i < g.Order; i++)
                {
                    if (!visited[i] && !inQueue[i]
                        && g.IsAdjacent(visitingVtx, i))
                    {
                        adjQueue.Enqueue(i);
                        inQueue[i] = true;
                    }
                }
            }

            // any v not visited is not connected
            disconnectedVertices = new List<int>();
            connectedVertices = new List<int>();

            for (int i = 0; i < g.Order; i++)
            {
                if (visited[i])
                    connectedVertices.Add(i);
                else
                    disconnectedVertices.Add(i);
            }


            if (connectedVertices.Count == g.Order)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
        * Runtime analysis for graph of order n and size m:
        * 2m + 2n for bridging disconnected graphs
        */
        public static Graph RandomConnectedGraph(int order)
        {
            Random random = new Random();

            int size = random.Next(order - 1, order * (order - 1) / 2);
            Graph graph = new Graph(order);
            
            while (graph.Size < size)
            {
                int v1 = random.Next(order);
                int v2 = random.Next(order);
                                
                graph.AddEdge(v1, v2);
            }            

            //add bridges to these vertices if not connected
            List<int> disconnectedVertices;
            List<int> connectedVertices;
            
            if (!TestConnectivityBFS(graph, out disconnectedVertices, out connectedVertices))
            {
                while (connectedVertices.Count < order)
                {
                    //select random vertex from connected subgraph
                    int randIndex = random.Next(connectedVertices.Count);
                    int disconnVertex = disconnectedVertices.First();

                    if (graph.AddEdge(connectedVertices[randIndex], disconnVertex))
                    {
                        connectedVertices.Add(disconnVertex);
                        disconnectedVertices.RemoveAt(0);
                    }
                }
            }

            graph.CalculateGraphParameters();
            graph.CalculateGraphEccentricities();
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

        public override string ToString()
        {
            string display = string.Empty;

            display += $"Order: {Order}\n";
            display += $"Size: {Size}\n";
            display += $"Max Degree: {MaxDegree}\n";
            display += $"Min Degree: {MinDegree}\n";

            display += "Vertex set: [ \n";
            foreach (Vertex v in VertexSet)
            {
                display += v.ToString() + "\n";
            }
            display += "]\n";

            display += "Edge set: [\n";
            foreach (Edge e in EdgeSet)
            {
                display += e.ToString();
            }
            display += "]\n";

            return display;
        }
    }
}
