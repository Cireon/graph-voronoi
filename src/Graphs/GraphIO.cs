using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed partial class Graph
    {
        public void SaveToFile(string file, IEnumerable<Player> players)
        {
            int index = 0;
            var vertexDict = this.vertices.ToDictionary(v => v, v => index++);
            index = 0;
            var edgeDict = this.edges.ToDictionary(e => e, e => index++);
            index = 0;
            var playerDict = players.ToDictionary(p => p, p => index++);

            using (var writer = new StreamWriter(file))
            {
                foreach (var v in this.vertices)
                    writer.WriteLine("v {0} {1}", v.Position.X, v.Position.Y);
                foreach (var e in this.edges)
                    writer.WriteLine("e {0} {1}", vertexDict[e.From], vertexDict[e.To]);
                foreach (var m in this.markers)
                    writer.WriteLine("m {0} {1} {2}", playerDict[m.Player], edgeDict[m.Edge], m.T);
            }
        }

        public static Graph FromFile(string file, Player[] players)
        {
            var graph = new Graph { CalculationsDisabled = true };

            var vertices = new List<Vertex>();
            var edges = new List<Edge>();

            using (var reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    switch (line[0])
                    {
                        case 'v':
                            Graph.parseVertex(graph, line, vertices);
                            break;
                        case 'e':
                            Graph.parseEdge(graph, line, vertices, edges);
                            break;
                        case 'm':
                            Graph.parseMarker(graph, line, edges, players);
                            break;
                        default:
                            throw new Exception("Invalid file");
                    }
                }
            }

            graph.CalculationsDisabled = false;
            return graph;
        }

        private static void parseVertex(Graph g, string line, ICollection<Vertex> vertices)
        {
            var data = line.Split(' ');
            var v = new Vertex(new PointF(float.Parse(data[1]), float.Parse(data[2])));
            g.AddVertex(v);
            vertices.Add(v);
        }

        private static void parseEdge(Graph g, string line, IList<Vertex> vertices, ICollection<Edge> edges)
        {
            var data = line.Split(' ');
            var e = new Edge(vertices[int.Parse(data[1])], vertices[int.Parse(data[2])]);
            g.AddEdge(e);
            edges.Add(e);
        }

        private static void parseMarker(Graph g, string line, IList<Edge> edges, IList<Player> players)
        {
            var data = line.Split(' ');
            g.AddMarker(players[int.Parse(data[1])], edges[int.Parse(data[2])], float.Parse(data[3]));
        }
    }
}