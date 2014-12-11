using System.Drawing;

namespace GraphVoronoi.Graphs
{
    sealed class Vertex : IDraggable
    {
        public const float CollisionRadius = 15f;

        public PointF Position { get; private set; }
        private PointF? dragOffset;

        private VertexOwner staticOwner;

        public VertexOwner Owner { get { return this.staticOwner; } }
        public Color Color { get { return this.Owner == null ? Color.Black : this.Owner.Color; } }

        public event VoidEventHandler Changed;

        public Vertex(PointF pos)
        {
            this.Position = pos;
        }

        public void Draw(GraphicsHelper graphics)
        {
            graphics.DrawVertex(this.Position, this.Color);
        }

        public bool OnMouseDown(PointF point)
        {
            var dSq = (point.X - this.Position.X) * (point.X - this.Position.X) +
                (point.Y - this.Position.Y) * (point.Y - this.Position.Y);

            if (dSq > Vertex.CollisionRadius * Vertex.CollisionRadius)
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

        public bool HasEdgeTo(Vertex other)
        {
            // todo: implement
            return false;
        }

        public void ResetStaticOwner()
        {
            this.staticOwner = null;
        }

        public void SetStaticOwner(VertexOwner owner)
        {
            this.staticOwner = owner;
        }
    }
}