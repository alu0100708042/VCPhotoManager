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
        private Int32[] m_Vector;
        private Pen m_Pen; 
                
        public HistogramaGraficsForm(Int32[] vector, String titulo, Int32 maximo)
        {
            if (vector != null)
            {
                InitializeComponent();
                this.MaximumSize = this.Size;
                m_Vector = vector;
                if(!String.IsNullOrEmpty(titulo))
                {
                    this.lbTitulo.Text = titulo;
                    this.lbMaximo.Text = maximo.ToString();
                }
                else
                {
                    this.lbTitulo.Text = "Histograma";
                    this.lbMaximo.Text = maximo.ToString();
                }
            }
        }
        
        private void HistogramaGraficsForm_Load(object sender, EventArgs e)
        {
            //paintHistogram(this.CreateGraphics());
        }
        
        private void paintHistogram(Graphics g)
        {
            
            Point p1 = new Point();
            Point p2 = new Point();

            m_Pen = new Pen(Color.Black);
            m_Pen.Width = 3;


            
            g.DrawLine(m_Pen, 44, 350, 562, 350);
            g.DrawLine(m_Pen, 44, 350, 44, 50);
            m_Pen.Color = Color.Blue;
            m_Pen.Width = 2;
            for(int i = 0; i < 256; i++)
            {
                p1.X = 47 + i * 2;
                p1.Y = 349;
                p2.X = 47 + i * 2;
                p2.Y = (349 - m_Vector[i]);
                g.DrawLine(m_Pen, p1, p2);
            }
<<<<<<< HEAD

            
=======
>>>>>>> 54215e71bcebc5421b9d59b39a8ba38d2de9916d
        }

        private void HistogramaGraficsForm_Paint(object sender, PaintEventArgs e)
        {
            paintHistogram(this.CreateGraphics());
        }

    }
}
