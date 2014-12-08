using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed class Graph
    {
        private readonly LinkedList<Vertex> vertices = new LinkedList<Vertex>();
        private readonly LinkedList<Edge> edges = new LinkedList<Edge>();

        public event VoidEventHandler Changed;

        public Graph()
        {
            Vertex v1, v2, v3, v4;

            this.AddVertex(v1 = new Vertex(new PointF(100, 100)));
            this.AddVertex(v2 = new Vertex(new PointF(200, 200)));
            this.AddVertex(v3 = new Vertex(new PointF(300, 100)));
            this.AddVertex(v4 = new Vertex(new PointF(200, 400)));

            this.edges.AddLast(new Edge(v1, v2));
            this.edges.AddLast(new Edge(v2, v3));
            this.edges.AddLast(new Edge(v3, v1));
            this.edges.AddLast(new Edge(v2, v4));
        }

        private void onChange()
        {
            if (this.Changed != null)
                this.Changed();
        }

        public void AddVertex(Vertex v)
        {
            this.vertices.AddLast(v);
            v.Changed += this.onChange;
        }

        public void RemoveVertex(Vertex v)
        {
            this.vertices.Remove(v);
            
            while (this.edges.First.Value.From == v || this.edges.First.Value.To == v)
                this.edges.RemoveFirst();
            var curr = this.edges.First;
            while (curr.Next != null)
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

        public IDraggable GetVertexAt(PointF position)
        {
            return this.vertices.FirstOrDefault(vertex => vertex.OnMouseDown(position));
        }

        public void Draw(GraphicsHelper graphics)
        {
            foreach (var e in this.edges)
                e.Draw(graphics);
            foreach (var v in this.vertices)
                v.Draw(graphics);
        }
    }
}