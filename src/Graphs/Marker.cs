using System.Drawing;

namespace GraphVoronoi.Graphs
{
    sealed class Marker : IEdgeObject, IDraggable
    {
        private readonly Graph graph;
        private readonly Player player;

        public Player Player { get { return this.player; } }
        public Edge Edge { get; private set; }
        public float T { get; private set; }

        public Color Color { get { return this.player.Color; } }
        public double Distance { get { return 0; } }

        public event VoidEventHandler Changed;

        private PointF position
        {
            get
            {
                var diffX = this.Edge.To.Position.X - this.Edge.From.Position.X;
                var diffY = this.Edge.To.Position.Y - this.Edge.From.Position.Y;
                return new PointF(this.Edge.From.Position.X + this.T * diffX, this.Edge.From.Position.Y + this.T * diffY);
            }
        }

        public Marker(Graph graph, Player player, Edge edge, float t)
        {
            this.graph = graph;
            this.player = player;
            this.Edge = edge;
            this.T = t;
        }

        public bool OnMouseDown(PointF point)
        {
            var dSq = (point.X - this.position.X) * (point.X - this.position.X) +
                (point.Y - this.position.Y) * (point.Y - this.position.Y);

            return dSq <= Vertex.CollisionRadius * Vertex.CollisionRadius;
        }

        public void Draw(GraphicsHelper graphics)
        {
            graphics.DrawMarker(this.position, this.Color);
        }

        public void OnMouseMove(PointF newPosition)
        {
            Edge newEdge;
            float newT;

            var tuple = this.graph.GetEdgeAt(newPosition);
            if (tuple == null)
            {
                newEdge = this.Edge;
                newT = this.Edge.ProjectPoint(newPosition).GetValueOrDefault(0);
            }
            else
            {
                newEdge = tuple.Item1;
                newT = tuple.Item2;
            }

            if (newT < 0.01)
                newT = 0.01f;
            if (newT > 0.99)
                newT = 0.99f;

            this.Edge.RemoveMarker(this);
            this.Edge = newEdge;
            this.T = newT;
            this.Edge.AddMarker(this);

            if (this.Changed != null)
                this.Changed();
        }

        public void OnMouseRelease() { }
    }
}