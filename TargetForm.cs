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
    public partial class TargetForm : Form
    {
        
        private List<Bitmap> m_Mapas;
        private MainForm m_Parent;

        public TargetForm(Image imagen)
        {
            InitializeComponent();
            m_Mapas = new List<Bitmap>();
            m_Mapas.Add(imagen as Bitmap);
            
        }

        private void picTarget_MouseMove(object sender, MouseEventArgs e)
        {
            Bitmap mapa = this.picTarget.Image as Bitmap;
            Color color = mapa.GetPixel(e.X, e.Y);
            m_Parent.X = e.X;
            m_Parent.Y = e.Y;
            m_Parent.R = color.R;
            m_Parent.G = color.G;
            m_Parent.B = color.B;
        }

        private void TargetForm_Load(object sender, EventArgs e)
        {
            this.picTarget.Image = m_Mapas[0];
            this.ClientSize = this.picTarget.Image.Size;
            this.MaximumSize = this.Size;
            m_Parent = this.MdiParent as MainForm;
        }

        public List<Bitmap> Historico
        {
            get { return m_Mapas; }
            set { m_Mapas = value; }
        }
        
        public PictureBox getPicTarget
        {
            get { return this.picTarget; }
            set { this.picTarget = value; }
        }

    }
}
