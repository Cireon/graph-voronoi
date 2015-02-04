using System;
using System.Drawing;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed partial class Graph
    {
        private static Random random = new Random();

        public static Graph Generate(Player[] players)
        {
            const int gridWidth = 6;
            const int gridHeight = 4;
            const int nVertices = 10;
            const int nEdges = 25;
            const int nSites = 4;

            var coords = Enumerable.Range(0, gridWidth)
                .SelectMany(i => Enumerable.Range(0, gridHeight).Select(j => new Tuple<int, int>(i, j)))
                .SelectRandom(nVertices);

            var graph = new Graph(players);

            var vertices = coords.Select(t =>
            {
                var v =
                    new Vertex(new PointF(50 + 180 * t.Item1 + (float) (random.NextDouble() - .5) * 50,
                        50 + 180 * t.Item2 + (float) (random.NextDouble() - .5) * 50));
                graph.AddVertex(v);
                return v;
            }).ToArray();

            var conns =
                Enumerable.Range(1, vertices.Length - 1)
                    .SelectMany(m => Enumerable.Range(0, m).Select(i => new Tuple<int, int>(i, m)))
                    .SelectRandom(nEdges);

            var edges = conns.Select(t =>
            {
                var e = new Edge(vertices[t.Item1], vertices[t.Item2]);
                graph.AddEdge(e);
                return e;
            }).ToArray();

            for (int i = 0; i < nSites; i++)
            {
                var e = edges.RandomElement();
                graph.AddMarker(players.RandomElement(), e, (float)random.NextDouble());
            }

            return graph;
        }
    }
}