using MongoDB.Bson;
using MongoDB.Driver;
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
using TFG_DavidGomez.Clases.Conexion;
using TFG_DavidGomez.Clases.Conexion.TFG_DavidGomez;

namespace TFG_DavidGomez.Sesion
{
    public partial class Registrarse : Form
    {
        public Registrarse()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los datos del formulario
                string nombre = txUsuario.Text.Trim();
                string DNI = txDNI.Text.Trim();
                string apellido = txApellidos.Text.Trim();
                string contraseña = TxContrasena.Text.Trim();
                string Telf = txTelf.Text.Trim();
                string Correo = txCorreo.Text.Trim();

                // Validar que todos los campos estén llenos
                if (string.IsNullOrEmpty(nombre) ||
                    string.IsNullOrEmpty(apellido) ||
                    string.IsNullOrEmpty(DNI) ||
                    string.IsNullOrEmpty(contraseña) ||
                    string.IsNullOrEmpty(Telf) ||
                    string.IsNullOrEmpty(Correo))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear un documento para insertar en MongoDB
                var nuevoUsuario = new BsonDocument
                {
                    { "nombre", nombre },
                    { "DNI", DNI },
                    { "apellido", apellido },
                    { "contraseña", contraseña },// Nota: Idealmente, la contraseña debe ser cifrada
                    { "Rol", "Padre"},
                    { "Telefono", Telf },
                    { "Correo", Correo }

                };

                // Insertar el documento en la base de datos
                IMongoDatabase _database = ConBD2.ObtenerConexionActiva();
                var usuariosCollection = _database.GetCollection<BsonDocument>("Usuarios");
                usuariosCollection.InsertOne(nuevoUsuario);

                // Mostrar mensaje de éxito
                MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos del formulario
                txUsuario.Clear();
                txDNI.Clear();
                txApellidos.Clear();
                TxContrasena.Clear();
                txTelf.Clear();
                txCorreo.Clear();

                // Cerrar el formulario si es necesario
                this.Close();
                //InicioSesion inicio = new InicioSesion();
                //inicio.VerificarInstancia();
            }
            catch (Exception ex)
            {
                // Manejar errores
                MessageBox.Show($"Ocurrió un error al registrar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void VerificarInstancia()
        {
            object obj = new Registrarse();

            if (obj is Registrarse Regis)
            {
                Console.WriteLine("El objeto es una instancia de PadresForm.");
                Regis.ShowDialog();
            }
            else
            {
                Console.WriteLine("El objeto no es una instancia de PadresForm.");
            }
        }
    }
}
