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

        public TargetForm(Image imagen)
        {
            InitializeComponent();
            m_Mapas = new List<Bitmap>();
            m_Mapas.Add(imagen as Bitmap);
            
        }

        private void TargetForm_Load(object sender, EventArgs e)
        {
            this.picTarget.Image = m_Mapas[0];
            this.ClientSize = this.picTarget.Image.Size;
            this.MaximumSize = this.Size;
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
