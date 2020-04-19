using System;
using GraphGeneration;
using System.Diagnostics;

namespace graphgenerator
{
    class Program
    {
        static void Main(string[] args)
        {            
            int numOfNodes = Int32.Parse(args[0]);  
            GraphTest(numOfNodes);
        }

        static void GraphTest(int numOfNodes)
        {
            Console.WriteLine("making random graph!");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Graph g = Graph.RandomConnectedGraph(numOfNodes);
            //g.CalculateGraphEccentricities();
            timer.Stop();

            Console.WriteLine($"Graph of order {numOfNodes} created in {timer.ElapsedMilliseconds} ms");
            Console.WriteLine(g);
        }

        static void EccentricityTest(int numOfNodes)
        {
            Console.WriteLine("making random graph!");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Graph g = Graph.RandomConnectedGraph(numOfNodes);
            timer.Stop();

            Console.WriteLine($"Graph of order {numOfNodes} created in {timer.ElapsedMilliseconds} ms");
            Console.WriteLine(g);

            int maxDistance;

            for (int testRoot = 0; testRoot < numOfNodes; testRoot++)
            {
                timer.Start();
                g.VertexSet[testRoot].Eccentricity = 0;
                g.CaluclateDistancesFromRoot(testRoot, out maxDistance);
                Console.WriteLine($"max distance {maxDistance} from {testRoot}");
                timer.Stop();

                Console.WriteLine($"new graph calculations in {timer.ElapsedMilliseconds} ms: \n{g.ToString()}");
            }

            timer.Start();
            g.CalculateGraphEccentricities();
            Console.WriteLine($"all graph eccentricites");
            timer.Stop();

            Console.WriteLine($"new graph calculations in {timer.ElapsedMilliseconds} ms: \n{g.ToString()}");

        }
    }
}
