using System.Drawing;

namespace GraphVoronoi.Graphs
{
    sealed class Marker : IDrawable, IEdgeObject
    {
        private readonly Player player;
        private readonly Edge edge;
        private readonly float t;

        public Edge Edge { get { return this.edge; } }
        public float T { get { return this.t; } }

        public Color Color { get { return this.player.Color; } }
        public double Distance { get { return 0; } }

        public PointF Position
        {
            get
            {
                var diffX = this.edge.To.Position.X - this.edge.From.Position.X;
                var diffY = this.edge.To.Position.Y - this.edge.From.Position.Y;
                return new PointF(this.edge.From.Position.X + this.t * diffX, this.edge.From.Position.Y + this.t * diffY);
            }
        }

        public Marker(Player player, Edge edge, float t)
        {
            this.player = player;
            this.edge = edge;
            this.t = t;
        }

        public bool OnMouseDown(PointF point)
        {
            var dSq = (point.X - this.Position.X) * (point.X - this.Position.X) +
                (point.Y - this.Position.Y) * (point.Y - this.Position.Y);

            return dSq <= Vertex.CollisionRadius * Vertex.CollisionRadius;
        }

        public void Draw(GraphicsHelper graphics)
        {
            graphics.DrawMarker(this.Position, this.Color);
        }
    }
}