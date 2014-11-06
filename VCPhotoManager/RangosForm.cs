using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VCPhotoManager.Clases;

namespace VCPhotoManager
{
    public partial class RangosForm : Form
    {
        MainForm m_mForm;
        Bitmap m_Image;
        Manager m_Manager;

        public RangosForm(Bitmap Image, MainForm main)
        {
            InitializeComponent();
            m_Image = Image;
            m_mForm = main;
            m_Manager = new Manager();
        }

        private void btnAddTramo_Click(object sender, EventArgs e)
        {
            Int32 x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            try
            {
                x1 = Int32.Parse(this.txtX1.Text);
                x2 = Int32.Parse(this.txtX2.Text);
                y1 = Int32.Parse(this.txtY1.Text);
                y2 = Int32.Parse(this.txtY2.Text);
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Todos los campos deben estan rellenos.", "Transformaciones lineales.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(FormatException)
            {
                MessageBox.Show("El formato de entrada.", "Transformaciones lineales.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OverflowException){
                MessageBox.Show("Se ha sobrepasado la capacidad de procesamiento.", "Transformaciones lineales.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           

            if((x1 >= 0 && x1 <= 255) && (x2 >= 0 && x2 <= 255) && (x2 > x1) && (y1 >= 0 && y1 <= 255) && (y2 >= 0 && y2 <= 255))
            {
                DataGridViewRow row = new DataGridViewRow();
                this.dgvTramos.Rows.Add(new Object[] { this.dgvTramos.Rows.Count + 1, x1, y1, x2, y2 });
                this.txtX1.Clear();
                this.txtX2.Clear();
                this.txtY1.Clear();
                this.txtY2.Clear();
            }
            else
            {
                MessageBox.Show("Debe seleccionar valores entres 0 y 255 ambos inclusive.", "Transformaciones lineales.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }





            
            
        }

        private void ComprobarRangos(object sender, KeyEventArgs e)
        {
            Int32 value = 0;
            if(sender is TextBox)
            {
                TextBox tb = sender as TextBox;
                try
                {
                    value = Int32.Parse(tb.Text.ToString());
                }
                catch(Exception)
                {}
                if(value < 0)
                {
                    tb.Text = "0";
                }
                if(value > 255)
                { 
                    tb.Text = "255";
                }
            }

            
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            ImageForm imf;
            Bitmap result;
            List<Point> puntos = new List<Point>(); 
            for(int i = 0; i < dgvTramos.Rows.Count; i++)
            {
                Point point1 = new Point();
                Point point2 = new Point();
                point1.X = Int32.Parse(this.dgvTramos.Rows[i].Cells["PuntoX1"].Value.ToString());
                point1.Y = Int32.Parse(this.dgvTramos.Rows[i].Cells["PuntoY1"].Value.ToString());
                point2.X = Int32.Parse(this.dgvTramos.Rows[i].Cells["PuntoX2"].Value.ToString());
                point2.Y = Int32.Parse(this.dgvTramos.Rows[i].Cells["PuntoY2"].Value.ToString());
                puntos.Add(point1);
                puntos.Add(point2);
            }
            result = m_Manager.linearTransformation(puntos, m_Image);
            imf = new ImageForm(result);
            imf.MdiParent = this.m_mForm;
            imf.Show();


        }

    }
}
