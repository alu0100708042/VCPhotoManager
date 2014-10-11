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
        private MainForm m_Parent = null;
        private List<Bitmap> m_Historico;
        private Int32[] m_Histograma;

        private ImageForm()
        {
            InitializeComponent();
            m_Historico = new List<Bitmap>();
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
                this.picSource.Image = imagen;
                m_Histograma = getDatosHistograma();
            }
        }
        
        private void SourceForm_Load(object sender, EventArgs e)
        {
                        
            this.ClientSize = this.picSource.Image.Size;
            this.MaximumSize = this.Size;

            m_Parent = this.MdiParent as MainForm;
            

            /* Para trabajar con arrays cast
             * Bitmap mapa = this.picsourceImage.Image as Bitmap;
             * mapa.getPixel(x,y)*/
        }
                
        public PictureBox getPictureBox()
        {
            return this.picSource;
        }

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

        public Int32[] Histograma
        {
            get { return m_Histograma; }
            set { m_Histograma = value; }
        }

        // Controlador de evento para el movimiento del raton sobre la imagen.
        private void picSource_MouseMove(object sender, MouseEventArgs e)
        {
            Bitmap mapa = this.picSource.Image as Bitmap;
            Color color = mapa.GetPixel(e.X, e.Y);
            m_Parent.X = e.X;
            m_Parent.Y = e.Y;
            m_Parent.R = color.R;
            m_Parent.G = color.G;
            m_Parent.B = color.B;
        }

        private Int32[] getDatosHistograma()
        {
            return new Manager().getHistogram(this.picSource.Image as Bitmap);
        }
        

    }
}
