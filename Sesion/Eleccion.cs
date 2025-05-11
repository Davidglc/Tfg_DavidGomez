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
using TFG_DavidGomez.Clases.Adaptador;

namespace TFG_DavidGomez.Sesion
{
    public partial class Eleccion : Form
    {
        private DateTime mesActual = DateTime.Now;
        private List<Actividades> actividadesDelMes = new List<Actividades>(); // Cargadas de la BD
        MariaDbAdapter mdba;


        public Eleccion()
        {
            InitializeComponent();
            mdba = new MariaDbAdapter();
        }

        private void CargarActividadesDelMes()
        {
            PanelBotones.Controls.Clear();

            // Suponiendo que obtienes actividades desde tu base de datos
            List<Actividades> actividades = mdba.ObtenerActividadesDelMes(mesActual);

            foreach (var actividad in actividades)
            {
                if (actividad.Fecha.Date < DateTime.Today) continue; // Ignorar fechas pasadas

                Button btn = new Button();
                btn.Width = 150;
                btn.Height = 180;
                btn.Margin = new Padding(10);
                btn.Tag = actividad; // Guardar el objeto para luego

                btn.Text = $"{actividad.Nombre}\n{actividad.Fecha.ToShortDateString()}";
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.Font = new Font("Segoe UI", 9);
                btn.ImageAlign = ContentAlignment.TopCenter;
                btn.TextImageRelation = TextImageRelation.ImageAboveText;

                if (actividad.Imagen != null)
                {
                    using (var ms = new MemoryStream(actividad.Imagen))
                    {
                        Image originalImage = Image.FromStream(ms);
                        Image resizedImage = new Bitmap(originalImage, new Size(140, 100));
                        btn.Image = resizedImage;

                    }
                }

                btn.Click += AbrirActividad_Click;

                PanelBotones.Controls.Add(btn);
            }

            // Mostrar u ocultar botones de navegación
            btn_izq.Visible = mesActual > DateTime.Now.AddMonths(-1);
        }


        private void añadirNiñoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btn_izq_Click(object sender, EventArgs e)
        {
            if (mesActual > DateTime.Now)
            {
                mesActual = mesActual.AddMonths(-1);
                CargarActividadesDelMes();
            }
        }

        private void btn_drch_Click(object sender, EventArgs e)
        {
            mesActual = mesActual.AddMonths(1);
            CargarActividadesDelMes();
        }

        private void AbrirActividad_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is Actividades actividad)
            {
                Actividad formActividad = new Actividad();
                formActividad.CargarDatos(actividad);
                formActividad.ShowDialog();
            }
        }
    }
}
