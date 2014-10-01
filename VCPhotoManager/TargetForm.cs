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
        private Image m_imagen;
        public TargetForm(Image imagen)
        {
            InitializeComponent();
            m_imagen = imagen;
        }

        private void TargetForm_Load(object sender, EventArgs e)
        {
            this.picTarget.Image = m_imagen;
            this.ClientSize = this.picTarget.Image.Size;


            this.MaximumSize = this.Size;
        }

        public Image getImage()
        {
            return this.m_imagen;
        }
    }
}
