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
    public partial class GammaForm : Form
    {
        private Bitmap m_Imagen;
        private MainForm m_Form;
        private Manager m_Manager;

        public GammaForm(Bitmap imagen, MainForm form)
        {
            InitializeComponent();
            this.m_Imagen = imagen;
            this.m_Form = form;
            this.MdiParent = m_Form;
            m_Manager = new Manager();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            Double value = 0.0;
            
            if(Double.TryParse(this.txtGamma.Text.Replace(".", ","), out value))
            {
                ImageForm f = new ImageForm(m_Manager.noLinearTransformation(m_Imagen, value) as Bitmap);
                f.MdiParent = this.m_Form;
                f.Show();

            }
        }


        
    }
}
