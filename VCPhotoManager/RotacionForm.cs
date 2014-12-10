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
    public partial class RotacionForm : Form
    {
        private Manager m_manager;
        private Bitmap m_Imagen;
        MainForm m_Parent;

        public RotacionForm(Bitmap imagen, MainForm form)
        {
            InitializeComponent();
            m_manager = new Manager();
            m_Imagen = imagen;
            m_Parent = form;
            this.MdiParent = m_Parent;
            comboBox1.SelectedIndex = 0;
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            Bitmap result = null;
            ImageForm f = null;

            if (Convert.ToInt32(textBox1.Text) == 90 || Convert.ToInt32(textBox1.Text) == 180 || Convert.ToInt32(textBox1.Text) == 270)
            {
                result = m_manager.rotarBasico(m_Imagen, Convert.ToInt32(textBox1.Text));
            }
            else if (comboBox1.SelectedIndex == 0)
            {
                result = m_manager.RotateImageTD(m_Imagen, Convert.ToInt32(textBox1.Text));
            }
            else 
            {
                //result = m_manager.RotateImageTI(m_Imagen, Convert.ToInt32(textBox1.Text));
                result = m_manager.rotarInterpolar(m_Imagen, 0, 0, Convert.ToInt32(textBox1.Text));
            }
            f = new ImageForm(result);
            f.MdiParent = m_Parent;
            f.Show();
        }

        private void RotacionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
