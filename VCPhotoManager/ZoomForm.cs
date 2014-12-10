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
    public partial class ZoomForm : Form
    {
        private Manager m_manager;
        private Bitmap m_Imagen;
        MainForm m_Parent;

        public ZoomForm(Bitmap imagen, MainForm form)
        {
            InitializeComponent();
            m_manager = new Manager();
            m_Imagen = imagen;
            m_Parent = form;
            comboBox1.SelectedIndex = 0;
            this.MdiParent = m_Parent;
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            Bitmap result = null;
            ImageForm f = null;
            if (comboBox1.SelectedIndex == 0)
            {
                result = m_manager.ZoomVMP(m_Imagen, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                f = new ImageForm(result);
            }
            else
            {
                result = m_manager.ZoomBilineal(m_Imagen, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                f = new ImageForm(result);            
            }
  
            f = new ImageForm(result);
            f.MdiParent = m_Parent;
            f.Show();
        }

    }
}
