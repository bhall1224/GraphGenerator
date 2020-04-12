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
            Console.WriteLine("making random graph!");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Graph g = Graph.RandomConnectedGraph(numOfNodes);
            timer.Stop();

            Console.WriteLine($"Graph of order {numOfNodes} created in {timer.ElapsedMilliseconds} ms");
            Console.WriteLine(g);
        }
    }
}
