using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed class CriticalPoint : IEdgeObject, IHighlightable
    {
        public enum Type
        {
            MarkerEqualDistance,
            PathEqualLength
        }

        public enum Direction
        {
            None,
            Increasing,
            Decreasing
        }

        private readonly Edge edge;
        private readonly IHighlightable[] relatedObjects;
        public float T { get; private set; }

        public double[] DominatingAreasFromBelow { get; private set; }
        public double[] DominatingAreasFromAbove { get; private set; }

        public CriticalPoint(Graph g, Type type, Direction dir, Edge e, float t, params IHighlightable[] relatedObjects)
        {
            this.edge = e;
            this.T = t;
            this.relatedObjects = relatedObjects;
            
            this.calculateDominatingArea(g, type, dir);
        }

        #region Dominating Area calculation
        private void calculateDominatingArea(Graph g, Type type, Direction dir)
        {
            switch (type)
            {
                case Type.MarkerEqualDistance:
                    var v = (Vertex)this.relatedObjects.FirstOrDefault(o => o is Vertex);

                    var withVertex = this.getDominatingAreasWithVertex(g, v);
                    var withoutVertex = this.getDominatingAreasWithoutVertex(g, v);

                    switch (dir)
                    {
                        case Direction.Increasing:
                            this.DominatingAreasFromBelow = withVertex;
                            this.DominatingAreasFromAbove = withoutVertex;
                            break;
                        case Direction.Decreasing:
                            this.DominatingAreasFromBelow = withoutVertex;
                            this.DominatingAreasFromAbove = withVertex;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("dir");
                    }
                    break;
                case Type.PathEqualLength:
                    this.DominatingAreasFromAbove = this.DominatingAreasFromBelow = this.getDominatingAreas(g);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        private double[] getDominatingAreas(Graph g, Vertex alwaysTake = null, Vertex neverTake = null)
        {
            var w = this.edge.Length;
            var q = new PriorityQueue<double, Vertex>();

            var vertexDict = g.VertexDictionary;
            var n = vertexDict.Count;

            var ds = new double[n];
            var visited = new bool[n];

            foreach (var pair in vertexDict)
                ds[pair.Value] = pair.Key.Owner != null ? pair.Key.Owner.Distance : double.PositiveInfinity;

            var si1 = vertexDict[this.edge.From];
            var si2 = vertexDict[this.edge.To];

            if (this.T * w < ds[si1])
            {
                ds[si1] = this.T * w;
                visited[si1] = true;
                q.Enqueue(ds[si1], this.edge.From);
            }

            if ((1 - this.T) * w < ds[si2])
            {
                ds[si2] = (1 - this.T) * w;
                visited[si2] = true;
                q.Enqueue(ds[si2], this.edge.To);
            }

            var edgeHs = new HashSet<Edge> { this.edge };
            var edgeList = new LinkedList<Edge>();
            edgeList.AddLast(this.edge);

            while (q.Count > 0)
            {
                var c = q.DequeueValue();
                var ci = vertexDict[c];

                foreach (var t in c.AdjacentVertices)
                {
                    var u = t.Item1;
                    var e = t.Item2;

                    var ui = vertexDict[u];

                    var newD = ds[ci] + e.Length;

                    if ((u != alwaysTake || visited[ui]) && (ds[ui] <= newD || u == neverTake))
                        continue;

                    ds[ui] = newD;

                    if (visited[ui])
                        q.DecreasePriority(u, ds[ui]);
                    else
                        q.Enqueue(ds[ui], u);

                    visited[ui] = true;
                    if (!edgeHs.Contains(e))
                    {
                        edgeHs.Add(e);
                        edgeList.AddLast(e);
                    }
                }
            }


            var playerDict = g.PlayerDictionary;
            var edges = g.Edges;
            var scores = new double[playerDict.Count + 1];

            var ghostI = playerDict.Count;

            foreach (var e in edges)
            {
                var i1 = vertexDict[e.From];
                var i2 = vertexDict[e.To];

                if (e.From.Owner == null)
                {
                    if (visited[i1])
                        scores[ghostI] += e.Length;
                    continue;
                }
                
                var objs = e.ObjectSet;

                if (e == this.edge)
                {
                    // add current critical point as marker
                    objs.Add(null);
                    for (int i = objs.Count - 2; i >= 0; i--)
                    {
                        if (objs[i].T > this.T)
                            objs[i + 1] = objs[i];
                        else
                        {
                            objs[i + 1] = new Marker(g, null, this.edge, this.T);
                            break;
                        }
                    }
                }

                for (int i = 0; i < objs.Count; i++)
                {
                    var prev = i - 1 >= 0 ? objs[i - 1] : null;
                    var curr = objs[i];
                    var next = i < objs.Count - 1 ? objs[i + 1] : null;

                    var t1 = curr.T;
                    var t2 = curr.T;

                    if (prev != null)
                    {
                        t1 = .5f * (prev.T + curr.T) + .5f * (float)((curr.Distance - prev.Distance) / w);
                    }
                    if (next != null)
                    {
                        t2 = .5f * (curr.T + next.T) + .5f * (float)((next.Distance - curr.Distance) / w);
                    }

                    var playerIndex = (i == 0 && visited[i1]) || (i == objs.Count - 1 && visited[i2]) ||
                        (curr.Player == null)
                        ? ghostI
                        : playerDict[curr.Player];

                    scores[playerIndex] += (t2 - t1) * w;
                }
            }

            return scores;
        }

        private double[] getDominatingAreasWithVertex(Graph g, Vertex v)
        {
            return this.getDominatingAreas(g, v);
        }

        private double[] getDominatingAreasWithoutVertex(Graph g, Vertex v)
        {
            return this.getDominatingAreas(g, neverTake: v);
        }
        #endregion

        public void Highlight()
        {
            foreach (var obj in this.relatedObjects)
                obj.Highlight();
        }

        public void UnHighlight()
        {
            foreach (var obj in this.relatedObjects)
                obj.UnHighlight();
        }
    }
}