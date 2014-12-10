using System;
using System.Drawing;

namespace GraphVoronoi.Graphs
{
    sealed class Edge : IMouseInputReceiver, IDrawable
    {
        private const float collisionThickness = 20f;

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
            var x1 = this.from.Position.X;
            var y1 = this.from.Position.Y;
            var x2 = this.to.Position.X;
            var y2 = this.to.Position.Y;

            var width = this.Length;
            const double halfHeight = .5 * Edge.collisionThickness;

            var edgeAngle = Math.Atan2(y2 - y1, x2 - x1);

            var diffX = position.X - x1;
            var diffY = position.Y - y1;

            var pA = Math.Atan2(diffY, diffX);
            var pR = Math.Sqrt(diffX * diffX + diffY * diffY);

            var pX = x1 + pR * Math.Cos(pA - edgeAngle);
            var pY = y1 + pR * Math.Sin(pA - edgeAngle);

            return (pX >= x1 && pX <= x1 + width && pY >= y1 - .5 * halfHeight && pY <= y1 + .5 * halfHeight);
        }
    }
}