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
        private MainForm m_Parent;
                
        public HistogramaGraficsForm(Int32[] vector, String titulo, Int32 maximo, MainForm parent)
        {
            InitializeComponent();
            if (vector != null)
            {
                m_Parent = parent;
                this.MaximumSize = this.Size;
                m_Vector = vector;
                if(!String.IsNullOrEmpty(titulo))
                {
                    if (titulo == "crossSection")
                        label1.Visible = false;
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

            if (lbTitulo.Text != "crossSection")
            {

                g.DrawLine(m_Pen, 44, 350, 562, 350);
                g.DrawLine(m_Pen, 44, 350, 44, 50);
                m_Pen.Color = Color.Blue;
                m_Pen.Width = 2;
                for (int i = 0; i < 256; i++)
                {
                    p1.X = 47 + i * 2;
                    p1.Y = 349;
                    p2.X = 47 + i * 2;
                    p2.Y = (349 - m_Vector[i]);
                    g.DrawLine(m_Pen, p1, p2);
                }
            }
            else 
            {
                g.DrawLine(m_Pen, 44, 350, 562, 350);
                g.DrawLine(m_Pen, 44, 350, 44, 50);
                m_Pen.Color = Color.Blue;

                for (int x = 0; x < m_Vector.Length; x++)
                {
                    p1.X = 47 + x * 2;
                    p1.Y = 349;
                    p2.X = 47 + x * 2;
                    p2.Y = (349 - m_Vector[x]);
                    g.DrawLine(m_Pen, p1, p2);
                }
            }

        }

        private void HistogramaGraficsForm_Paint(object sender, PaintEventArgs e)
        {
            paintHistogram(this.CreateGraphics());
        }

        private void HistogramaGraficsForm_MouseMove(object sender, MouseEventArgs e)
        {
            Int32 value = e.X - 47;
            if(value >= 0 && value <= 512)
            {
                m_Parent.X = value/2;
            }
        }

    }
}
