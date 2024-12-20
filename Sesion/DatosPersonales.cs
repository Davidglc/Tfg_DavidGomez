using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TFG_DavidGomez.Clases;

namespace TFG_DavidGomez.Sesion
{
    public partial class DatosPersonales : Form
    {
        public DatosPersonales()
        {
            InitializeComponent();
        }

        private void datosNiñosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisNino regisNino = new RegisNino();
            regisNino.VerificarInstancia();
            SesionIniciada si = new SesionIniciada();
            
        }
    }
}

