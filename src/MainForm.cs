using System.Drawing;
using System.Windows.Forms;
using GraphVoronoi.Graphs;

namespace GraphVoronoi
{
    public partial class MainForm : Form
    {
        private readonly GraphPanel panel;
        private Graph graph;
        private IDraggable currentDraggable;

        public MainForm()
        {
            InitializeComponent();

            this.panel = new GraphPanel
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Size = new Size(this.Width - 200, this.Height),
                Location = new Point(0, 0)
            };
            this.Controls.Add(this.panel);

            this.setGraph();
            this.Resize += (sender, args) => this.onGraphChanged();

            this.panel.MouseDown += onMouseDown;
            this.panel.MouseUp += onMouseUp;
            this.panel.MouseMove += onMouseMove;
        }

        private void onMouseDown(object sender, MouseEventArgs mouseEventArgs)
        {
            if (this.currentDraggable != null)
                this.currentDraggable.OnMouseRelease();
            this.currentDraggable = this.graph.GetVertexAt(mouseEventArgs.Location);
        }

        private void onMouseUp(object sender, MouseEventArgs mouseEventArgs)
        {
            if (this.currentDraggable == null)
                return;

            this.currentDraggable.OnMouseRelease();
            this.currentDraggable = null;
        }

        private void onMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (this.currentDraggable != null)
                this.currentDraggable.OnMouseMove(mouseEventArgs.Location);
        }

        private void setGraph(Graph g = null)
        {
            if (this.graph != null)
                this.graph.Changed -= this.onGraphChanged;

            this.graph = g ?? new Graph();
            this.graph.Changed += this.onGraphChanged;

            this.onGraphChanged();
        }

        private void onGraphChanged()
        {
            this.panel.DrawGraph(this.graph);
        }

        private void newToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.setGraph();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
