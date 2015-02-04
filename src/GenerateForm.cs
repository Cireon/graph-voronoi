using System;
using System.Windows.Forms;
using GraphVoronoi.Graphs;

namespace GraphVoronoi
{
    partial class GenerateForm : Form
    {
        public event VoidEventHandler GenerateButtonClicked;

        public GenerateForm()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (this.GenerateButtonClicked != null)
                this.GenerateButtonClicked();
        }

        public Graph.GenerationParameters GetParameters()
        {
            return new Graph.GenerationParameters
            {
                NVertices = (int)this.nudVertices.Value,
                NEdges = (int)this.nudEdges.Value,
                NSites = (int)this.nudSites.Value,
                GridWidth = (int)this.nudGridX.Value,
                GridHeight = (int)this.nudGridY.Value,
                PerturbationPercentage = .01f * this.tbPerturbation.Value,
                ForcePlanar = this.cbPlanar.Checked
            };
        }
    }
}
