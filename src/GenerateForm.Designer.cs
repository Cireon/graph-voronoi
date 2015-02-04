namespace GraphVoronoi
{
    partial class GenerateForm
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblVertices = new System.Windows.Forms.Label();
            this.nudVertices = new System.Windows.Forms.NumericUpDown();
            this.lblEdges = new System.Windows.Forms.Label();
            this.nudEdges = new System.Windows.Forms.NumericUpDown();
            this.lblSites = new System.Windows.Forms.Label();
            this.nudSites = new System.Windows.Forms.NumericUpDown();
            this.lblGrid = new System.Windows.Forms.Label();
            this.nudGridX = new System.Windows.Forms.NumericUpDown();
            this.nudGridY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPerturbation = new System.Windows.Forms.TrackBar();
            this.cbPlanar = new System.Windows.Forms.CheckBox();
            this.lblPlanar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudVertices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSites)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPerturbation)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(12, 187);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(219, 30);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblVertices
            // 
            this.lblVertices.AutoSize = true;
            this.lblVertices.Location = new System.Drawing.Point(12, 9);
            this.lblVertices.Name = "lblVertices";
            this.lblVertices.Size = new System.Drawing.Size(57, 13);
            this.lblVertices.TabIndex = 1;
            this.lblVertices.Text = "# vertices:";
            // 
            // nudVertices
            // 
            this.nudVertices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudVertices.Location = new System.Drawing.Point(177, 7);
            this.nudVertices.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVertices.Name = "nudVertices";
            this.nudVertices.Size = new System.Drawing.Size(54, 20);
            this.nudVertices.TabIndex = 2;
            this.nudVertices.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblEdges
            // 
            this.lblEdges.AutoSize = true;
            this.lblEdges.Location = new System.Drawing.Point(12, 35);
            this.lblEdges.Name = "lblEdges";
            this.lblEdges.Size = new System.Drawing.Size(49, 13);
            this.lblEdges.TabIndex = 3;
            this.lblEdges.Text = "# edges:";
            // 
            // nudEdges
            // 
            this.nudEdges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudEdges.Location = new System.Drawing.Point(177, 33);
            this.nudEdges.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudEdges.Name = "nudEdges";
            this.nudEdges.Size = new System.Drawing.Size(54, 20);
            this.nudEdges.TabIndex = 4;
            this.nudEdges.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // lblSites
            // 
            this.lblSites.AutoSize = true;
            this.lblSites.Location = new System.Drawing.Point(12, 61);
            this.lblSites.Name = "lblSites";
            this.lblSites.Size = new System.Drawing.Size(41, 13);
            this.lblSites.TabIndex = 5;
            this.lblSites.Text = "# sites:";
            // 
            // nudSites
            // 
            this.nudSites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSites.Location = new System.Drawing.Point(177, 59);
            this.nudSites.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudSites.Name = "nudSites";
            this.nudSites.Size = new System.Drawing.Size(54, 20);
            this.nudSites.TabIndex = 6;
            this.nudSites.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblGrid
            // 
            this.lblGrid.AutoSize = true;
            this.lblGrid.Location = new System.Drawing.Point(12, 87);
            this.lblGrid.Name = "lblGrid";
            this.lblGrid.Size = new System.Drawing.Size(27, 13);
            this.lblGrid.TabIndex = 7;
            this.lblGrid.Text = "grid:";
            // 
            // nudGridX
            // 
            this.nudGridX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudGridX.Location = new System.Drawing.Point(117, 85);
            this.nudGridX.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudGridX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGridX.Name = "nudGridX";
            this.nudGridX.Size = new System.Drawing.Size(54, 20);
            this.nudGridX.TabIndex = 8;
            this.nudGridX.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // nudGridY
            // 
            this.nudGridY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudGridY.Location = new System.Drawing.Point(177, 85);
            this.nudGridY.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudGridY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGridY.Name = "nudGridY";
            this.nudGridY.Size = new System.Drawing.Size(54, 20);
            this.nudGridY.TabIndex = 9;
            this.nudGridY.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "grid perturbation:";
            // 
            // tbPerturbation
            // 
            this.tbPerturbation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPerturbation.LargeChange = 10;
            this.tbPerturbation.Location = new System.Drawing.Point(117, 111);
            this.tbPerturbation.Maximum = 50;
            this.tbPerturbation.Name = "tbPerturbation";
            this.tbPerturbation.Size = new System.Drawing.Size(114, 45);
            this.tbPerturbation.TabIndex = 11;
            this.tbPerturbation.TickFrequency = 10;
            this.tbPerturbation.Value = 25;
            // 
            // cbPlanar
            // 
            this.cbPlanar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPlanar.AutoSize = true;
            this.cbPlanar.Checked = true;
            this.cbPlanar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPlanar.Location = new System.Drawing.Point(216, 142);
            this.cbPlanar.Name = "cbPlanar";
            this.cbPlanar.Size = new System.Drawing.Size(15, 14);
            this.cbPlanar.TabIndex = 12;
            this.cbPlanar.UseVisualStyleBackColor = true;
            // 
            // lblPlanar
            // 
            this.lblPlanar.AutoSize = true;
            this.lblPlanar.Location = new System.Drawing.Point(12, 142);
            this.lblPlanar.Name = "lblPlanar";
            this.lblPlanar.Size = new System.Drawing.Size(66, 13);
            this.lblPlanar.TabIndex = 13;
            this.lblPlanar.Text = "force planar:";
            // 
            // GenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 229);
            this.Controls.Add(this.lblPlanar);
            this.Controls.Add(this.cbPlanar);
            this.Controls.Add(this.tbPerturbation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudGridY);
            this.Controls.Add(this.nudGridX);
            this.Controls.Add(this.lblGrid);
            this.Controls.Add(this.nudSites);
            this.Controls.Add(this.lblSites);
            this.Controls.Add(this.nudEdges);
            this.Controls.Add(this.lblEdges);
            this.Controls.Add(this.nudVertices);
            this.Controls.Add(this.lblVertices);
            this.Controls.Add(this.btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerateForm";
            this.Text = "Generate Graph";
            ((System.ComponentModel.ISupportInitialize)(this.nudVertices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEdges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSites)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPerturbation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblVertices;
        private System.Windows.Forms.NumericUpDown nudVertices;
        private System.Windows.Forms.Label lblEdges;
        private System.Windows.Forms.NumericUpDown nudEdges;
        private System.Windows.Forms.Label lblSites;
        private System.Windows.Forms.NumericUpDown nudSites;
        private System.Windows.Forms.Label lblGrid;
        private System.Windows.Forms.NumericUpDown nudGridX;
        private System.Windows.Forms.NumericUpDown nudGridY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbPerturbation;
        private System.Windows.Forms.CheckBox cbPlanar;
        private System.Windows.Forms.Label lblPlanar;
    }
}