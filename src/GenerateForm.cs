using System;
using System.Windows.Forms;

namespace GraphVoronoi
{
    public partial class GenerateForm : Form
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
    }
}
