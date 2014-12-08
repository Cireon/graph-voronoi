using System.Drawing;

namespace GraphVoronoi.Graphs
{
    sealed class Vertex : IDraggable
    {
        private const float collisionRadius = 10f;

        public PointF Position { get; private set; }
        private PointF? dragOffset;

        public event VoidEventHandler Changed;

        public Vertex(PointF pos)
        {
            this.Position = pos;
        }

        public void Draw(GraphicsHelper graphics)
        {
            graphics.DrawVertex(this.Position);
        }

        public bool OnMouseDown(PointF point)
        {
            var dSq = (point.X - this.Position.X) * (point.X - this.Position.X) +
                (point.Y - this.Position.Y) * (point.Y - this.Position.Y);

            if (dSq > Vertex.collisionRadius * Vertex.collisionRadius)
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
    }
}