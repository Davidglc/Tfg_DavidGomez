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
using System.Drawing.Drawing2D;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


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
            RedondearBoton(btnGuardar,20);
            RedondearBoton(BtnGuardarMoni,20);
            BtnGuardarMoni.Visible = false;
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

                // Validar campos obligatorios
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(DNI) ||
                    string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(Telf) || string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(direccion))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato de teléfono
                if (!long.TryParse(Telf, out _))
                {
                    MessageBox.Show("El teléfono debe contener solo números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Validar formato multinacional de documento
                if (!ValidarDocumentoMultinacional(DNI))
                {
                    MessageBox.Show("El documento de identidad no tiene un formato reconocido.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                //// Validar formato de DNI
                //if (!System.Text.RegularExpressions.Regex.IsMatch(DNI, @"^\d{8}[A-Za-z]$"))
                //{
                //    MessageBox.Show("El DNI debe contener 8 números seguidos de una sola letra al final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                if (!System.Text.RegularExpressions.Regex.IsMatch(Correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("El formato del correo electrónico no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Encriptar la contraseña
                string contraseñaEncriptada = EncriptarSHA256(contraseña);

                // Conexión a MariaDB
                ConMDB con = new ConMDB();
                con.AbrirConexion();

                // Verificar si el usuario ya existe
                string queryVerificar = "SELECT COUNT(*) FROM Usuarios WHERE dni = @dni";
                using (var cmdVerificar = new MySqlCommand(queryVerificar, con.ObtenerConexion()))
                {
                    cmdVerificar.Parameters.AddWithValue("@dni", DNI);
                    int existe = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (existe > 0)
                    {
                        MessageBox.Show("Ya existe un usuario registrado con el mismo DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        con.CerrarConexion();
                        return;
                    }
                }

                // Insertar usuario nuevo
                string queryInsert = @"INSERT INTO Usuarios (nombre, apellidos, dni, contrasena, tipo, telefono, correo, direccion)
                               VALUES (@nombre, @apellidos, @dni, @contrasena, 'padre', @telefono, @correo, @direccion)";
                using (var cmdInsert = new MySqlCommand(queryInsert, con.ObtenerConexion()))
                {
                    cmdInsert.Parameters.AddWithValue("@nombre", nombre);
                    cmdInsert.Parameters.AddWithValue("@apellidos", apellido);
                    cmdInsert.Parameters.AddWithValue("@dni", DNI);
                    cmdInsert.Parameters.AddWithValue("@contrasena", contraseñaEncriptada);
                    cmdInsert.Parameters.AddWithValue("@telefono", Telf);
                    cmdInsert.Parameters.AddWithValue("@correo", Correo);
                    cmdInsert.Parameters.AddWithValue("@direccion", direccion);

                    cmdInsert.ExecuteNonQuery();
                }

                con.CerrarConexion();

                MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos del formulario
                txUsuario.Clear();
                txDNI.Clear();
                txApellidos.Clear();
                TxContrasena.Clear();
                txTelf.Clear();
                txCorreo.Clear();
                TxDirec.Clear();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDocumentoMultinacional(string doc)
        {
            if (string.IsNullOrWhiteSpace(doc))
                return false;

            var patrones = new[]
            {
                // España: 8 dígitos + letra
                @"^\d{8}[A-Za-z]$",

                // México:
                // – CURP: 4 letras + 6 dígitos + 8 alfanuméricos
                @"^[A-Z]{4}\d{6}[A-Z0-9]{8}$",
                // – RFC (opcionalmente homoclave): 3 o 4 letras + 6 dígitos + 3 alfanuméricos
                @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$",

                // Argentina: 7 u 8 dígitos
                @"^\d{7,8}$",

                // Chile (RUT): 7 u 8 dígitos + guión + dígito o K
                @"^\d{7,8}-[0-9Kk]$",

                // Colombia (Cédula): 6 a 10 dígitos
                @"^\d{6,10}$",

                // Brasil (CPF):
                // – Formato con puntos y guión: 000.000.000-00
                @"^\d{3}\.\d{3}\.\d{3}-\d{2}$",
                // – Sólo dígitos: 11 dígitos seguidos
                @"^\d{11}$",

                // Venezuela (Cédula):
                // – Con prefijo V- o E-: V-12345678 o E-12345678
                @"^[VE]-\d{7,8}$",
                // – Sólo dígitos (sin prefijo): 7 u 8 dígitos
                @"^\d{7,8}$"
            };

            foreach (var pat in patrones)
            {
                if (Regex.IsMatch(doc, pat, RegexOptions.IgnoreCase))
                    return true;
            }

            return false;
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

                // Validar campos obligatorios
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(DNI) ||
                    string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(Telf) || string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(direccion))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!long.TryParse(Telf, out _))
                {
                    MessageBox.Show("El teléfono debe contener solo números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato multinacional de documento
                if (!ValidarDocumentoMultinacional(DNI))
                {
                    MessageBox.Show("El documento de identidad no tiene un formato reconocido.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                ConMDB con = new ConMDB();
                con.AbrirConexion();

                // Verificar si ya existe el usuario con el mismo DNI
                string queryVerificar = "SELECT COUNT(*) FROM Usuarios WHERE dni = @dni";
                using (var cmdVerificar = new MySqlCommand(queryVerificar, con.ObtenerConexion()))
                {
                    cmdVerificar.Parameters.AddWithValue("@dni", DNI);
                    int existe = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (existe > 0)
                    {
                        MessageBox.Show("Ya existe un usuario registrado con el mismo DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        con.CerrarConexion();
                        return;
                    }
                }


                string contraseñaEncriptada = EncriptarSHA256(contraseña);

                // Insertar nuevo monitor
                string queryInsert = @"INSERT INTO Usuarios (nombre, apellidos, dni, contrasena, tipo, telefono, correo, direccion)
                               VALUES (@nombre, @apellidos, @dni, @contrasena, 'monitor', @telefono, @correo, @direccion)";
                using (var cmdInsert = new MySqlCommand(queryInsert, con.ObtenerConexion()))
                {
                    cmdInsert.Parameters.AddWithValue("@nombre", nombre);
                    cmdInsert.Parameters.AddWithValue("@apellidos", apellido);
                    cmdInsert.Parameters.AddWithValue("@dni", DNI);
                    cmdInsert.Parameters.AddWithValue("@contrasena", contraseñaEncriptada);
                    cmdInsert.Parameters.AddWithValue("@telefono", Telf);
                    cmdInsert.Parameters.AddWithValue("@correo", Correo);
                    cmdInsert.Parameters.AddWithValue("@direccion", direccion);

                    cmdInsert.ExecuteNonQuery();
                }

                con.CerrarConexion();

                MessageBox.Show("Monitor registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos
                txUsuario.Clear();
                txDNI.Clear();
                txApellidos.Clear();
                TxContrasena.Clear();
                txTelf.Clear();
                txCorreo.Clear();
                TxDirec.Clear();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el monitor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

