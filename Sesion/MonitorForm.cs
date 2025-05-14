using MongoDB.Bson;
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
using TFG_DavidGomez.Sesion;

namespace TFG_DavidGomez
{

    /// <summary>
    /// Clase que representa el formulario de gestión para monitores.
    /// Proporciona funcionalidades como visualizar actividades, materiales asociados, niños asignados,
    /// consultar datos personales y gestionar monitores.
    /// </summary>
    public partial class MonitorForm : Form
    {

        /// <summary>
        /// Constructor por defecto de la clase.
        /// Inicializa el formulario de Monitor.
        /// </summary>
        public MonitorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor que inicializa el formulario con información específica de una actividad.
        /// </summary>
        /// <param name="fecha">Fecha de la actividad.</param>
        /// <param name="actividad">Nombre de la actividad.</param>
        /// <param name="materiales">Lista de materiales necesarios para la actividad.</param>
        /// <param name="niños">Lista de niños inscritos en la actividad.</param>

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

        /// <summary>
        /// Verifica si un objeto es una instancia de la clase <see cref="MonitorForm"/>.
        /// Si lo es, muestra el formulario.
        /// </summary>

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


        /// <summary>
        /// Evento que se ejecuta al seleccionar "Cerrar sesión" en el menú.
        /// Cierra la sesión actual y redirige al formulario de inicio de sesión.
        /// </summary>
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cerrar sesión
            SesionIniciada si = new SesionIniciada();
            si.CerrarSesion();
            this.Close();

            // Crear una nueva instancia del formulario de inicio de sesión
            InicioSesion inicioSesion = new InicioSesion();

            // Limpiar los campos de usuario y contraseña
            inicioSesion.LimpiarCampos();

            // Mostrar el formulario de inicio de sesión
            inicioSesion.VerificarInstancia();

        }


        /// <summary>
        /// Evento que se ejecuta al seleccionar "Datos personales" en el menú.
        /// Carga y muestra los datos personales del monitor desde la base de datos.
        /// </summary>
        private void datosPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del usuario desde la sesión
                // Obtener el ID del usuario desde la sesión
                int id = int.Parse(SesionIniciada.IdUsuario);

                // Crear una instancia de MongoDBAdapter
                MariaDbAdapter ma = new MariaDbAdapter();

                // Obtener el usuario desde la base de datos
                UsuarioMonitor u = ma.ObtenerUsuarioPorIdMoni(id);

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


        /// <summary>
        /// Evento que se ejecuta al seleccionar "Añadir monitor" en el menú.
        /// Abre el formulario de registro con la opción específica para registrar monitores.
        /// </summary>

        private void añadirMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registrarse registrarse = new Registrarse();
            registrarse.BtnGuardarMoni.Visible = true;
            registrarse.btnGuardar.Visible = false;
            registrarse.ShowDialog();
        }

        private void modificarActividadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //private void CerrarAplicacion(object sender, FormClosedEventArgs e)
        //{
        //    Application.Exit();
        //}


    }

}
