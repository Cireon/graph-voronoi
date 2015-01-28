using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed partial class Graph
    {
        private readonly Player[] players;
        private readonly LinkedList<Vertex> vertices = new LinkedList<Vertex>();
        private readonly LinkedList<Edge> edges = new LinkedList<Edge>();
        private readonly LinkedList<Marker> markers = new LinkedList<Marker>(); 
        private GhostEdge currentGhostEdge;

        public event VoidEventHandler Changed;

        private bool calculationsDisabled;
        public bool CalculationsDisabled
        {
            get { return this.calculationsDisabled; }
            set
            {
                this.calculationsDisabled = value;
                this.onArithmeticChange();
            }
        }

        public Graph(Player[] players)
        {
            this.players = players;
        }

        public void OnVisualChange()
        {
            if (this.Changed != null)
                this.Changed();
        }

        private void onArithmeticChange()
        {
            this.recalculateDistances();
            this.recalculateCriticalPoints();
            this.OnVisualChange();
        }

        public void AddVertex(Vertex v)
        {
            this.vertices.AddLast(v);
            v.Changed += this.onArithmeticChange;
            this.onArithmeticChange();
        }

        public void AddEdge(Edge e)
        {
            this.edges.AddLast(e);
            this.onArithmeticChange();
        }

        public void AddMarker(Player p, Edge e, float t)
        {
            Marker m;
            this.markers.AddLast(m = new Marker(this, p, e, t));
            e.AddMarker(m);
            m.Changed += this.onArithmeticChange;
            this.onArithmeticChange();
        }

        public void RemoveVertex(Vertex v)
        {
            this.vertices.Remove(v);
            
            while (this.edges.Count > 0 && (this.edges.First.Value.From == v || this.edges.First.Value.To == v))
                this.edges.RemoveFirst();
            var curr = this.edges.First;
            while (curr != null && curr.Next != null)
            {
                if (curr.Next.Value.From == v || curr.Next.Value.To == v)
                    this.edges.Remove(curr.Next);
                else
                    curr = curr.Next;
            }

            v.Changed -= this.onArithmeticChange;
            this.onArithmeticChange();
        }

        public void RemoveEdge(Edge e)
        {
            this.edges.Remove(e);

            while (this.markers.Count > 0 && this.markers.First.Value.Edge == e)
                this.markers.RemoveFirst();
            var curr = this.markers.First;
            while (curr != null && curr.Next != null)
            {
                if (curr.Next.Value.Edge == e)
                    this.markers.Remove(curr.Next);
                else
                    curr = curr.Next;
            }

            e.From.RemoveAdjacency(e.To);
            e.To.RemoveAdjacency(e.From);

            this.onArithmeticChange();
        }

        public void RemoveMarker(Marker m)
        {
            this.markers.Remove(m);
            m.Edge.RemoveMarker(m);
            m.Changed -= this.onArithmeticChange;
            this.onArithmeticChange();
        }

        public void RegisterGhostEdge(GhostEdge edge)
        {
            this.currentGhostEdge = edge;
            this.currentGhostEdge.Changed += this.OnVisualChange;
        }

        public void UnregisterGhostEdge()
        {
            this.currentGhostEdge.Changed -= this.OnVisualChange;
            this.currentGhostEdge = null;
            this.onArithmeticChange();
        }

        public Vertex GetVertexAt(PointF position)
        {
            return this.vertices.FirstOrDefault(vertex => vertex.OnMouseDown(position));
        }

        public Tuple<Edge, float> GetEdgeAt(PointF position)
        {
            foreach (var edge in this.edges)
            {
                float? t;
                if ((t = edge.OnMouseDown(position)) != null)
                {
                    return new Tuple<Edge, float>(edge, t.Value);
                }
            }

            return null;
        }

        public Marker GetMarkerAt(PointF position)
        {
            return this.markers.FirstOrDefault(marker => marker.OnMouseDown(position));
        }

        public CriticalPoint GetCriticalPointAt(PointF position)
        {
            return this.edges.Select(e => e.GetCriticalPointAt(position)).FirstOrDefault(p => p != null);
        }

        public void Draw(GraphicsHelper graphics)
        {
            if (this.currentGhostEdge != null)
                this.currentGhostEdge.Draw(graphics);

            var scores = new PlayerScores(this.players);

            foreach (var e in this.edges)
                e.Draw(graphics, scores);
            foreach (var v in this.vertices)
                v.Draw(graphics);
            foreach (var m in this.markers)
                m.Draw(graphics);

            if (this.CalculationsDisabled)
                return;

            var scoresNorm = scores.GetScoresNormalised();
            if (scoresNorm == null)
                return;

            for (int i = 0; i < this.players.Length; i++)
                graphics.DrawScoreBar(this.players.Length - 1 - i, this.players[i].Color, scoresNorm[this.players[i]]);
        }

        private void recalculateDistances()
        {
            foreach (var vertex in this.vertices)
                vertex.ResetStaticOwner();

            if (this.CalculationsDisabled || this.markers.Count == 0 || this.vertices.Count == 0)
                return;

            var n = this.markers.Count + this.vertices.Count;
            var d = new double[n, n];
            for (int j = 0; j < n; j++)
                for (int i = 0; i < n; i++)
                    d[i, j] = i == j ? 0 : double.PositiveInfinity;

            int index = 0;
            var markerIndices = this.markers.ToDictionary(m => index++, m => m);

            index = 0;
            var markerDict = this.markers.ToDictionary(m => m, m => index++);
            var vertexDict = this.vertices.ToDictionary(v => v, v => index++);

            foreach (var edge in this.edges)
            {
                int i = vertexDict[edge.From];
                int j = vertexDict[edge.To];
                d[i, j] = d[j, i] = edge.Length;
            }

            foreach (var marker in this.markers)
            {
                var edge = marker.Edge;
                var w = edge.Length;

                int i = vertexDict[edge.From];
                int j = vertexDict[edge.To];
                int m = markerDict[marker];

                d[i, m] = d[m, i] = marker.T * w;
                d[j, m] = d[m, j] = (1 - marker.T) * w;
            }

            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (d[i, j] > d[i, k] + d[k, j])
                            d[i, j] = d[i, k] + d[k, j];

            var vi = markers.Count;
            foreach (var v in this.vertices)
            {
                int currIndex = -1;
                double currD = double.PositiveInfinity;

                for (int i = 0; i < this.markers.Count; i++)
                    if (d[vi, i] < currD)
                    {
                        currIndex = i;
                        currD = d[vi, i];
                    }

                if (currIndex >= 0)
                    v.SetStaticOwner(new VertexOwner(markerIndices[currIndex], currD));
                vi++;
            }
        }

        private void recalculateCriticalPoints()
        {
            foreach (var e in this.edges)
                e.ClearCriticalPoints();

            if (this.CalculationsDisabled || this.vertices.Count == 0)
                return;

            var n = this.vertices.Count;
            int index = 0;
            var vertexDict = this.vertices.ToDictionary(v => v, v => index++);

            foreach (var v in this.vertices.Where(v => v.Degree > 2))
            {
                var parents = new int[n];
                var ds = new double[n];
                var visited = new bool[n];

                var si = vertexDict[v];

                for (int i = 0; i < n; i++)
                {
                    ds[i] = i == si ? 0 : double.PositiveInfinity;
                    visited[i] = i == si;
                }

                var q = new PriorityQueue<double, Vertex>(n);
                q.Enqueue(0, v);

                while (!q.IsEmpty)
                {
                    var c = q.DequeueValue();
                    foreach (var t in c.AdjacentVertices)
                    {
                        var u = t.Item1;
                        var e = t.Item2;

                        var ci = vertexDict[c];
                        var ui = vertexDict[u];

                        if (ds[ui] <= ds[ci] + e.Length) continue;

                        ds[ui] = ds[ci] + e.Length;
                        parents[ui] = ci;

                        if (visited[ui])
                            q.DecreasePriority(u, ds[ui]);
                        else
                            q.Enqueue(ds[ui], u);

                        visited[ui] = true;
                    }
                }

                foreach (var e in this.edges.Where(edge =>
                {
                    var i1 = vertexDict[edge.From];
                    var i2 = vertexDict[edge.To];
                    return parents[i1] != i2 && parents[i2] != i1;
                }))
                {
                    var i1 = vertexDict[e.From];
                    var i2 = vertexDict[e.To];
                    var t = .5f + .5f * (float)((ds[i2] - ds[i1]) / e.Length);
                    e.AddCriticalPoint(new CriticalPoint(e, t, v));
                }

                Marker closestM = null;
                double closestD = double.PositiveInfinity;
                Edge closestE = null;
                foreach (var m in this.markers)
                {
                    var i1 = vertexDict[m.Edge.From];
                    var i2 = vertexDict[m.Edge.To];
                    var w = m.Edge.Length;

                    var d = Math.Min(ds[i1] + m.T * w, ds[i2] + (1 - m.T) * w);
                    if (!(d < closestD)) continue;
                    closestM = m;
                    closestD = d;
                    closestE = m.Edge;
                }

                foreach (var e in this.edges.Where(e => e != closestE))
                {
                    var w = e.Length;
                    var i1 = vertexDict[e.From];
                    var i2 = vertexDict[e.To];

                    if (closestD >= ds[i1] && closestD <= ds[i1] + w)
                        e.AddCriticalPoint(new CriticalPoint(e, (float)((closestD - ds[i1]) / w), v, closestM));
                    if (closestD >= ds[i2] && closestD <= ds[i2] + w)
                        e.AddCriticalPoint(new CriticalPoint(e, 1 - (float)((closestD - ds[i2]) / w), v, closestM));
                }
            }
        }

        private static bool between(double value, double m1, double m2)
        {
            if (m1 > m2)
            {
                var tmp = m2;
                m2 = m1;
                m1 = tmp;
            }

            return value >= m1 && value <= m2;
        }
    }
}