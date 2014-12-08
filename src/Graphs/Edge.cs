using System;
using System.Drawing;

namespace GraphVoronoi.Graphs
{
    sealed class Edge : IMouseInputReceiver
    {
        private const float collisionThickness = .1f;

        private readonly Vertex from, to;

        public Vertex From { get { return this.from; } }
        public Vertex To { get { return this.to; } }

        public double Length
        {
            get
            {
                var p1 = this.from.Position;
                var p2 = this.to.Position;
                return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
            }
        }

        public Edge(Vertex from, Vertex to)
        {
            this.from = from;
            this.to = to;
        }

        public void Draw(GraphicsHelper graphics)
        {
            graphics.DrawEdge(this.from.Position, this.to.Position);
        }

        public bool OnMouseDown(PointF position)
        {
            return false;
        }
    }
}