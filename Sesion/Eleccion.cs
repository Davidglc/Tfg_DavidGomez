using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFG_DavidGomez.Sesion
{
    public partial class Eleccion : Form
    {
        public string Elecciones { get; private set; } // Propiedad para guardar la elección

        public Eleccion()
        {
            InitializeComponent();
            Elecciones = null; // Inicializamos como null
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Elecciones = "Monitor";
            this.DialogResult = DialogResult.OK; // Cierra el popup y marca resultado como OK
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Elecciones = "Padre";
            this.DialogResult = DialogResult.OK; // Cierra el popup y marca resultado como OK
            this.Close();
        }
    }
}
