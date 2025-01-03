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
            BtnGuardarMoni.Visible = false;
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
                string direccion = TxDirec.Text.Trim();

                // Validar que todos los campos estén llenos
                if (string.IsNullOrEmpty(nombre) ||
                    string.IsNullOrEmpty(apellido) ||
                    string.IsNullOrEmpty(DNI) ||
                    string.IsNullOrEmpty(contraseña) ||
                    string.IsNullOrEmpty(Telf) ||
                    string.IsNullOrEmpty(Correo) ||
                    string.IsNullOrEmpty(direccion))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato del teléfono (solo números)
                if (!long.TryParse(Telf, out _))
                {
                    MessageBox.Show("El teléfono debe contener solo números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato del DNI (solo una letra al final)
                if (!System.Text.RegularExpressions.Regex.IsMatch(DNI, @"^\d{8}[A-Za-z]$"))
                {
                    MessageBox.Show("El DNI debe contener 8 números seguidos de una sola letra al final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Conectar con la base de datos
                IMongoDatabase _database = ConBD2.ObtenerConexionActiva();
                var usuariosCollection = _database.GetCollection<BsonDocument>("Usuarios");

                // Validar si ya existe un usuario con el mismo DNI
                var filtroDNI = Builders<BsonDocument>.Filter.Eq("DNI", DNI);
                var usuarioExistente = usuariosCollection.Find(filtroDNI).FirstOrDefault();

                if (usuarioExistente != null)
                {
                    MessageBox.Show("Ya existe un padre registrado con el mismo DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear un documento para insertar en MongoDB
                var nuevoUsuario = new BsonDocument
                {
                    { "_id", ObjectId.GenerateNewId() }, // Generar un nuevo ObjectId
                    { "Nombre", nombre },
                    { "DNI", DNI },
                    { "Apellidos", apellido },
                    { "Contrasena", contraseña }, // Nota: Idealmente, la contraseña debe ser cifrada
                    { "Rol", "Padre" },
                    { "Telefono", Telf },
                    { "Correo", Correo },
                    { "Direccion", direccion },
                    { "FechaRegistro", DateTime.Now }
                };

                // Insertar el documento en la base de datos
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
                TxDirec.Clear();

                // Cerrar el formulario si es necesario
                this.Close();
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

        private void BtnGuardarMoni_Click(object sender, EventArgs e)
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
                string direccion = TxDirec.Text.Trim();

                // Validar que todos los campos estén llenos
                if (string.IsNullOrEmpty(nombre) ||
                    string.IsNullOrEmpty(apellido) ||
                    string.IsNullOrEmpty(DNI) ||
                    string.IsNullOrEmpty(contraseña) ||
                    string.IsNullOrEmpty(Telf) ||
                    string.IsNullOrEmpty(Correo) ||
                    string.IsNullOrEmpty(direccion))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato del teléfono (solo números)
                if (!long.TryParse(Telf, out _))
                {
                    MessageBox.Show("El teléfono debe contener solo números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato del DNI (solo una letra al final)
                if (!System.Text.RegularExpressions.Regex.IsMatch(DNI, @"^\d{8}[A-Za-z]$"))
                {
                    MessageBox.Show("El DNI debe contener 8 números seguidos de una sola letra al final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Conectar con la base de datos
                IMongoDatabase _database = ConBD2.ObtenerConexionActiva();
                var usuariosCollection = _database.GetCollection<BsonDocument>("Usuarios");

                // Validar si ya existe un usuario con el mismo DNI
                var filtroDNI = Builders<BsonDocument>.Filter.Eq("DNI", DNI);
                var usuarioExistente = usuariosCollection.Find(filtroDNI).FirstOrDefault();

                if (usuarioExistente != null)
                {
                    MessageBox.Show("Ya existe un monitor registrado con el mismo DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear un documento para insertar en MongoDB
                var nuevoUsuario = new BsonDocument
                {
                    { "_id", ObjectId.GenerateNewId() }, // Generar un nuevo ObjectId
                    { "Nombre", nombre },
                    { "DNI", DNI },
                    { "Apellidos", apellido },
                    { "Contrasena", contraseña }, // Nota: Idealmente, la contraseña debe ser cifrada
                    { "Rol", "Monitor" },
                    { "Telefono", Telf },
                    { "Correo", Correo },
                    { "Direccion", direccion },
                    { "FechaRegistro", DateTime.Now }
                };

                // Insertar el documento en la base de datos
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
                TxDirec.Clear();

                // Cerrar el formulario si es necesario
                this.Close();
            }
            catch (Exception ex)
            {
                // Manejar errores
                MessageBox.Show($"Ocurrió un error al registrar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

