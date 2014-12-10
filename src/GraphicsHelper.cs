using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphVoronoi
{
    sealed class GraphicsHelper
    {
        private const float vertexOuterDrawRadius = 10f;
        private const float vertexInnerDrawRadius = 8f;
        private const float edgeOuterThickness = 8f;
        private const float edgeInnerThickness = 4f;
        private const int ghostAlpha = 160;

        private readonly Graphics graphics;

        public GraphicsHelper(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public void DrawVertex(PointF position, Color? color)
        {
            const float r = GraphicsHelper.vertexOuterDrawRadius;

            var brush = new SolidBrush(Color.Black);
            this.graphics.FillEllipse(brush, position.X - r, position.Y - r, 2 * r, 2 * r);

            if (color.HasValue)
                this.DrawMarker(position, color.Value);
        }

        public void DrawMarker(PointF position, Color color)
        {
            const float r = GraphicsHelper.vertexInnerDrawRadius;

            var brush = new SolidBrush(color);
            this.graphics.FillEllipse(brush, position.X - r, position.Y - r, 2 * r, 2 * r);
        }

        public void DrawEdge(PointF from, PointF to, bool ghost = false)
        {
            var pen = new Pen(Color.FromArgb(ghost ? GraphicsHelper.ghostAlpha : 255, Color.Black), GraphicsHelper.edgeOuterThickness)
            {
                Alignment = PenAlignment.Center
            };

            this.graphics.DrawLine(pen, from, to);
        }

        public void DrawEdgeSegment(PointF from, PointF to, Color color, bool ghost = false)
        {
            var pen = new Pen(Color.FromArgb(ghost ? GraphicsHelper.ghostAlpha : 255, color), GraphicsHelper.edgeInnerThickness)
            {
                Alignment = PenAlignment.Center
            };

            this.graphics.DrawLine(pen, from, to);
        }
    }
}