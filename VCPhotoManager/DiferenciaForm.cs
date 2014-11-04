using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VCPhotoManager.Clases;

namespace VCPhotoManager
{
    public partial class DiferenciaForm : Form
    {
        private MainForm m_Parent;

        public DiferenciaForm(MainForm parent)
        {
            InitializeComponent();
            this.pictureBox1.AllowDrop = true;
            this.pictureBox2.AllowDrop = true;
            this.m_Parent = parent;
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            this.pictureBox1.Image = e.Data.GetData(DataFormats.Bitmap) as Image;
        }


        private void pictureBox2_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBox2_DragDrop(object sender, DragEventArgs e)
        {
            this.pictureBox2.Image = e.Data.GetData(DataFormats.Bitmap) as Image;
        }


        private void btnElimnar1_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = null;
        }

        private void btnEliminar2_Click(object sender, EventArgs e)
        {
            this.pictureBox2.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager m = new Manager();
            Bitmap result = m.getImageDifference(this.pictureBox1.Image as Bitmap, this.pictureBox2.Image as Bitmap, Int32.Parse(this.lbPrecision.Text));
            ImageForm f = new ImageForm(result);
            f.MdiParent = m_Parent;
            f.Show();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.lbPrecision.Text = this.trackBar1.Value.ToString();
        }

        
    }
}
