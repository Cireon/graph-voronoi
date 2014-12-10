using System.Windows.Forms;
using GraphVoronoi.Graphs;

namespace GraphVoronoi
{
    public partial class MainForm : Form
    {
        private enum Mode
        {
            Vertex,
            Edge,
            Marker,
            Hover
        }

        private Graph graph;
        private IDraggable currentDraggable;
        private Mode currentMode = Mode.Vertex;

        public MainForm()
        {
            InitializeComponent();

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

            switch (this.currentMode)
            {
                case Mode.Vertex:
                    var vertex = this.graph.GetVertexAt(mouseEventArgs.Location);

                    if (vertex == null && mouseEventArgs.Button == MouseButtons.Left)
                        this.graph.AddVertex(new Vertex(mouseEventArgs.Location));
                    else if (mouseEventArgs.Button == MouseButtons.Left)
                        this.currentDraggable = vertex;
                    else if (mouseEventArgs.Button == MouseButtons.Right)
                        if (vertex != null)
                            this.graph.RemoveVertex(vertex);

                    break;
                case Mode.Edge:
                    if (mouseEventArgs.Button == MouseButtons.Left)
                    {
                        var originVertex = this.graph.GetVertexAt(mouseEventArgs.Location);
                        if (originVertex != null)
                            this.currentDraggable = new GhostEdge(this.graph, originVertex);
                    }
                    else if (mouseEventArgs.Button == MouseButtons.Right)
                    {
                        var edge = this.graph.GetEdgeAt(mouseEventArgs.Location);
                        if (edge != null)
                            this.graph.RemoveEdge(edge);
                    }
                    break;
            }
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

        private void setMode(Mode mode)
        {
            this.currentMode = mode;

            this.btnModeVertex.Enabled = this.currentMode != Mode.Vertex;
            this.btnModeEdge.Enabled = this.currentMode != Mode.Edge;
            this.btnModeMarker.Enabled = this.currentMode != Mode.Marker;
            this.btnModeHover.Enabled = this.currentMode != Mode.Hover;
        }

        private void btnModeVertex_Click(object sender, System.EventArgs e)
        {
            this.setMode(Mode.Vertex);
        }

        private void btnModeEdge_Click(object sender, System.EventArgs e)
        {
            this.setMode(Mode.Edge);
        }

        private void btnModeMarker_Click(object sender, System.EventArgs e)
        {
            this.setMode(Mode.Marker);
        }

        private void btnModeHover_Click(object sender, System.EventArgs e)
        {
            this.setMode(Mode.Hover);
        }
    }
}
