using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
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
        private DateTime mesActual = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        private List<Actividades> actividadesDelMes = new List<Actividades>(); // Cargadas de la BD
        MariaDbAdapter mdba;


        public Eleccion()
        {
            InitializeComponent();
            mdba = new MariaDbAdapter();
            PanelBotones.AutoScroll = true;
            RedondearBoton(btn_drch, 20);
            RedondearBoton(btn_izq, 20);
            CargarActividadesDelMes();
        }

        private void CargarActividadesDelMes()
        {
            PanelBotones.Controls.Clear();

            List<Actividades> actividades = mdba.ObtenerActividadesDelMes(mesActual);

            foreach (var actividad in actividades)
            {
                if (actividad.Fecha.Date < DateTime.Today) continue;

                Button btn = new Button
                {
                    Width = 150,
                    Height = 180,
                    Margin = new Padding(10),
                    Tag = actividad,
                    Text = $"{actividad.Nombre}\n{actividad.Fecha.ToShortDateString()}",
                    TextAlign = ContentAlignment.BottomCenter,
                    Font = new Font("Segoe UI", 9),
                    ImageAlign = ContentAlignment.TopCenter,
                    TextImageRelation = TextImageRelation.ImageAboveText
                };

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

            // Mostrar/ocultar flecha izquierda
            btn_izq.Visible = mesActual > new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        }



        private void añadirNiñoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisNino formNino = new RegisNino(); // Este es el formulario para registrar niños
            formNino.ShowDialog(); // Abre el formulario como ventana modal
        }

        private void btn_izq_Click(object sender, EventArgs e)
        {
            DateTime mesActualSistema = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            if (mesActual > mesActualSistema)
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

        private void editarDatosPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                // Obtener el ID del usuario desde la sesión
                int id = int.Parse(SesionIniciada.IdUsuario);

                // Crear una instancia de MongoDBAdapter
                MariaDbAdapter ma = new MariaDbAdapter();

                // Obtener el usuario desde la base de datos
                Usuario u = ma.ObtenerUsuarioPorId(id);

                // Validar si el usuario fue encontrado
                if (u == null)
                {
                    MessageBox.Show("No se encontró información para este usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mostrar los datos personales del usuario
                DatosPersonales dp = new DatosPersonales(u);
                dp.VerificarInstancia2(u);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos personales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SesionIniciada si = new SesionIniciada();
            si.CerrarSesion();
            this.Close();
            InicioSesion inicioSesion = new InicioSesion();
            inicioSesion.ShowDialog();
        }

        private void actividadesApuntadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PadresForm pf = new PadresForm();
            pf.ShowDialog();
        }

        private void RedondearBoton(Button btn, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(btn.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(btn.Width - radio, btn.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, btn.Height - radio, radio, radio, 90, 90);
            path.CloseAllFigures();
            btn.Region = new Region(path);
        }
    }
}
