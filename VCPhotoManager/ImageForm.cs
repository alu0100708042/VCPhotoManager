using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using VCPhotoManager.Clases;
namespace VCPhotoManager
{
    public partial class ImageForm : Form
    {
        private String m_PhotoPath;
        private Rectangle m_Rectangulo;
        private Point m_Pt1, m_Pt2;
        private Color m_lineColor = Color.Red;
        private Boolean m_Seleccionar;
        private Boolean m_Bloqueado = false;
        private MainForm m_Parent = null;
        private List<Bitmap> m_Historico = null;
        private Int32[] m_Histograma = null;
        private Manager m_Manager = null;
        private Bitmap m_Recorte = null;
        private Int32 m_MinValue;
        private Int32 m_MaxValue;
        
        
        private ImageForm()
        {
            InitializeComponent();
            m_Historico = new List<Bitmap>();
            m_Manager = new Manager();
            Cursor = Cursors.Cross;
        }

        public ImageForm(String path) : this()
        {
            if(!String.IsNullOrEmpty(path))
            {
                m_PhotoPath = path;
                this.picSource.Image = Image.FromFile(m_PhotoPath);
                m_Histograma = getDatosHistograma();
            }
        }

        public ImageForm(Bitmap imagen) : this()
        {
            if(imagen != null)
            {
                this.picSource.Image = imagen as Image;
                m_Histograma = getDatosHistograma();
            }
        }
        
        private void ImageForm_Load(object sender, EventArgs e)
        {
            if(this.picSource.Image != null)
            {
                this.ClientSize = this.picSource.Image.Size;
                this.MaximumSize = this.Size;
            }
<<<<<<< HEAD
            m_Parent = this.MdiParent as MainForm;
            MaxMinValueLoad();
           
            this.ClientSize = this.picSource.Image.Size;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            m_Parent = this.MdiParent as MainForm;           
        }

        private void MaxMinValueLoad()
        {
            int i = 0;
            bool load = false;
            while (load == false)
            {
                if (m_Histograma[i] != 0)
                {
                    m_MinValue = i;
                    load = true;
                }
                else
                {
                    i++;
                }
            }

            i = 255;
            load = false;
            while (load == false)
            {
                if (m_Histograma[i] != 0)
                {
                    m_MaxValue = i;
                    load = true;
                }
                else
                {
                    i--;
                }
                
            }
        }
=======
            m_Parent = this.MdiParent as MainForm;       
        }

>>>>>>> Ecualizacion y resta absoluta
        public Bitmap Imagen
        {
            get { return this.picSource.Image as Bitmap; }
            set { this.picSource.Image = value; }
        }

<<<<<<< HEAD
        public Int32 getMinValue()
        {
            return m_MinValue;
        }

        public Int32 getMaxValue()
        {
            return m_MaxValue;
        }


            //public PictureBox getPictureBox()
=======

        //public PictureBox getPictureBox()
>>>>>>> Ecualizacion y resta absoluta
        //{
        //    return this.picSource;
        //}


        public String PhotoPath
        {
            get{ return m_PhotoPath; }
            set{ m_PhotoPath = value; }
        }

        public List<Bitmap> Historico
        {
            get { return m_Historico; }
            set { m_Historico = value; }
        }

        public Boolean ModoSeleccionar
        {
            get { return m_Seleccionar; }
            set { m_Seleccionar = value; }
        }

        public Int32[] Histograma
        {
            get { return m_Histograma; }
            set { m_Histograma = value; }
        }

        // ==================== Esta funcion creo que esta duplicada con la de arriba =====================================
        private Int32[] getDatosHistograma()
        {
            return new Manager().getHistogram(this.picSource.Image as Bitmap);
        }

        public Bitmap getRecorte
        {
            get { return m_Recorte; }
            set { m_Recorte = value; }
        }

        private void picSource_MouseDown(object sender, MouseEventArgs e)
        {
            if(m_Seleccionar)
            {
                if(e.Button != System.Windows.Forms.MouseButtons.Left)
                {
                    return;
                }

                if(m_Rectangulo != null)
                {
                    m_Rectangulo = Rectangle.Empty;
                    this.picSource.Refresh();
                }

                // Punto de inicio del rectángulo
                m_Pt1 = new Point(e.X, e.Y);
                m_Pt2 = m_Pt1;
                m_Bloqueado = true;
            }
            else
            {
                this.picSource.DoDragDrop(this.picSource.Image as Bitmap, DragDropEffects.Copy);
            }

        }
         
        // Controlador de evento para el movimiento del raton sobre la imagen.
        private void picSource_MouseMove(object sender, MouseEventArgs e)
        {
            // Punto final del rectángulo
            // Invalidamos el control PictureBox
            if(m_Bloqueado)
            {
                m_Pt2 = new Point(e.X, e.Y);
                this.picSource.Invalidate();
            }

            Bitmap mapa = this.picSource.Image as Bitmap;
            Color color;
            try
            {
                color = mapa.GetPixel(e.X, e.Y);
                m_Parent.X = e.X;
                m_Parent.Y = e.Y;
                m_Parent.R = color.R;
                m_Parent.G = color.G;
                m_Parent.B = color.B;
<<<<<<< HEAD
                m_Parent.MinValue = m_MinValue;
                m_Parent.MaxValue = m_MaxValue;
=======
>>>>>>> Ecualizacion y resta absoluta
            }
            catch(ArgumentOutOfRangeException) { }
            
            
        }

        /// <summary>
        /// Obtiene la imagen contenida en el rectangulo dibujado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picSource_MouseUp(object sender, MouseEventArgs e)
        {
            if(!m_Bloqueado)
            {
                return;
            }
                        
            m_Rectangulo = m_Manager.getRectangle(m_Pt1, new Point(e.X, e.Y));
                        
            m_Bloqueado = false;
            m_Recorte = m_Manager.createSubBitmap(this.picSource.Image as Bitmap, m_Rectangulo);
        }

        private void picSource_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if(m_Bloqueado)
            {
                g.DrawRectangle(new Pen(m_lineColor), m_Manager.getRectangle(m_Pt1, m_Pt2));
            }
            else
            {
                g.DrawRectangle(new Pen(m_lineColor), m_Rectangulo);
            }
        }
        


        


    }
}
