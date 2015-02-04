namespace GraphVoronoi
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnModeVertex = new System.Windows.Forms.Button();
            this.btnModeEdge = new System.Windows.Forms.Button();
            this.btnModeMarker = new System.Windows.Forms.Button();
            this.btnColorRed = new System.Windows.Forms.Button();
            this.btnColorBlue = new System.Windows.Forms.Button();
            this.btnColorGreen = new System.Windows.Forms.Button();
            this.btnColorYellow = new System.Windows.Forms.Button();
            this.chkCalculationDisabled = new System.Windows.Forms.CheckBox();
            this.openGraphDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveGraphDialog = new System.Windows.Forms.SaveFileDialog();
            this.panDrawModes = new System.Windows.Forms.Panel();
            this.radDrawWinAreas = new System.Windows.Forms.RadioButton();
            this.radDrawColours = new System.Windows.Forms.RadioButton();
            this.chkDrawCriticalPoints = new System.Windows.Forms.CheckBox();
            this.panel = new GraphVoronoi.GraphPanel();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.panDrawModes.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1264, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btnModeVertex
            // 
            this.btnModeVertex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModeVertex.Enabled = false;
            this.btnModeVertex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModeVertex.Location = new System.Drawing.Point(1058, 48);
            this.btnModeVertex.Name = "btnModeVertex";
            this.btnModeVertex.Size = new System.Drawing.Size(194, 27);
            this.btnModeVertex.TabIndex = 2;
            this.btnModeVertex.Text = "&Vertex Mode";
            this.btnModeVertex.UseVisualStyleBackColor = true;
            this.btnModeVertex.Click += new System.EventHandler(this.btnModeVertex_Click);
            // 
            // btnModeEdge
            // 
            this.btnModeEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModeEdge.Location = new System.Drawing.Point(1058, 81);
            this.btnModeEdge.Name = "btnModeEdge";
            this.btnModeEdge.Size = new System.Drawing.Size(194, 27);
            this.btnModeEdge.TabIndex = 3;
            this.btnModeEdge.Text = "&Edge Mode";
            this.btnModeEdge.UseVisualStyleBackColor = true;
            this.btnModeEdge.Click += new System.EventHandler(this.btnModeEdge_Click);
            // 
            // btnModeMarker
            // 
            this.btnModeMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModeMarker.Location = new System.Drawing.Point(1058, 114);
            this.btnModeMarker.Name = "btnModeMarker";
            this.btnModeMarker.Size = new System.Drawing.Size(194, 27);
            this.btnModeMarker.TabIndex = 0;
            this.btnModeMarker.Text = "&Marker Mode";
            this.btnModeMarker.UseVisualStyleBackColor = true;
            this.btnModeMarker.Click += new System.EventHandler(this.btnModeMarker_Click);
            // 
            // btnColorRed
            // 
            this.btnColorRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorRed.BackColor = System.Drawing.Color.Red;
            this.btnColorRed.Enabled = false;
            this.btnColorRed.Location = new System.Drawing.Point(1068, 147);
            this.btnColorRed.Name = "btnColorRed";
            this.btnColorRed.Size = new System.Drawing.Size(39, 27);
            this.btnColorRed.TabIndex = 4;
            this.btnColorRed.UseVisualStyleBackColor = false;
            this.btnColorRed.Click += new System.EventHandler(this.btnColorRed_Click);
            // 
            // btnColorBlue
            // 
            this.btnColorBlue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorBlue.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnColorBlue.Location = new System.Drawing.Point(1113, 147);
            this.btnColorBlue.Name = "btnColorBlue";
            this.btnColorBlue.Size = new System.Drawing.Size(39, 27);
            this.btnColorBlue.TabIndex = 5;
            this.btnColorBlue.UseVisualStyleBackColor = false;
            this.btnColorBlue.Click += new System.EventHandler(this.btnColorBlue_Click);
            // 
            // btnColorGreen
            // 
            this.btnColorGreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorGreen.BackColor = System.Drawing.Color.Green;
            this.btnColorGreen.Location = new System.Drawing.Point(1158, 147);
            this.btnColorGreen.Name = "btnColorGreen";
            this.btnColorGreen.Size = new System.Drawing.Size(39, 27);
            this.btnColorGreen.TabIndex = 6;
            this.btnColorGreen.UseVisualStyleBackColor = false;
            this.btnColorGreen.Click += new System.EventHandler(this.btnColorGreen_Click);
            // 
            // btnColorYellow
            // 
            this.btnColorYellow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorYellow.BackColor = System.Drawing.Color.Yellow;
            this.btnColorYellow.Location = new System.Drawing.Point(1203, 147);
            this.btnColorYellow.Name = "btnColorYellow";
            this.btnColorYellow.Size = new System.Drawing.Size(39, 27);
            this.btnColorYellow.TabIndex = 7;
            this.btnColorYellow.UseVisualStyleBackColor = false;
            this.btnColorYellow.Click += new System.EventHandler(this.btnColorYellow_Click);
            // 
            // chkCalculationDisabled
            // 
            this.chkCalculationDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCalculationDisabled.AutoSize = true;
            this.chkCalculationDisabled.Location = new System.Drawing.Point(1058, 180);
            this.chkCalculationDisabled.Name = "chkCalculationDisabled";
            this.chkCalculationDisabled.Size = new System.Drawing.Size(160, 17);
            this.chkCalculationDisabled.TabIndex = 8;
            this.chkCalculationDisabled.Text = "&Disable Voronoi Calculations";
            this.chkCalculationDisabled.UseVisualStyleBackColor = true;
            this.chkCalculationDisabled.CheckedChanged += new System.EventHandler(this.chkCalculationDisabled_CheckedChanged);
            // 
            // openGraphDialog
            // 
            this.openGraphDialog.Filter = "Text files|*.txt";
            this.openGraphDialog.Title = "Open Graph";
            this.openGraphDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openGraphDialog_FileOk);
            // 
            // saveGraphDialog
            // 
            this.saveGraphDialog.Filter = "Text files|*.txt";
            this.saveGraphDialog.Title = "Save graph";
            this.saveGraphDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveGraphDialog_FileOk);
            // 
            // panDrawModes
            // 
            this.panDrawModes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panDrawModes.Controls.Add(this.radDrawWinAreas);
            this.panDrawModes.Controls.Add(this.radDrawColours);
            this.panDrawModes.Location = new System.Drawing.Point(1053, 216);
            this.panDrawModes.Name = "panDrawModes";
            this.panDrawModes.Size = new System.Drawing.Size(210, 46);
            this.panDrawModes.TabIndex = 9;
            // 
            // radDrawWinAreas
            // 
            this.radDrawWinAreas.AutoSize = true;
            this.radDrawWinAreas.Location = new System.Drawing.Point(5, 26);
            this.radDrawWinAreas.Name = "radDrawWinAreas";
            this.radDrawWinAreas.Size = new System.Drawing.Size(98, 17);
            this.radDrawWinAreas.TabIndex = 1;
            this.radDrawWinAreas.Text = "Draw &win areas";
            this.radDrawWinAreas.UseVisualStyleBackColor = true;
            this.radDrawWinAreas.CheckedChanged += new System.EventHandler(this.radDrawWinAreas_CheckedChanged);
            // 
            // radDrawColours
            // 
            this.radDrawColours.AutoSize = true;
            this.radDrawColours.Checked = true;
            this.radDrawColours.Location = new System.Drawing.Point(5, 3);
            this.radDrawColours.Name = "radDrawColours";
            this.radDrawColours.Size = new System.Drawing.Size(87, 17);
            this.radDrawColours.TabIndex = 0;
            this.radDrawColours.TabStop = true;
            this.radDrawColours.Text = "Draw &colours";
            this.radDrawColours.UseVisualStyleBackColor = true;
            this.radDrawColours.CheckedChanged += new System.EventHandler(this.radDrawColours_CheckedChanged);
            // 
            // chkDrawCriticalPoints
            // 
            this.chkDrawCriticalPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDrawCriticalPoints.AutoSize = true;
            this.chkDrawCriticalPoints.Checked = true;
            this.chkDrawCriticalPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawCriticalPoints.Location = new System.Drawing.Point(1058, 268);
            this.chkDrawCriticalPoints.Name = "chkDrawCriticalPoints";
            this.chkDrawCriticalPoints.Size = new System.Drawing.Size(115, 17);
            this.chkDrawCriticalPoints.TabIndex = 10;
            this.chkDrawCriticalPoints.Text = "Draw critical &points";
            this.chkDrawCriticalPoints.UseVisualStyleBackColor = true;
            this.chkDrawCriticalPoints.CheckedChanged += new System.EventHandler(this.chkDrawCriticalPoints_CheckedChanged);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Location = new System.Drawing.Point(0, 25);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1052, 656);
            this.panel.TabIndex = 1;
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.generateToolStripMenuItem.Text = "&Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.chkDrawCriticalPoints);
            this.Controls.Add(this.panDrawModes);
            this.Controls.Add(this.chkCalculationDisabled);
            this.Controls.Add(this.btnColorYellow);
            this.Controls.Add(this.btnColorGreen);
            this.Controls.Add(this.btnColorBlue);
            this.Controls.Add(this.btnColorRed);
            this.Controls.Add(this.btnModeMarker);
            this.Controls.Add(this.btnModeEdge);
            this.Controls.Add(this.btnModeVertex);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.panDrawModes.ResumeLayout(false);
            this.panDrawModes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private GraphPanel panel;
        private System.Windows.Forms.Button btnModeVertex;
        private System.Windows.Forms.Button btnModeEdge;
        private System.Windows.Forms.Button btnModeMarker;
        private System.Windows.Forms.Button btnColorRed;
        private System.Windows.Forms.Button btnColorBlue;
        private System.Windows.Forms.Button btnColorGreen;
        private System.Windows.Forms.Button btnColorYellow;
        private System.Windows.Forms.CheckBox chkCalculationDisabled;
        private System.Windows.Forms.OpenFileDialog openGraphDialog;
        private System.Windows.Forms.SaveFileDialog saveGraphDialog;
        private System.Windows.Forms.Panel panDrawModes;
        private System.Windows.Forms.RadioButton radDrawWinAreas;
        private System.Windows.Forms.RadioButton radDrawColours;
        private System.Windows.Forms.CheckBox chkDrawCriticalPoints;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;

    }
}