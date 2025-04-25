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
using System.Security.Cryptography;


namespace TFG_DavidGomez.Sesion
{

    /// <summary>
    /// Clase que representa el formulario para registrar usuarios en la aplicación.
    /// Ofrece funcionalidades para registrar diferentes roles de usuario, como "Padre" o "Monitor",
    /// validando sus datos y almacenándolos en una base de datos MongoDB.
    /// </summary>

    public partial class Registrarse : Form
    {

        /// <summary>
        /// Constructor de la clase.
        /// Inicializa el formulario y configura la visibilidad de elementos del formulario según las necesidades.
        /// </summary>
        public Registrarse()
        {
            InitializeComponent();
            BtnGuardarMoni.Visible = false;
        }


        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de guardar para registrar un usuario con el rol de "Padre".
        /// Valida los datos ingresados y los guarda en la base de datos si son correctos.
        /// </summary>
        /// <param name="sender">El objeto que genera el evento.</param>
        /// <param name="e">Los datos del evento.</param>
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

                // Validar formato del DNI (8 números seguidos de una letra)
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
                    MessageBox.Show("Ya existe un usuario registrado con el mismo DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Encriptar la contraseña con SHA-256
                string contraseñaEncriptada = EncriptarSHA256(contraseña);

                // Crear un documento para insertar en MongoDB
                var nuevoUsuario = new BsonDocument
                {
                    { "_id", ObjectId.GenerateNewId() }, // Generar un nuevo ObjectId
                    { "Nombre", nombre },
                    { "DNI", DNI },
                    { "Apellidos", apellido },
                    { "Contrasena", contraseñaEncriptada }, // Guardar la contraseña encriptada
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

        /// <summary>
        /// Verifica si el objeto actual es una instancia de la clase <see cref="Registrarse"/>.
        /// Si lo es, muestra el formulario en un cuadro de diálogo.
        /// </summary>
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

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de guardar para registrar un usuario con el rol de "Monitor".
        /// Valida los datos ingresados y los guarda en la base de datos si son correctos.
        /// </summary>
        /// <param name="sender">El objeto que genera el evento.</param>
        /// <param name="e">Los datos del evento.</param>
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
                    MessageBox.Show("Ya existe un usuario registrado con el mismo DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string contraseñaEncriptada = EncriptarSHA256(contraseña);

                // Crear un documento para insertar en MongoDB
                var nuevoUsuario = new BsonDocument
                {
                    { "_id", ObjectId.GenerateNewId() }, // Generar un nuevo ObjectId
                    { "Nombre", nombre },
                    { "DNI", DNI },
                    { "Apellidos", apellido },
                    { "Contrasena", contraseñaEncriptada }, // Nota: Idealmente, la contraseña debe ser cifrada
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

        // Método para encriptar con SHA-256
        private string EncriptarSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}

