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
    public partial class Registrarse : Form
    {
        public Registrarse()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new RegisNino().ShowDialog(this);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        public static void ActualizarLabelNino(Nino nino)
        {
            
            //LblNino.Text = $"Nombre: {nino.Nombre}, Apellidos: {nino.Apellidos}, Edad: {nino.Edad}";
        }

    }
}
