using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TFG_DavidGomez.Clases;
using TFG_DavidGomez.Clases.Adaptador;
using TFG_DavidGomez.Clases.Conexion;

namespace TFG_DavidGomez.Sesion
{

    /// <summary>
    /// Formulario para gestionar los datos personales de los usuarios.
    /// Permite cargar, visualizar y actualizar la información de los usuarios en una base de datos MongoDB.
    /// </summary> 
    public partial class DatosPersonales : Form
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public DatosPersonales()
        {
            InitializeComponent();
            BtnGuardarMoni.Visible = false;
        }


        /// <summary>
        /// Constructor para inicializar datos de un usuario general.
        /// </summary>
        /// <param name="usuario">Objeto de tipo Usuario con los datos a cargar en el formulario.</param>
        public DatosPersonales(Usuario usuario)
        {
            InitializeComponent();

            // Asignar los datos a los controles
            txUsuario.Text = usuario.Nombre;
            txDNI.Text = usuario.DNI;
            TxContrasena.Text = "";//usuario.Contrasena;
            txApellidos.Text = usuario.Apellidos;
            txCorreo.Text = usuario.Correo;
            txTelefono.Text = usuario.Telefono;
            txDirec.Text = usuario.Direccion;
            BtnGuardarMoni.Visible = false;
            btnGuardar.Visible = true;
            RedondearBoton(btnGuardar, 20);
            VerificarVisibilidadDatosNinos();
        }


        /// <summary>
        /// Constructor para inicializar datos de un monitor.
        /// </summary>
        /// <param name="usuario">Objeto de tipo UsuarioMonitor con los datos a cargar en el formulario.</param>
        public DatosPersonales(UsuarioMonitor usuario)
        {
            InitializeComponent();

            // Asignar los datos a los controles
            txUsuario.Text = usuario.Nombre;
            txDNI.Text = usuario.DNI;
            TxContrasena.Text = "";//usuario.Contrasena;
            txApellidos.Text = usuario.Apellidos;
            txCorreo.Text = usuario.Correo;
            txTelefono.Text = usuario.Telefono;
            txDirec.Text = usuario.Direccion;
            BtnGuardarMoni.Visible = true;
            btnGuardar.Visible = false;
            datosNiñosToolStripMenuItem.Visible = false;
            RedondearBoton(BtnGuardarMoni, 20);
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
        /// Evento para mostrar los datos de los niños asociados al usuario actual.
        /// </summary>
        private void datosNiñosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string idPadreTexto = SesionIniciada.IdUsuario;

                if (string.IsNullOrEmpty(idPadreTexto))
                {
                    MessageBox.Show("No hay una sesión activa. Por favor, inicie sesión.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPadre = int.Parse(idPadreTexto);

                // Abrir el formulario y pasar el ID del padre
                DatosPersonalesNinos datosNinosForm = new DatosPersonalesNinos(idPadre);
                datosNinosForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Método para verificar si un objeto es una instancia de la clase DatosPersonales.
        /// </summary>
        public void VerificarInstancia()
        {
            object obj = new DatosPersonales();

            if (obj is DatosPersonales dp)
            {
                Console.WriteLine("El objeto proporcionado es de tipo MonitorForm.");
                dp.Show();
            }
            else
            {
                Console.WriteLine("El objeto proporcionado no es de tipo MonitorForm.");
            }
        }

        /// <summary>
        /// Sobrecarga de VerificarInstancia para objetos de tipo Usuario.
        /// </summary>
        public void VerificarInstancia2(Usuario u)
        {
            object obj = new DatosPersonales(u);

            if (obj is DatosPersonales dp)
            {
                Console.WriteLine("El objeto proporcionado es de tipo MonitorForm.");
                dp.Show();
            }
            else
            {
                Console.WriteLine("El objeto proporcionado no es de tipo MonitorForm.");
            }
        }


        /// <summary>
        /// Sobrecarga de VerificarInstancia para objetos de tipo UsuarioMonitor.
        /// </summary>
        public void VerificarInstancia2(UsuarioMonitor u)
        {
            object obj = new DatosPersonales(u);

            if (obj is DatosPersonales dp)
            {
                Console.WriteLine("El objeto proporcionado es de tipo MonitorForm.");
                dp.Show();
            }
            else
            {
                Console.WriteLine("El objeto proporcionado no es de tipo MonitorForm.");
            }
        }


        /// <summary>
        /// Evento para guardar los datos actualizados de un usuario.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Obtener el ID del usuario
                if (!int.TryParse(SesionIniciada.IdUsuario, out int idUsuario))
                {
                    MessageBox.Show("ID de usuario inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2) Leer valores de los TextBox
                string nombre = txUsuario.Text.Trim();
                string apellidos = txApellidos.Text.Trim();
                string dni = txDNI.Text.Trim();
                string correo = txCorreo.Text.Trim();
                string telefono = txTelefono.Text.Trim();
                string direccion = txDirec.Text.Trim();
                string nuevaPwd = TxContrasena.Text.Trim();

                // 3) Encriptar contraseña si se ha escrito
                string pwdHash = null;
                bool cambiarPwd = !string.IsNullOrEmpty(nuevaPwd);
                if (cambiarPwd)
                    pwdHash = EncriptarSHA256(nuevaPwd);

                // 4) Armar el SQL dinámicamente
                var sb = new StringBuilder();
                sb.AppendLine("UPDATE Usuarios");
                sb.AppendLine("   SET nombre    = @nombre,");
                sb.AppendLine("       apellidos = @apellidos,");
                sb.AppendLine("       dni       = @dni,");
                sb.AppendLine("       correo    = @correo,");
                if (cambiarPwd)
                    sb.AppendLine("       contrasena= @contrasena,");
                sb.AppendLine("       telefono  = @telefono,");
                sb.AppendLine("       direccion = @direccion");
                sb.AppendLine(" WHERE id        = @id;");

                // 5) Ejecutar
                var con = new ConMDB();
                try
                {
                    con.AbrirConexion();
                    using (var cmd = new MySqlCommand(sb.ToString(), con.ObtenerConexion()))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellidos", apellidos);
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        if (cambiarPwd)
                            cmd.Parameters.AddWithValue("@contrasena", pwdHash);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.Parameters.AddWithValue("@id", idUsuario);

                        int filas = cmd.ExecuteNonQuery();
                        if (filas > 0)
                            MessageBox.Show("Los datos han sido actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No se encontró el usuario o no hubo cambios.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (MySqlException sqlEx)
                {
                    MessageBox.Show($"Error de base de datos: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.CerrarConexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Evento para guardar los datos actualizados de un Monitor.
        /// </summary>
        private void btnGuardarMoni_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Obtener el ID del usuario
                if (!int.TryParse(SesionIniciada.IdUsuario, out int idUsuario))
                {
                    MessageBox.Show("ID de usuario inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2) Leer valores de los TextBox
                string nombre = txUsuario.Text.Trim();
                string apellidos = txApellidos.Text.Trim();
                string dni = txDNI.Text.Trim();
                string correo = txCorreo.Text.Trim();
                string telefono = txTelefono.Text.Trim();
                string direccion = txDirec.Text.Trim();
                string nuevaPwd = TxContrasena.Text.Trim();

                // 3) Encriptar contraseña si se ha escrito
                string pwdHash = null;
                bool cambiarPwd = !string.IsNullOrEmpty(nuevaPwd);
                if (cambiarPwd)
                    pwdHash = EncriptarSHA256(nuevaPwd);

                // 4) Armar el SQL dinámicamente
                var sb = new StringBuilder();
                sb.AppendLine("UPDATE Usuarios");
                sb.AppendLine("   SET nombre    = @nombre,");
                sb.AppendLine("       apellidos = @apellidos,");
                sb.AppendLine("       dni       = @dni,");
                sb.AppendLine("       correo    = @correo,");
                if (cambiarPwd)
                    sb.AppendLine("       contrasena= @contrasena,");
                sb.AppendLine("       telefono  = @telefono,");
                sb.AppendLine("       direccion = @direccion");
                sb.AppendLine(" WHERE id        = @id;");

                // 5) Ejecutar
                var con = new ConMDB();
                try
                {
                    con.AbrirConexion();
                    using (var cmd = new MySqlCommand(sb.ToString(), con.ObtenerConexion()))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellidos", apellidos);
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        if (cambiarPwd)
                            cmd.Parameters.AddWithValue("@contrasena", pwdHash);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.Parameters.AddWithValue("@id", idUsuario);

                        int filas = cmd.ExecuteNonQuery();
                        if (filas > 0)
                            MessageBox.Show("Los datos han sido actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No se encontró el usuario o no hubo cambios.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (MySqlException sqlEx)
                {
                    MessageBox.Show($"Error de base de datos: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.CerrarConexion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Verifica si se deben mostrar los datos de niños para el usuario actual.
        /// </summary>
        private void VerificarVisibilidadDatosNinos()
        {
            try
            {
                // Obtener el ID del usuario desde la sesión
                string idPadreTexto = SesionIniciada.IdUsuario;

                if (string.IsNullOrEmpty(idPadreTexto))
                {
                    // Si no hay sesión activa, ocultar la opción
                    datosNiñosToolStripMenuItem.Visible = false;
                    return;
                }

                int idPadre = int.Parse(idPadreTexto);

                // Instancia de MongoDBAdapter para cargar los datos
                MariaDbAdapter ma = new MariaDbAdapter();
                List<Nino> ninos = ma.CargarDatosNinoPorPadre(idPadre);


                // Mostrar u ocultar la opción dependiendo si hay niños
                datosNiñosToolStripMenuItem.Visible = ninos != null && ninos.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al verificar los datos de los niños: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Ocultar la opción en caso de error
                datosNiñosToolStripMenuItem.Visible = false;
            }
        }

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

