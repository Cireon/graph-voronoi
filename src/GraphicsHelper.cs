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
        private readonly Size size;

        public GraphicsHelper(Graphics graphics, Size size)
        {
            this.graphics = graphics;
            this.size = size;
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

        public void DrawScoreBar(int i, Color color, double ratio)
        {
            var pen = new Pen(Color.FromArgb(160, color), 10) { Alignment = PenAlignment.Center };
            var y = this.size.Height - 32 - 5 - i * 15;
            this.graphics.DrawLine(pen, 32, y, 32 + (float)ratio * (this.size.Width - 64), y);
        }
    }
}