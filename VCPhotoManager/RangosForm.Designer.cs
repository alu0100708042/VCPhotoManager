namespace VCPhotoManager
{
    partial class RangosForm
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
            this.dgvTramos = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuntoX1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuntoY1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuntoX2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuntoY2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddTramo = new System.Windows.Forms.Button();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTramos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTramos
            // 
            this.dgvTramos.AllowUserToAddRows = false;
            this.dgvTramos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTramos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTramos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.PuntoX1,
            this.PuntoY1,
            this.PuntoX2,
            this.PuntoY2});
            this.dgvTramos.Location = new System.Drawing.Point(12, 118);
            this.dgvTramos.Name = "dgvTramos";
            this.dgvTramos.ReadOnly = true;
            this.dgvTramos.Size = new System.Drawing.Size(639, 178);
            this.dgvTramos.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // PuntoX1
            // 
            this.PuntoX1.HeaderText = "Punto X1";
            this.PuntoX1.Name = "PuntoX1";
            this.PuntoX1.ReadOnly = true;
            // 
            // PuntoY1
            // 
            this.PuntoY1.HeaderText = "Punto Y1";
            this.PuntoY1.Name = "PuntoY1";
            this.PuntoY1.ReadOnly = true;
            // 
            // PuntoX2
            // 
            this.PuntoX2.HeaderText = "Punto X2";
            this.PuntoX2.Name = "PuntoX2";
            this.PuntoX2.ReadOnly = true;
            // 
            // PuntoY2
            // 
            this.PuntoY2.HeaderText = "Punto Y2";
            this.PuntoY2.Name = "PuntoY2";
            this.PuntoY2.ReadOnly = true;
            // 
            // btnAddTramo
            // 
            this.btnAddTramo.Location = new System.Drawing.Point(576, 80);
            this.btnAddTramo.Name = "btnAddTramo";
            this.btnAddTramo.Size = new System.Drawing.Size(75, 23);
            this.btnAddTramo.TabIndex = 1;
            this.btnAddTramo.Text = "Añadir Punto";
            this.btnAddTramo.UseVisualStyleBackColor = true;
            this.btnAddTramo.Click += new System.EventHandler(this.btnAddTramo_Click);
            // 
            // txtX1
            // 
            this.txtX1.Location = new System.Drawing.Point(29, 43);
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(100, 20);
            this.txtX1.TabIndex = 2;
            this.txtX1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ComprobarRangos);
            // 
            // txtY1
            // 
            this.txtY1.Location = new System.Drawing.Point(135, 43);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(100, 20);
            this.txtY1.TabIndex = 3;
            this.txtY1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ComprobarRangos);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Location = new System.Drawing.Point(576, 302);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 4;
            this.btnExecute.Text = "Ejecutar";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // txtY2
            // 
            this.txtY2.Location = new System.Drawing.Point(137, 43);
            this.txtY2.Name = "txtY2";
            this.txtY2.Size = new System.Drawing.Size(100, 20);
            this.txtY2.TabIndex = 6;
            this.txtY2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ComprobarRangos);
            // 
            // txtX2
            // 
            this.txtX2.Location = new System.Drawing.Point(31, 43);
            this.txtX2.Name = "txtX2";
            this.txtX2.Size = new System.Drawing.Size(100, 20);
            this.txtX2.TabIndex = 5;
            this.txtX2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ComprobarRangos);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Y";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtX1);
            this.groupBox1.Controls.Add(this.txtY1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 91);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Punto 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtX2);
            this.groupBox2.Controls.Add(this.txtY2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(293, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 91);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Punto 2";
            // 
            // RangosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 335);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnAddTramo);
            this.Controls.Add(this.dgvTramos);
            this.Name = "RangosForm";
            this.Text = "Transformaciones Lineales - Tramos";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgvTramos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTramos;
        private System.Windows.Forms.Button btnAddTramo;
        private System.Windows.Forms.TextBox txtX1;
        private System.Windows.Forms.TextBox txtY1;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuntoX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuntoY1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuntoX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuntoY2;
        private System.Windows.Forms.TextBox txtY2;
        private System.Windows.Forms.TextBox txtX2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}