# GraphGenerator
## A basic class library for building graphs
Clone this library, build the .dll file and include in your Unity .csproj file class references, or simply copy and paste into your code directly.  

*Example usage*:
`Graph graph = Graph.RandomConnectedGraph(5);`
                
In this snippet a graph object is instantiated that will be a randomly generated connected graph of order 5 (5 verticies).  

One can then use this graph and retrieve some data such as the size, order, max degree, min degree, and degree sum.
The entire vertex and edge set is also available as a list of these objects.
These can be calculated using `graph.CalculateGraphParameters();`

u-v distances for a vertex u can be calculated as such: `graph.CaluclateDistancesFromRoot(0, out maxDistance);`
Where 0 is the integer label value for the vertex in quesiton.
One can rely on RandomConnectedGraph() to build the vertex set, or define the vertex set explicitly this way:
`int u = 0;`
`int v = 1;`
`int order = 2;`
`Graph g = new Graph(order);`
`g.AddEdge(u.Value(), v.Value());`

Vertex objects have data fields as well inteded for graph colorings and eccentricity.

Eccentricities are empty by default, but can be calculated using `graph.CalculateGraphEccentricities();`
