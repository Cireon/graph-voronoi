using System.Drawing;

namespace GraphVoronoi.Graphs
{
    sealed class GhostEdge : IDraggable
    {
        private readonly Graph graph;
        private readonly Vertex origin;
        private PointF position;

        public event VoidEventHandler Changed;

        public GhostEdge(Graph graph, Vertex origin)
        {
            this.graph = graph;
            this.origin = origin;
            graph.RegisterGhostEdge(this);
        }

        public void Draw(GraphicsHelper graphics)
        {
            graphics.DrawEdge(origin.Position, this.position, true);
        }

        public void OnMouseMove(PointF newPosition)
        {
            this.position = newPosition;
            if (this.Changed != null)
                this.Changed();
        }

        public void OnMouseRelease()
        {
            this.graph.UnregisterGhostEdge();

            var vertex = this.graph.GetVertexAt(this.position);
            if (vertex == null || vertex.HasEdgeTo(this.origin))
                return;
            
            this.graph.AddEdge(new Edge(this.origin, vertex));
        }
    }
}