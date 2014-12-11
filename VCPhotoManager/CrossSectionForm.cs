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
    public partial class CrossSectionForm : Form
    {
        MainForm m_Parent;
        Int32[] m_perfil;
        Int32[] m_derivada;
        public CrossSectionForm(MainForm mform, Int32[]perfil, Int32[]derivada)
        {
            InitializeComponent();
            m_Parent = mform;
            m_perfil = perfil;
            m_derivada = derivada;
        }

        private void CrossSectionForm_Load(object sender, EventArgs e)
        {
            pintarGrafica();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.MaximizeBox = false;
            
        }

        private void pintarGrafica()
        {
            for (int i = 0; i < m_perfil.Length; i++)
            {
                chart1.Series["Perfil"].Points.AddXY
                                ( i, m_perfil[i]);
                chart1.Series["Derivada"].Points.AddXY
                                (i, m_derivada[i]);
            }
 
        }
    }
}
