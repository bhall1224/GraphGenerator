using System;
using GraphGeneration;
using System.Timers;

namespace graphgenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int numOfNodes = Int32.Parse(args[0]);
            Console.WriteLine("making random graph!");
            Timer timer = new Timer();
            timer.Start();
            Graph g = Graph.RandomConnectedGraph(numOfNodes);
            timer.Stop();

            Console.WriteLine($"Graph of order {numOfNodes} created in {timer.Interval} ms");
            Console.WriteLine(g);
        }
    }
}
