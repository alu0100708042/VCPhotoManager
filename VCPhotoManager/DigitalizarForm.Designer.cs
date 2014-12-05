namespace VCPhotoManager
{
    partial class DigitalizarForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rb2x2 = new System.Windows.Forms.RadioButton();
            this.rb3x3 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnDigitalizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnDigitalizar);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.rb3x3);
            this.splitContainer1.Panel2.Controls.Add(this.rb2x2);
            this.splitContainer1.Size = new System.Drawing.Size(287, 129);
            this.splitContainer1.SplitterDistance = 136;
            this.splitContainer1.TabIndex = 0;
            // 
            // rb2x2
            // 
            this.rb2x2.AutoSize = true;
            this.rb2x2.Location = new System.Drawing.Point(18, 44);
            this.rb2x2.Name = "rb2x2";
            this.rb2x2.Size = new System.Drawing.Size(42, 17);
            this.rb2x2.TabIndex = 0;
            this.rb2x2.TabStop = true;
            this.rb2x2.Text = "2x2";
            this.rb2x2.UseVisualStyleBackColor = true;
            // 
            // rb3x3
            // 
            this.rb3x3.AutoSize = true;
            this.rb3x3.Location = new System.Drawing.Point(18, 67);
            this.rb3x3.Name = "rb3x3";
            this.rb3x3.Size = new System.Drawing.Size(42, 17);
            this.rb3x3.TabIndex = 1;
            this.rb3x3.TabStop = true;
            this.rb3x3.Text = "3x3";
            this.rb3x3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Muestrear";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cuantizar";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboBox1.Location = new System.Drawing.Point(16, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(99, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // btnDigitalizar
            // 
            this.btnDigitalizar.Location = new System.Drawing.Point(18, 91);
            this.btnDigitalizar.Name = "btnDigitalizar";
            this.btnDigitalizar.Size = new System.Drawing.Size(75, 23);
            this.btnDigitalizar.TabIndex = 3;
            this.btnDigitalizar.Text = "Ejecutar";
            this.btnDigitalizar.UseVisualStyleBackColor = true;
            this.btnDigitalizar.Click += new System.EventHandler(this.btnDigitalizar_Click);
            // 
            // DigitalizarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 129);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DigitalizarForm";
            this.Text = "DigitalizarForm";
            this.Load += new System.EventHandler(this.DigitalizarForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDigitalizar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb3x3;
        private System.Windows.Forms.RadioButton rb2x2;
    }
}