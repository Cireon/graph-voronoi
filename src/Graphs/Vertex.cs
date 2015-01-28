using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphVoronoi.Graphs
{
    sealed class Vertex : IDraggable, IHighlightable
    {
        public const float CollisionRadius = 20f;

        public PointF Position { get; private set; }
        private PointF? dragOffset;

        private VertexOwner staticOwner;

        private bool highlighted;

        public VertexOwner Owner { get { return this.staticOwner; } }
        public Color Color { get { return this.Owner == null ? Color.Black : this.Owner.Color; } }

        public readonly LinkedList<Tuple<Vertex, Edge>> AdjacentVertices = new LinkedList<Tuple<Vertex, Edge>>();
        public int Degree { get { return this.AdjacentVertices.Count; } }

        public event VoidEventHandler Changed;

        public Vertex(PointF pos)
        {
            this.Position = pos;
        }

        public void Draw(GraphicsHelper graphics)
        {
            graphics.DrawVertex(this.Position, this.Color, this.highlighted);
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
            return this.AdjacentVertices.Any(t => t.Item1 == other);
        }

        public void ResetStaticOwner()
        {
            this.staticOwner = null;
        }

        public void SetStaticOwner(VertexOwner owner)
        {
            this.staticOwner = owner;
        }

        public void AddAdjacency(Vertex v, Edge e)
        {
            this.AdjacentVertices.AddLast(new Tuple<Vertex, Edge>(v, e));
        }

        public void RemoveAdjacency(Vertex v)
        {
            var ts = this.AdjacentVertices.Where(t => t.Item1 == v).ToList();
            foreach (var t in ts)
                this.AdjacentVertices.Remove(t);
        }

        public void Highlight()
        {
            this.highlighted = true;
        }

        public void UnHighlight()
        {
            this.highlighted = false;
        }
    }
}