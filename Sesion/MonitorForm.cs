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

namespace TFG_DavidGomez
{
    public partial class MonitorForm : Form
    {
        public MonitorForm()
        {
            InitializeComponent();
        }

        public MonitorForm(DateTime fecha, string actividad, List<string> materiales, List<string> niños)
        {
            InitializeComponent();

            LbFecha2.Text = fecha.ToString("dd/MM/yyyy");
            LbAtividad2.Text = actividad;

            // Cargar materiales en el ListBox
            foreach (var material in materiales)
            {
                LbMateriales.Items.Add(material);
            }

            foreach (var niño in niños)
            {
                LbNiños.Items.Add(niño);
            }
        }

        public void VerificarInstancia()
        {
            object obj = new MonitorForm();

            if (obj is MonitorForm monitorForm)
            {
                Console.WriteLine("El objeto proporcionado es de tipo MonitorForm.");
                monitorForm.Show();
            }
            else
            {
                Console.WriteLine("El objeto proporcionado no es de tipo MonitorForm.");
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SesionIniciada si = new SesionIniciada();
            si.CerrarSesion();
            this.Close();
            InicioSesion inicioSesion = new InicioSesion();
            inicioSesion.ShowDialog();
        }

        private void datosPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void añadirMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }

}
