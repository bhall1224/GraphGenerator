using System;
using System.Collections.Generic;
using System.Text;

namespace MapGeneration
{
    class MapGenerator
    {
        private Graph _graph;

        public MapGenerator(int order, int size)
        {
            _graph = Graph.RandomGraph(order, size);
        }
    }
}
