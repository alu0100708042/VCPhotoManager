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
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace VCPhotoManager
{
    public partial class MainForm : Form
    {
        ImageForm m_SourceForm;
        Manager m_Manager;
        HistogramaGraficsForm m_HistogramaForm;

        SpeechSynthesizer m_Synthesizer;
        PromptBuilder m_Builder;
        SpeechRecognitionEngine m_Engine;

        //private Int32 m_MinValue;
        //private Int32 m_MaxValue;
        
        public MainForm()
        {
            InitializeComponent();
            m_Manager = new Manager();
            m_Synthesizer = new SpeechSynthesizer();
            m_Builder = new PromptBuilder();
            m_Engine = new SpeechRecognitionEngine();


        }

        public Int32 MinValue
        {
            get { return Int32.Parse(this.lbMinVal.Text); }
            set { this.lbMinVal.Text = value.ToString(); }
        }

        public Int32 MaxValue
        {
            get { return Int32.Parse(this.lbMaxVal.Text); }
            set { this.lbMaxVal.Text = value.ToString(); }
        }

        public Int32 X
        {
            get { return Int32.Parse(this.lbX.Text); }
            set { this.lbX.Text = value.ToString(); }
        }

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
            abrirImagen();
        }

        private void abrirImagen()
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Imágenes con pérdida (*.gif, *.jpg)|*.gif;*.jpg|Imágenes sin pérdida (*.bmp, *.tif)|*.bmp;*.tif";

            if(o.ShowDialog() == DialogResult.OK)
            {
                String path = o.FileName;
                m_SourceForm = new ImageForm(o.FileName);
                m_SourceForm.MdiParent = this;
                m_SourceForm.Show();
            }
        }

        private void guardarImagen(object sender, EventArgs e)
        {
            guardarImagen();
        }

        private void guardarImagen()
        {
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            String formats = "(.jpg|.gif |.bmp|.tiff)";

            if(this.ActiveMdiChild is ImageForm)
            {
                String path = m_SourceForm.PhotoPath;
                String[] subStrings = Regex.Split(path, formats);
                String format = "";
                Int16 cont = 0;
                foreach(String aux in subStrings)
                {
                    if(cont == 1)
                    {
                        format = aux;
                    }
                    cont++;
                }
                ImageForm s = this.ActiveMdiChild as ImageForm;
                String imageName = Regex.Replace(path, formats, String.Empty);
                imageName = imageName + "-copia" + format;
                try
                {
                    s.Imagen.Save(imageName);
                    MessageBox.Show("Se ha guardado el archivo con exito." + Environment.NewLine + Environment.NewLine +
                        imageName, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch(ExternalException ex)
                {
                    MessageBox.Show(ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Para guardar una imagen debe tener seleccionado un formulario de imagen", 
                    "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GuardarComo( ImageForm f)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "Imágenes con pérdida (*.gif, *.jpg)|*.gif;*.jpg|Imágenes sin pérdida (*.bmp, *.tif)|*.bmp;*.tif";
            ImageFormat format = ImageFormat.Jpeg;
            if (s.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(s.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;
                    case ".tif":
                        format = ImageFormat.Tiff;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                f.Imagen.Save(s.FileName, format);
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //m_Manager = new Manager();
            //m_SourceForm = new SourceForm(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\imagenprueba.jpg");
            //m_SourceForm.Show();
            //System.Threading.Thread.Sleep(500);
            ///*m_TargetForm = new TargetForm(m_Manager.changeToGrayScale((m_SourceForm.getPictureBox().Image as Bitmap)));
            //m_TargetForm.Show();*/

            //initSpeech();

        }

        private void initSpeech()
        {
            /*
            m_Builder.ClearContent();
            m_Builder.AppendText("Hello Oliver & Ricky");
            m_Synthesizer.Speak(m_Builder);
            m_Builder.ClearContent();
            */

            Choices lista = new Choices();
            lista.Add(new String[] {"histograma de frecuencias", "histograma acumulativo", "abrir imagen",
                "cargar imagen", "abrir", "guardar imagen", "grabar imagen", "guardar", "grabar", 
                "negativizar", "negativizar imagen"});
            Grammar gr = new Grammar(new GrammarBuilder(lista));
            try
            {
                m_Engine.RequestRecognizerUpdate();
                m_Engine.LoadGrammar(gr);
                m_Engine.SpeechRecognized += m_Engine_SpeechRecognized;
                m_Engine.SetInputToDefaultAudioDevice();
                m_Engine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
        }

        private void m_Engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            String result = e.Result.Text;

            if(result == "histograma de frecuencias")
            {
                histogramaFrecencias();
            }
            else if(result == "histograma acumulativo")
            {
                histogramaAcumulativo();
            }
            else if((result == "abrir imagen") || (result == "cargar imagen") || (result == "abrir"))
            {
                abrirImagen();
            }
            else if((result == "guardar imagen") || (result == "grabar imagen") || (result == "guardar") || (result == "grabar"))
            {
                guardarImagen();
            }
            else if((result == "negativizar") || (result == "negativizar imagen"))
            {
                negativizar();
            }           

        }

        public void CambiaraEscalaDeGrises(object sender, EventArgs e) 
        {
            Manager m = new Manager();
            Bitmap b = new Bitmap(this.m_SourceForm.Imagen);
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
                entropy = m_Manager.Entropia(f.Imagen);
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
            // Lo comente porque siempre peta creo que no tiene lógica porque siempre que se hace un cambio
            // se genera una imagen nueva
            //m_SourceForm.Historico.RemoveAt(m_SourceForm.Historico.Count - 1);
            //m_SourceForm.Imagen = m_SourceForm.Historico[m_SourceForm.Historico.Count - 1];
        }

        private void copiarToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void acumulativoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            histogramaAcumulativo();
        }

        private void histogramaAcumulativo()
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;

                //Int32[] vector = m_Manager.getNormalizeHistogram(m_Manager.getHistogram(f.getPictureBox().Image as Bitmap));
                //m_HistogramaForm = new HistogramaGraficsForm(vector, null);

                Int32 max = -9999;

                Int32[] aux = m_Manager.getHistogram(f.Imagen);
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
                m_HistogramaForm = new HistogramaGraficsForm(vector, "Histograma acumulativo", max, this);

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
            histogramaFrecencias();
        }

        private void histogramaFrecencias()
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                Int32 max = -99999;
                Int32[] aux = m_Manager.getHistogram(f.Imagen as Bitmap);

                for(int i = 0; i < 256; i++)
                {
                    if(max < aux[i])
                    {
                        max = aux[i];
                    }
                }
                Int32[] vector = m_Manager.getNormalizeHistogram(aux);
                m_HistogramaForm = new HistogramaGraficsForm(vector, null, max, this);
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
            negativizar();
        }

        private void negativizar()
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                List<Point> point = new List<Point>();
                point.Add(new Point(0, 255));
                point.Add(new Point(255, 0));
                ImageForm s = new ImageForm(m_Manager.linearTransformation(point, f.Imagen));
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
                BrilloContrasteForm bcForm = new BrilloContrasteForm(f.Imagen, this);
                bcForm.MdiParent = this;
                bcForm.Show();
              /*  
                Int32[] brillo = m_Manager.brightnessAndContrast(f.getPictureBox().Image as Bitmap);
                MessageBox.Show("El brillo de la imagen es:" + brillo[0] + " y el constraste:" + brillo[1]);
                // Ojo el primer valor que pasamos a la funcion de brillo es la diferencia entre el nuevo y el actual
                ImageForm s = new ImageForm(m_Manager.changeBrightnessAndContrast(200,69, f.getPictureBox().Image as Bitmap));
                s.MdiParent = this;
                s.Show();
               * */
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Generación de Histogramas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           // MessageBox.Show("El brillo de la imagen es:" + brillo[0].ToString() + " y el contraste:" + brillo[1].ToString());
        }






        private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                GammaForm g = new GammaForm(f.Imagen, this);
                g.MdiParent = this;
                g.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Generación de Histogramas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ayudaToolStripButton_Click(object sender, EventArgs e)
        {
          
        }

        private void porTramosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm imgForm = this.ActiveMdiChild as ImageForm;
                RangosForm rangForm = new RangosForm(imgForm.Imagen, this);
                rangForm.MdiParent = this;
                rangForm.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Generación de Histogramas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void acercadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDeForm f = new AcercaDeForm();
            f.Show();
        }

        private void diferenciaDeImagenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiferenciaForm f = new DiferenciaForm(this,null);
            f.MdiParent = this;
            f.Show();
        }

        private void ecualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                ImageForm s = new ImageForm(m_Manager.EcualizeImage(f.Imagen, f.MaxValue, f.MinValue));
                s.MdiParent = this;
                s.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Ecualización de Imagen",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void especificaciónDelHistogramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiferenciaForm f = new DiferenciaForm(this, "HistogramSpecification");
            f.MdiParent = this;
            f.Show();
        }

        private void pruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                DigitalizarForm d = new DigitalizarForm(f.Imagen, this);
                d.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Digitalización",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guardarcomoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                GuardarComo(f);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Guardar Como",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void espejoVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                ImageForm n = new ImageForm(m_Manager.EspejoHorizontal(f.Imagen));
                n.MdiParent = this;
                n.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Guardar Espejo Horizontal",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void espejoVerticalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                ImageForm n = new ImageForm(m_Manager.EspejoVertical(f.Imagen));
                n.MdiParent = this;
                n.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Guardar Espejo Horizontal",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                ZoomForm n = new ZoomForm(f.Imagen, this);
                n.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Zoom",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

        private void rotarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ImageForm)
            {
                ImageForm f = this.ActiveMdiChild as ImageForm;
                RotacionForm n = new RotacionForm(f.Imagen, this);
                n.Show();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una imagen.", "Rotación",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
