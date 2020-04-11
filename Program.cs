using System;
using GraphGeneration;

namespace graphgenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("making random graph!");

            Graph g = Graph.RandomConnectedGraph(5);

            Console.WriteLine(g);
        }
    }
}
