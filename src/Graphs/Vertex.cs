using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed class Vertex : IDraggable, IHighlightable
    {
        public const float CollisionRadius = 20f;

        public PointF Position { get; private set; }
        private PointF? dragOffset;

        private VertexOwner staticOwner;

        private bool highlighted;

        public VertexOwner Owner { get { return this.staticOwner; } }
        public Color Color { get { return this.Owner == null ? Color.Black : this.Owner.Color; } }

        public readonly LinkedList<Tuple<Vertex, Edge>> AdjacentVertices = new LinkedList<Tuple<Vertex, Edge>>();
        public int Degree { get { return this.AdjacentVertices.Count; } }

        public double[] DominatingAreas { get; private set; }

        public event VoidEventHandler Changed;

        public Vertex(PointF pos)
        {
            this.Position = pos;
        }

        public void Draw(GraphicsHelper graphics)
        {
            Color? color = null;
            switch (graphics.Settings.Mode)
            {
                case DrawMode.Colour:
                    color = this.Color;
                    break;
                case DrawMode.WinArea:
                    color = this.DominatingAreas.All(d => d <= this.DominatingAreas[this.DominatingAreas.Length - 1])
                        ? Color.LimeGreen
                        : Color.OrangeRed;
                    break;
            }

            graphics.DrawVertex(this.Position, color, this.highlighted);
        }

        public bool OnMouseDown(PointF point)
        {
            var dSq = (point.X - this.Position.X) * (point.X - this.Position.X) +
                (point.Y - this.Position.Y) * (point.Y - this.Position.Y);

            if (dSq > Vertex.CollisionRadius * Vertex.CollisionRadius)
                return false;

            this.dragOffset = new PointF(this.Position.X - point.X, this.Position.Y - point.Y);
            return true;
        }

        public void OnMouseRelease()
        {
            this.dragOffset = null;
        }

        public void OnMouseMove(PointF newPoint)
        {
            if (this.dragOffset == null)
                return;
            var offset = dragOffset.Value;

            this.Position = new PointF(newPoint.X + offset.X, newPoint.Y + offset.Y);

            if (this.Changed != null)
                this.Changed();
        }

        public bool HasEdgeTo(Vertex other)
        {
            return this.AdjacentVertices.Any(t => t.Item1 == other);
        }

        public void ResetStaticOwner()
        {
            this.staticOwner = null;
        }

        public void SetStaticOwner(VertexOwner owner)
        {
            this.staticOwner = owner;
        }

        public void AddAdjacency(Vertex v, Edge e)
        {
            this.AdjacentVertices.AddLast(new Tuple<Vertex, Edge>(v, e));
        }

        public void RemoveAdjacency(Vertex v)
        {
            var ts = this.AdjacentVertices.Where(t => t.Item1 == v).ToList();
            foreach (var t in ts)
                this.AdjacentVertices.Remove(t);
        }

        public void Highlight()
        {
            this.highlighted = true;
        }

        public void UnHighlight()
        {
            this.highlighted = false;
        }

        public void RecalculateDominatingAreas(Graph g)
        {
            var q = new PriorityQueue<double, Vertex>();

            var vertexDict = g.VertexDictionary;
            var n = vertexDict.Count;

            var ds = new double[n];
            var visited = new bool[n];

            foreach (var pair in vertexDict)
                ds[pair.Value] = pair.Key.Owner != null ? pair.Key.Owner.Distance : double.PositiveInfinity;

            var si = vertexDict[this];

            ds[si] = 0;
            visited[si] = true;
            q.Enqueue(0, this);

            while (q.Count > 0)
            {
                var c = q.DequeueValue();
                var ci = vertexDict[c];

                foreach (var t in c.AdjacentVertices)
                {
                    var u = t.Item1;
                    var e = t.Item2;

                    var ui = vertexDict[u];

                    if (ds[ui] <= ds[ci] + e.Length)
                        continue;

                    ds[ui] = ds[ci] + e.Length;

                    if (visited[ui])
                        q.DecreasePriority(u, ds[ui]);
                    else
                        q.Enqueue(ds[ui], u);

                    visited[ui] = true;
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

                var w = e.Length;

                if (e.From.Owner == null)
                {
                    if (visited[i1])
                        scores[ghostI] += e.Length;
                    continue;
                }
                
                var objs = e.ObjectSet;

                for (int i = 0; i < objs.Count; i++)
                {
                    var prev = i - 1 >= 0 ? objs[i - 1] : null;
                    var curr = objs[i];
                    var next = i < objs.Count - 1 ? objs[i + 1] : null;

                    var t1 = curr.T;
                    var t2 = curr.T;

                    var currD = (prev == null && visited[i1])
                        ? ds[i1]
                        : (next == null && visited[i2] ? ds[i2] : curr.Distance);

                    if (prev != null)
                    {
                        var prevD = (i - 1 == 0) && visited[i1] ? ds[i1] : prev.Distance;

                        t1 = .5f * (prev.T + curr.T) + .5f * (float)((currD - prevD) / w);
                    }
                    if (next != null)
                    {
                        var nextD = (i + 2 == objs.Count && visited[i2] ? ds[i2] : next.Distance);

                        t2 = .5f * (curr.T + next.T) + .5f * (float)((nextD - currD) / w);
                    }

                    var playerIndex = (prev == null && visited[i1]) || (next == null && visited[i2]) ||
                        (curr.Player == null)
                        ? ghostI
                        : playerDict[curr.Player];

                    scores[playerIndex] += (t2 - t1) * w;
                }
            }

            this.DominatingAreas = scores;
        }
    }
}