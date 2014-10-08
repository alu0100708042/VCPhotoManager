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
    public partial class MainForm : Form
    {
        SourceForm m_SourceForm;
        TargetForm m_TargetForm;
        Manager m_Manager;
        
        public MainForm()
        {
            InitializeComponent();
            m_Manager = new Manager();
        }

        // Propiedad X para tener acceso a la etiqueta y mostrar su valor
        public Int32 X
        {
            get { return Int32.Parse(this.lbX.Text); }
            set { this.lbX.Text = value.ToString(); }
        }

        // Propiedad Y para tener acceso a la etiqueta y mostrar su valor
        public Int32 Y
        {
            get { return Int32.Parse(this.lbY.Text); }
            set { this.lbY.Text = value.ToString(); }
        }

        public Int32 R
        {
            get { return Int32.Parse(this.lbR.Text); }
            set { this.lbR.Text = value.ToString(); }
        }

        public Int32 G
        {
            get { return Int32.Parse(this.lbG.Text); }
            set { this.lbG.Text = value.ToString(); }
        }

        public Int32 B
        {
            get { return Int32.Parse(this.lbB.Text); }
            set { this.lbB.Text = value.ToString(); }
        }
        
        private void AbrirImagen(object sender, EventArgs e) 
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Imágenes sin pérdida (*.bmp, *.tiff)|*.bmp;*.tiff|Imágenes con pérdida (*.gif, *.jpg)|*.gif;*.jpg";
            
            if (o.ShowDialog() == DialogResult.OK)
            {
                String path = o.FileName;
                m_SourceForm = new SourceForm(o.FileName);
                m_SourceForm.MdiParent = this;
                m_SourceForm.Show(); // Sin cortar la ejecucion para poder usar los controles del primario
                // Para abrir una imagen nueva tendrás que deshabilitar el abrir otra.
                this.abrirToolStripButton.Enabled = false;
                this.abrirToolStripMenuItem.Enabled = false;
            }
        }

        private void GuardarImagen(object sender, EventArgs e)
        {
            String ruta =Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (this.ActiveMdiChild == m_SourceForm)
            {
                m_SourceForm.getPictureBox().Image.Save(ruta + @"\cocot.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else if(this.ActiveMdiChild == m_TargetForm)
            {
                m_TargetForm.getPicTarget.Image.Save(ruta + @"\gatogris.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else  
            {
                MessageBox.Show("Para almacenar una imagen hay que hacer click sobre la misma");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public void CambiaraEscalaDeGrises(object sender, EventArgs e) 
        {
            Manager m = new Manager();
            m_TargetForm = new TargetForm(m.changeToGrayScale(this.m_SourceForm.getPictureBox().Image as Bitmap));
            m_TargetForm.MdiParent = this;
            m_TargetForm.Show();
        }

        public void EntropiaDeImagen(object sender, EventArgs e)
        {
            Double entropy;
            entropy = m_Manager.Entropia(this.m_SourceForm.getPictureBox().Image as Bitmap);
            MessageBox.Show("La entropía de la imagen es de: " + entropy.ToString());
        }
        private void cerrarSource(object sender, FormClosedEventArgs e)
        {
            this.abrirToolStripButton.Enabled = true;
            this.abrirToolStripMenuItem.Enabled = true;
        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_TargetForm.Historico.RemoveAt(m_TargetForm.Historico.Count - 1);
            m_TargetForm.getPicTarget.Image = m_TargetForm.Historico[m_TargetForm.Historico.Count - 1];
        }
    }
}
