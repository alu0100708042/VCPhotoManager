using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VCPhotoManager
{
    public partial class HistogramaGraficsForm : Form
    {
        Int32[] m_Vector;
        System.Drawing.Pen m_Pen = new System.Drawing.Pen(System.Drawing.Color.Black);
        
        

        public HistogramaGraficsForm(Int32[] vector)
        {
            m_Vector = new Int32[256];
            m_Pen.Width = 3;
            if (vector != null)
            {
                InitializeComponent();
                this.MaximumSize = this.Size;
                m_Vector = vector;
            }
            
        }

        public void HistogramaAculativo() 
        {

        }



        public void Histograma() 
        {
            //this.SuspendLayout();
            Point p1 = new Point();
            Point p2 = new Point();

            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            
            formGraphics.DrawLine(m_Pen, 44, 350, 562, 350);
            formGraphics.DrawLine(m_Pen, 44, 350, 44, 50);
            m_Pen.Color = Color.Blue;
            m_Pen.Width = 2;
            for (int i = 0; i < 256; i++)
            {
                p1.X = 50 + i * 2;
                p1.Y = 349;
                p2.X = 50 + i * 2;
                p2.Y = (349 - m_Vector[i] % 300);
                formGraphics.DrawLine(m_Pen, p1, p2);
            }
            m_Pen.Dispose();
            formGraphics.Dispose();




          /*  Label l = new Label();
            l.Text = "Histograma";
            l.Location = new Point(172, 0);
            this.Controls.Add(l);
            this.ResumeLayout();
            this.Refresh();*/
        }
        
        private void HistogramaGraficsForm_Load(object sender, EventArgs e)
        {
            
            //this.Refresh();
        }

        

    }
}
