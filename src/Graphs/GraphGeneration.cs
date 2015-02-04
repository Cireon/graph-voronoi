using System;
using System.Drawing;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed partial class Graph
    {
        private static readonly Random random = new Random();

        public static Graph Generate(GenerationParameters parameters, Player[] players, Size panelSize)
        {
            var coords = Enumerable.Range(0, parameters.GridWidth)
                .SelectMany(i => Enumerable.Range(0, parameters.GridHeight).Select(j => new Tuple<int, int>(i, j)))
                .SelectRandom(parameters.NVertices);

            var graph = new Graph(players);

            var w = (float) (panelSize.Width - 100) / (parameters.GridWidth - 1);
            var h = (float) (panelSize.Height - 100) / (parameters.GridHeight - 1);

            var vertices = coords.Select(t =>
            {
                var v =
                    new Vertex(new PointF(50 + w * (t.Item1 + (float) (random.NextDouble() - .5) * parameters.PerturbationPercentage),
                        50 + h * (t.Item2 + (float) (random.NextDouble() - .5) * parameters.PerturbationPercentage)));
                graph.AddVertex(v);
                return v;
            }).ToArray();

            var conns =
                Enumerable.Range(1, vertices.Length - 1)
                    .SelectMany(m => Enumerable.Range(0, m).Select(i => new Tuple<int, int>(i, m))).ToList();

            if (parameters.ForcePlanar)
            {
                int edgesToInsert = parameters.NEdges;

                conns.Shuffle();

                for (int i = 0; i < conns.Count; i++)
                {
                    var tuple = conns[i];

                    if (graph.Edges.Any(e =>
                    {
                        var v1 = e.From;
                        var v2 = e.To;
                        var u1 = vertices[tuple.Item1];
                        var u2 = vertices[tuple.Item2];

                        return v1 != u1 && v2 != u2 && v1 != u2 && v2 != u1 &&
                            doIntersect(v1.Position, v2.Position, u1.Position, u2.Position);
                    }))
                    {
                        continue;
                    }

                    var edge = new Edge(vertices[tuple.Item1], vertices[tuple.Item2]);
                    graph.AddEdge(edge);

                    edgesToInsert--;
                    if (edgesToInsert <= 0)
                        break;
                }
            }
            else
            {
                foreach (var t in conns.SelectRandom(parameters.NEdges))
                {
                    var edge = new Edge(vertices[t.Item1], vertices[t.Item2]);
                    graph.AddEdge(edge);
                }
            }

            for (int i = 0; i < parameters.NSites; i++)
            {
                var e = graph.Edges.RandomElement();
                graph.AddMarker(players.RandomElement(), e, (float)random.NextDouble());
            }

            return graph;
        }

        #region line intersection
        // Given three colinear points p, q, r, the function checks if
        // point q lies on line segment 'pr'
        private static bool onSegment(PointF p, PointF q, PointF r)
        {
            return q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y);
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        private static int orientation(PointF p, PointF q, PointF r)
        {
            // See 10th slides from following link for derivation of the formula
            // http://www.dcs.gla.ac.uk/~pat/52233/slides/Geometry1x1.pdf
            float val = (q.Y - p.Y) * (r.X - q.X) -
                      (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;  // colinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        // The main function that returns true if line segment 'p1q1'
        // and 'p2q2' intersect.
        private static bool doIntersect(PointF p1, PointF q1, PointF p2, PointF q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases
        }
        #endregion

        public class GenerationParameters
        {
            public int GridWidth;
            public int GridHeight;
            public int NVertices;
            public int NEdges;
            public int NSites;
            public float PerturbationPercentage;
            public bool ForcePlanar;
        }
    }
}