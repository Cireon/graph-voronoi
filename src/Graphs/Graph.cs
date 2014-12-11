using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed partial class Graph
    {
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
                this.onChange();
            }
        }

        private void onChange()
        {
            this.recalculateDistances();
            if (this.Changed != null)
                this.Changed();
        }

        public void AddVertex(Vertex v)
        {
            this.vertices.AddLast(v);
            v.Changed += this.onChange;
            this.onChange();
        }

        public void AddEdge(Edge e)
        {
            this.edges.AddLast(e);
            this.onChange();
        }

        public void AddMarker(Player p, Edge e, float t)
        {
            Marker m;
            this.markers.AddLast(m = new Marker(this, p, e, t));
            e.AddMarker(m);
            m.Changed += this.onChange;
            this.onChange();
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

            v.Changed -= this.onChange;
            this.onChange();
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

            this.onChange();
        }

        public void RemoveMarker(Marker m)
        {
            this.markers.Remove(m);
            m.Edge.RemoveMarker(m);
            m.Changed -= this.onChange;
            this.onChange();
        }

        public void RegisterGhostEdge(GhostEdge edge)
        {
            this.currentGhostEdge = edge;
            this.currentGhostEdge.Changed += this.onChange;
        }

        public void UnregisterGhostEdge()
        {
            this.currentGhostEdge.Changed -= this.onChange;
            this.currentGhostEdge = null;
            this.onChange();
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

        public void Draw(GraphicsHelper graphics)
        {
            if (this.currentGhostEdge != null)
                this.currentGhostEdge.Draw(graphics);

            foreach (var e in this.edges)
                e.Draw(graphics);
            foreach (var v in this.vertices)
                v.Draw(graphics);
            foreach (var m in this.markers)
                m.Draw(graphics);
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
    }
}