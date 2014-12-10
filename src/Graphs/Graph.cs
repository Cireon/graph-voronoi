using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed class Graph
    {
        private readonly LinkedList<Vertex> vertices = new LinkedList<Vertex>();
        private readonly LinkedList<Edge> edges = new LinkedList<Edge>();
        private GhostEdge currentGhostEdge;

        public event VoidEventHandler Changed;

        public Graph() { }

        private void onChange()
        {
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

        public Edge GetEdgeAt(PointF position)
        {
            return this.edges.FirstOrDefault(edge => edge.OnMouseDown(position));
        }

        public void Draw(GraphicsHelper graphics)
        {
            if (this.currentGhostEdge != null)
                this.currentGhostEdge.Draw(graphics);

            foreach (var e in this.edges)
                e.Draw(graphics);
            foreach (var v in this.vertices)
                v.Draw(graphics);
        }
    }
}