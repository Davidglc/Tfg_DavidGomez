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

        private void datosPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del usuario desde la sesión
                var idUsuario = ObjectId.Parse(SesionIniciada.IdUsuario);

                // Instancia del adaptador
                MongoDBAdapter ma = new MongoDBAdapter();

                // Obtener el usuario como BsonDocument
                var usuario = ma.ObtenerUsuarioComoBsonDocument(idUsuario);

                if (usuario == null)
                {
                    MessageBox.Show("No se encontró información para este monitor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Inspeccionar el documento (opcional)
                Console.WriteLine($"Documento encontrado: {usuario.ToJson()}");

                // Usar los datos del documento
                var nombre = usuario["Nombre"].AsString;
                var apellidos = usuario["Apellidos"].AsString;
                var dni = usuario["DNI"].AsString;
                var correo = usuario["Correo"].AsString;
                var contrasena = usuario["Contrasena"].AsString;
                var rol = usuario["Rol"].AsString;
                var fechaRegistro = usuario["FechaRegistro"].ToUniversalTime();
                var telefono = usuario["Telefono"].AsString;
                var direccion = usuario["Direccion"].AsString;

                // Mostrar los datos en el formulario
                DatosPersonales dp = new DatosPersonales();
                Usuario u = new Usuario(nombre,apellidos,dni,correo,contrasena,fechaRegistro,telefono,direccion);
                dp.VerificarInstancia2(u);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos personales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void añadirMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registrarse registrarse = new Registrarse();
            registrarse.BtnGuardarMoni.Visible = true;
            registrarse.btnGuardar.Visible = false;
            registrarse.ShowDialog();
        }

        //private void CerrarAplicacion(object sender, FormClosedEventArgs e)
        //{
        //    Application.Exit();
        //}


    }

}
