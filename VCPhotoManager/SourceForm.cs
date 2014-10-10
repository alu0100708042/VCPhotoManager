using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
namespace VCPhotoManager
{
    public partial class SourceForm : Form
    {
        private String m_PhotoPath;
        private MainForm m_Parent;

        public SourceForm(String path)
        {
            InitializeComponent();
            m_PhotoPath = path;
        }
        
        private void SourceForm_Load(object sender, EventArgs e)
        {
            this.picSource.Image = Image.FromFile(m_PhotoPath);
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

        public String getPhotoPath()
        {
            return m_PhotoPath;
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

    }
}
