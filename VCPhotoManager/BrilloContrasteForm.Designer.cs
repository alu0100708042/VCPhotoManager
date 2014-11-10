namespace VCPhotoManager
{
    partial class BrilloContrasteForm
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
            if(disposing && (components != null))
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lbContraste = new System.Windows.Forms.Label();
            this.lbBrillo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trbContraste = new System.Windows.Forms.TrackBar();
            this.trbBrillo = new System.Windows.Forms.TrackBar();
            this.picImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbContraste)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbBrillo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnAceptar);
            this.splitContainer1.Panel1.Controls.Add(this.lbContraste);
            this.splitContainer1.Panel1.Controls.Add(this.lbBrillo);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.trbContraste);
            this.splitContainer1.Panel1.Controls.Add(this.trbBrillo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.picImage);
            this.splitContainer1.Size = new System.Drawing.Size(411, 233);
            this.splitContainer1.SplitterDistance = 137;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(30, 198);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lbContraste
            // 
            this.lbContraste.Location = new System.Drawing.Point(72, 163);
            this.lbContraste.Name = "lbContraste";
            this.lbContraste.Size = new System.Drawing.Size(33, 23);
            this.lbContraste.TabIndex = 5;
            this.lbContraste.Text = "0";
            this.lbContraste.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbBrillo
            // 
            this.lbBrillo.Location = new System.Drawing.Point(12, 163);
            this.lbBrillo.Name = "lbBrillo";
            this.lbBrillo.Size = new System.Drawing.Size(29, 23);
            this.lbBrillo.TabIndex = 4;
            this.lbBrillo.Text = "0";
            this.lbBrillo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contraste";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Brillo";
            // 
            // trbContraste
            // 
            this.trbContraste.Location = new System.Drawing.Point(75, 38);
            this.trbContraste.Maximum = 255;
            this.trbContraste.Name = "trbContraste";
            this.trbContraste.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbContraste.Size = new System.Drawing.Size(45, 104);
            this.trbContraste.TabIndex = 1;
            this.trbContraste.ValueChanged += new System.EventHandler(this.trbContraste_ValueChanged);
            this.trbContraste.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trbContraste_KeyUp);
            this.trbContraste.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbContraste_MouseUp);
            // 
            // trbBrillo
            // 
            this.trbBrillo.Location = new System.Drawing.Point(12, 38);
            this.trbBrillo.Maximum = 255;
            this.trbBrillo.Name = "trbBrillo";
            this.trbBrillo.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbBrillo.Size = new System.Drawing.Size(45, 104);
            this.trbBrillo.TabIndex = 0;
            this.trbBrillo.ValueChanged += new System.EventHandler(this.trbBrillo_ValueChanged);
            this.trbBrillo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trbBrillo_KeyUp);
            this.trbBrillo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbBrillo_MouseUp);
            // 
            // picImage
            // 
            this.picImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picImage.Location = new System.Drawing.Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(270, 233);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            // 
            // BrilloContrasteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 233);
            this.Controls.Add(this.splitContainer1);
            this.Name = "BrilloContrasteForm";
            this.Text = "BrilloContrasteForm";
            this.Load += new System.EventHandler(this.BrilloContrasteForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trbContraste)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbBrillo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lbContraste;
        private System.Windows.Forms.Label lbBrillo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trbContraste;
        private System.Windows.Forms.TrackBar trbBrillo;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Button btnAceptar;
    }
}