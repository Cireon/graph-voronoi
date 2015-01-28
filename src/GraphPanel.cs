using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GraphVoronoi.Graphs;

namespace GraphVoronoi
{
    sealed class GraphPanel : Panel
    {
        Bitmap image; // buffered image to minimize uneeded drawing
        Bitmap backbuffer; // internal backbuffer to minimize memory usage
        private bool resized;

        public GraphPanel()
        {
            this.Paint += this.onPaint;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint
                        | ControlStyles.UserPaint
                        | ControlStyles.DoubleBuffer
                        | ControlStyles.SupportsTransparentBackColor, true);

            this.onResize(this, EventArgs.Empty);
            this.Resize += this.onResize;
        }

        private void onResize(object sender, EventArgs eventArgs)
        {
            this.image = new Bitmap(this.Width, this.Height);
            this.resized = true;
        }

        private void onPaint(object sender, PaintEventArgs paintEventArgs)
        {
            if (this.DesignMode)
            {
                paintEventArgs.Graphics.Clear(Color.Gray);
            }

            paintEventArgs.Graphics.DrawImage(this.image, new Point(0, 0));
        }

        public void DrawGraph(Graph graph, DrawSettings settings)
        {
            if (this.resized)
            {
                this.backbuffer = null;
            }

            if (this.backbuffer == null)
                this.backbuffer = new Bitmap(this.Width, this.Height);

            var graphics = Graphics.FromImage(this.backbuffer);
            graphics.Clear(Color.Gray);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (graph != null)
                graph.Draw(new GraphicsHelper(graphics, this.Size, settings));

            var tmp = this.image;
            this.image = this.backbuffer;
            this.backbuffer = tmp;

            this.Invalidate();
        }
    }
}