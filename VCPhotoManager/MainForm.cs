using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VCPhotoManager.Clases;
using System.Text.RegularExpressions;
namespace VCPhotoManager
{
    public partial class MainForm : Form
    {
        ImageForm m_SourceForm;
        Manager m_Manager;
        HistogramaGraficsForm m_HistogramaForm;
        
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
            o.Filter = "Imágenes con pérdida (*.gif, *.jpg)|*.gif;*.jpg|Imágenes sin pérdida (*.bmp, *.tiff)|*.bmp;*.tiff";

            if (o.ShowDialog() == DialogResult.OK)
            {
                String path = o.FileName;
                m_SourceForm = new ImageForm(o.FileName);
                m_SourceForm.MdiParent = this;
                m_SourceForm.Show();
            }
        }

        private void GuardarImagen(object sender, EventArgs e)
        {
            String ruta =Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            String formats = "(.jpg|.gif |.bmp|.tiff)";
           
            if(this.ActiveMdiChild is ImageForm)
            {
                String path = m_SourceForm.PhotoPath;
                String[] subStrings = Regex.Split(path, formats);
                String format = "";
                Int16 cont = 0;
                foreach (String aux in subStrings)
                {
                    if (cont == 1)
                    {
                        format = aux;
                    }
                    cont++;
                }
                ImageForm  s = this.ActiveMdiChild as ImageForm;
                String imageName = Regex.Replace(path, formats, String.Empty);
                s.getPictureBox().Image.Save(imageName + "-copia" + format);
            }
            else  
            {
                MessageBox.Show("Para almacenar una imagen hay que hacer click sobre la misma");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            this.MaximizeBox = false;
            this.MinimumSize = this.Size;
            //m_Manager = new Manager();
            //m_SourceForm = new SourceForm(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\imagenprueba.jpg");
            //m_SourceForm.Show();
            //System.Threading.Thread.Sleep(500);
            ///*m_TargetForm = new TargetForm(m_Manager.changeToGrayScale((m_SourceForm.getPictureBox().Image as Bitmap)));
            //m_TargetForm.Show();*/

        }

        public void CambiaraEscalaDeGrises(object sender, EventArgs e) 
        {
            Manager m = new Manager();
            Bitmap b = new Bitmap(this.m_SourceForm.getPictureBox().Image as Bitmap);
            ImageForm s= new ImageForm(m.changeToGrayScale(b));
            s.MdiParent = this;
            s.Show();
        }

        public void EntropiaDeImagen(object sender, EventArgs e)
        {
            Double entropy;

            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                entropy = m_Manager.Entropia(f.getPictureBox().Image as Bitmap);
                MessageBox.Show("El valor de la entropía de la imagen es: " + Math.Round(entropy, 2).ToString(),
                    "Entropia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Generación de Histogramas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SourceForm.Historico.RemoveAt(m_SourceForm.Historico.Count - 1);
            m_SourceForm.getPictureBox().Image = m_SourceForm.Historico[m_SourceForm.Historico.Count - 1];
        }

        private void copiarToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void acumulativoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                
                //Int32[] vector = m_Manager.getNormalizeHistogram(m_Manager.getHistogram(f.getPictureBox().Image as Bitmap));
                //m_HistogramaForm = new HistogramaGraficsForm(vector, null);

                Int32 max = -9999;

                Int32[] aux = m_Manager.getHistogram(f.getPictureBox().Image as Bitmap);
                for(int i = 0; i < 256; i++)
                {
                    if(max < aux[i])
                    {
                        max = aux[i];
                    }
                }
                
                //Int32[] vector = m_Manager.getNormalizeHistogram(aux);
                //m_HistogramaForm = new HistogramaGraficsForm(vector, null,max);


                Int32[] vector = m_Manager.getCumulativeHistogram(f.Histograma);
                m_HistogramaForm = new HistogramaGraficsForm(vector, "Histograma acumulativo", max);

                m_HistogramaForm.MdiParent = this;
                m_HistogramaForm.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Generación de Histogramas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frecuenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                Int32 max = -99999; 
                Int32[] aux = m_Manager.getHistogram(f.getPictureBox().Image as Bitmap);
                for (int i = 0; i < 256; i++)
                {
                    if(max < aux[i])
                    {
                        max = aux[i];
                    }
                }
                Int32[] vector = m_Manager.getNormalizeHistogram(aux);
                m_HistogramaForm = new HistogramaGraficsForm(vector, null,max);
                m_HistogramaForm.MdiParent = this;
                m_HistogramaForm.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Generación de Histogramas", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void negativizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                Point[] point = new Point[2];
                point[0] = new Point(0, 255);
                point[1] = new Point(255, 0);
                ImageForm s = new ImageForm(m_Manager.linearTransformation(point, f.getPictureBox().Image as Bitmap));
                s.MdiParent = this;
                s.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Generación de Histogramas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                f.ModoSeleccionar = true;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Selección",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                

                Bitmap imagen = f.getRecorte;
                
                if(imagen != null)
                {
                    
                    ImageForm target = new ImageForm(imagen);
                    target.MdiParent = this;
                    target.Show();
                }
                else
                {
                    MessageBox.Show("Debe tener una selección en la imagen actual. Use Editar, Seleccionar.", "Selección",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Selección",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void imprimirToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void brilloYContrasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                Int32[] brillo = m_Manager.brightnessAndContrast(f.getPictureBox().Image as Bitmap);
                MessageBox.Show("El brillo de la imagen es:" + brillo[0] + " y el constraste:" + brillo[1]);
                // Ojo el primer valor que pasamos a la funcion de brillo es la diferencia entre el nuevo y el actual
                ImageForm s = new ImageForm(m_Manager.changeBrightnessAndContrast(200,69, f.getPictureBox().Image as Bitmap));
                s.MdiParent = this;
                s.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Generación de Histogramas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           // MessageBox.Show("El brillo de la imagen es:" + brillo[0].ToString() + " y el contraste:" + brillo[1].ToString());
        }

        private void transformacionesNoLinealesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                ImageForm s = new ImageForm(m_Manager.noLinearTransformation(f.getPictureBox().Image as Bitmap,.25));
                s.MdiParent = this;
                s.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Generación de Histogramas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
