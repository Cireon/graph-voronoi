using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphVoronoi
{
    sealed class GraphicsHelper
    {
        private const float vertexDrawRadius = 10f;

        private readonly Graphics graphics;

        private readonly Brush vertexBrush;
        private readonly Pen edgePen, ghostPen;

        public GraphicsHelper(Graphics graphics)
        {
            this.graphics = graphics;

            this.vertexBrush = Brushes.Black;
            this.edgePen = new Pen(Color.Black, 6f)
            {
                Alignment = PenAlignment.Center
            };

            this.ghostPen = new Pen(Color.FromArgb(120, Color.Black), 6f)
            {
                Alignment = PenAlignment.Center
            };
        }

        public void DrawVertex(PointF position)
        {
            const float r = GraphicsHelper.vertexDrawRadius;
            this.graphics.FillEllipse(this.vertexBrush, position.X - r, position.Y - r, 2 * r, 2 * r);
        }

        public void DrawEdge(PointF from, PointF to, bool ghost = false)
        {
            var pen = ghost ? this.ghostPen : this.edgePen;

            this.graphics.DrawLine(pen, from, to);
        }
    }
}