using System.IO;
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
            Marker
        }

        private Graph graph;
        private readonly DrawSettings drawSettings;
        private IDraggable currentDraggable;
        private IHighlightable currentlyHighlighted;
        private Mode currentMode = Mode.Vertex;
        private int currentPlayerIndex;
        private Player currentPlayer { get { return this.players[this.currentPlayerIndex]; } }

        private readonly Player[] players;

        public MainForm()
        {
            InitializeComponent();

            this.players = new[]
            {
                new Player(this.btnColorRed.BackColor),
                new Player(this.btnColorBlue.BackColor),
                new Player(this.btnColorGreen.BackColor),
                new Player(this.btnColorYellow.BackColor)
            };
            this.drawSettings = new DrawSettings
            {
                Mode = DrawMode.Colour,
                DrawCriticalPoints = true
            };

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
                        var tuple = this.graph.GetEdgeAt(mouseEventArgs.Location);
                        if (tuple != null)
                            this.graph.RemoveEdge(tuple.Item1);
                    }
                    break;
                case Mode.Marker:
                    var marker = this.graph.GetMarkerAt(mouseEventArgs.Location);

                    if (marker == null && mouseEventArgs.Button == MouseButtons.Left)
                    {
                        var tuple = this.graph.GetEdgeAt(mouseEventArgs.Location);
                        if (tuple != null)
                            this.graph.AddMarker(this.currentPlayer, tuple.Item1, tuple.Item2);
                    }
                    else if (mouseEventArgs.Button == MouseButtons.Left)
                        this.currentDraggable = marker;
                    else if (mouseEventArgs.Button == MouseButtons.Right)
                        if (marker != null)
                            this.graph.RemoveMarker(marker);
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

            var p = this.graph.GetCriticalPointAt(mouseEventArgs.Location);
            if (p == this.currentlyHighlighted) return;

            if (this.currentlyHighlighted != null)
                this.currentlyHighlighted.UnHighlight();
            this.currentlyHighlighted = p;
            if (this.currentlyHighlighted != null)
                this.currentlyHighlighted.Highlight();
            this.graph.OnVisualChange();
        }

        private void setGraph(Graph g = null)
        {
            if (this.graph != null)
                this.graph.Changed -= this.onGraphChanged;

            this.graph = g ?? new Graph(this.players);
            this.graph.Changed += this.onGraphChanged;

            this.currentDraggable = null;

            this.setMode(Mode.Vertex);
            this.setColor(0);

            this.onGraphChanged();
        }

        private void onGraphChanged()
        {
            this.panel.DrawGraph(this.graph, this.drawSettings);
        }

        private void newToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.setGraph();
        }

        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.openGraphDialog.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.saveGraphDialog.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void openGraphDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (File.Exists(this.openGraphDialog.FileName))
                this.setGraph(Graph.FromFile(this.openGraphDialog.FileName, this.players));
        }

        private void saveGraphDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.graph.SaveToFile(this.saveGraphDialog.FileName, this.players);
        }

        private void setMode(Mode mode)
        {
            this.currentMode = mode;

            this.btnModeVertex.Enabled = this.currentMode != Mode.Vertex;
            this.btnModeEdge.Enabled = this.currentMode != Mode.Edge;
            this.btnModeMarker.Enabled = this.currentMode != Mode.Marker;
        }

        private void setColor(int i)
        {
            this.currentPlayerIndex = i;

            this.btnColorRed.Enabled = this.currentPlayerIndex != 0;
            this.btnColorBlue.Enabled = this.currentPlayerIndex != 1;
            this.btnColorGreen.Enabled = this.currentPlayerIndex != 2;
            this.btnColorYellow.Enabled = this.currentPlayerIndex != 3;
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

        private void btnColorRed_Click(object sender, System.EventArgs e)
        {
            this.setColor(0);
        }

        private void btnColorBlue_Click(object sender, System.EventArgs e)
        {
            this.setColor(1);
        }

        private void btnColorGreen_Click(object sender, System.EventArgs e)
        {
            this.setColor(2);
        }

        private void btnColorYellow_Click(object sender, System.EventArgs e)
        {
            this.setColor(3);
        }

        private void chkCalculationDisabled_CheckedChanged(object sender, System.EventArgs e)
        {
            this.graph.CalculationsDisabled = this.chkCalculationDisabled.Checked;
        }

        private void radDrawColours_CheckedChanged(object sender, System.EventArgs e)
        {
            if (radDrawColours.Checked)
                this.drawSettings.Mode = DrawMode.Colour;
            this.graph.OnVisualChange();
        }

        private void radDrawWinAreas_CheckedChanged(object sender, System.EventArgs e)
        {
            if (radDrawWinAreas.Checked)
                this.drawSettings.Mode = DrawMode.WinArea;
            this.graph.OnVisualChange();
        }

        private void chkDrawCriticalPoints_CheckedChanged(object sender, System.EventArgs e)
        {
            this.drawSettings.DrawCriticalPoints = chkDrawCriticalPoints.Checked;
            this.graph.OnVisualChange();
        }
    }
}
