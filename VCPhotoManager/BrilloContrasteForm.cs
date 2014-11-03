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
    public partial class BrilloContrasteForm : Form
    {
        private Int32 m_brillo;
        private Int32 m_contraste;
        private Manager m_manager;
        private Bitmap m_Imagen;
        MainForm m_Parent;

        public BrilloContrasteForm(Bitmap imagen, MainForm form)
        {
            InitializeComponent();
            m_manager = new Manager();
            m_Imagen = imagen;
            m_Parent = form;
        }

        private void BrilloContrasteForm_Load(object sender, EventArgs e)
        {
            this.picImage.Image = m_Imagen;
            
           
            m_Parent = this.MdiParent as MainForm; 
            Int32[] valores = m_manager.brightnessAndContrast(m_Imagen);
            
            m_brillo = valores[0];//
            m_contraste = valores[1];
            this.trbBrillo.Value = m_brillo;
            this.trbContraste.Value = m_contraste;

            //this.splitContainer1.Panel2.Width = this.picImage.Width;
            //this.splitContainer1.Panel2.Height = this.picImage.Height;

            //this.ClientSize = this.picImage.Image.Size;
            this.MinimumSize = this.Size;
            
        }








        private void cambiarBrillo()
        {
            int value = this.trbBrillo.Value;
            int Contrastvalue = this.trbContraste.Value;
            this.lbBrillo.Text = value.ToString();
            this.picImage.Image = m_manager.changeBrightnessAndContrast(value, m_contraste, m_Imagen);
            m_Imagen = this.picImage.Image as Bitmap;
        }

        private void cambiarContraste()
        {
            int value = this.trbContraste.Value;
            int Brightnessvalue = this.trbBrillo.Value;
            this.lbContraste.Text = value.ToString();
            this.picImage.Image = m_manager.changeBrightnessAndContrast(Brightnessvalue, value , m_Imagen);
            m_Imagen = this.picImage.Image as Bitmap;
        }

        private void trbBrillo_MouseUp(object sender, MouseEventArgs e)
        {
            cambiarBrillo();
        }

        private void trbBrillo_KeyUp(object sender, KeyEventArgs e)
        {
            cambiarBrillo();
        }

        private void trbBrillo_ValueChanged(object sender, EventArgs e)
        {
            this.lbBrillo.Text = this.trbBrillo.Value.ToString();
        }

        private void trbContraste_MouseUp(object sender, MouseEventArgs e)
        {
            cambiarContraste();
        }

        private void trbContraste_KeyUp(object sender, KeyEventArgs e)
        {
            cambiarContraste();
        }

        private void trbContraste_ValueChanged(object sender, EventArgs e)
        {
            this.lbContraste.Text = this.trbContraste.Value.ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ImageForm f = new ImageForm(this.picImage.Image as Bitmap);
            f.MdiParent = m_Parent;
            f.Show();
            
        }

        
        


    }
}
