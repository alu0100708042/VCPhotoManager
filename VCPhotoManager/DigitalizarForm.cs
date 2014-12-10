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
    public partial class DigitalizarForm : Form
    {
        private Manager m_manager;
        private Bitmap m_Imagen;
        MainForm m_Parent;

        public DigitalizarForm(Bitmap imagen, MainForm form)
        {
            InitializeComponent();
            m_manager = new Manager();
            m_Imagen = imagen;
            m_Parent = form;
        }

        private void DigitalizarForm_Load(object sender, EventArgs e)
        {
            this.MdiParent = m_Parent;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            comboBox1.SelectedIndex = 0;
            rb2x2.Select();
        }

        private void btnDigitalizar_Click(object sender, EventArgs e)
        {
            Bitmap imagen = new Bitmap(m_Imagen.Width, m_Imagen.Height);
            if (rb2x2.Checked)
            {
                imagen = m_manager.DigitalSimulation(m_Imagen as Bitmap, 2, Convert.ToInt32(comboBox1.SelectedItem));
            }
            else
            {
                imagen = m_manager.DigitalSimulation(m_Imagen as Bitmap, 2, Convert.ToInt32(comboBox1.SelectedItem));
            }

            ImageForm f = new ImageForm(imagen);
            f.MdiParent = m_Parent;
            f.Show();
        }
    }
}
