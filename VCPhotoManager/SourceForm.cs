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

        public SourceForm(String path)
        {
            InitializeComponent();
            m_PhotoPath = path;
        /*Para guardar
        */
        
        }

        public PictureBox getPicSource()
        {
            return this.picSource;
        }


        private void SourceForm_Load(object sender, EventArgs e)
        {
            this.picSource.Image = Image.FromFile(m_PhotoPath);
            this.ClientSize = this.picSource.Image.Size;
            this.MaximumSize = this.Size;


            /* Para trabajar con arrays cast
             * Bitmap mapa = this.picsourceImage.Image as Bitmap;
             * mapa.getPixel(x,y)*/
        }
        public PictureBox getPictureBox()
        {
            return this.picSource;
        }
    }
}
