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
using TFG_DavidGomez.Sesion;

namespace TFG_DavidGomez
{
    public partial class PadresForm : Form
    {
        public PadresForm()
        {
            InitializeComponent();
        }

        private void añadirNiñoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisNino formAnadirNino = new RegisNino();

            formAnadirNino.ShowDialog();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = e.Start;

            List<string> actividades = ObtenerActividadesPorFecha(selectedDate);

            Actividades.Items.Clear();

            foreach (string actividad in actividades)
            {
                Actividades.Items.Add(actividad);
            }
        }

        public void AgregarNiño(Nino Niño)
        {
            LbNinos.Items.Add(Niño);
        }

        private List<string> ObtenerActividadesPorFecha(DateTime fecha)
        {

            Dictionary<DateTime, List<string>> actividadesPorFecha = new Dictionary<DateTime, List<string>>
    {
        { new DateTime(2024, 11, 25), new List<string> { "Fútbol", "Baloncesto" } },
        { new DateTime(2024, 11, 26), new List<string> { "Natación", "Pintura" } },
    };

            if (actividadesPorFecha.ContainsKey(fecha))
            {
                return actividadesPorFecha[fecha];
            }

            return new List<string>();
        }

    }
}
