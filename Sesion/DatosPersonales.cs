using MongoDB.Bson;
using MongoDB.Driver;
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

                MariaDbAdapter ma = new MariaDbAdapter();
                List<Nino> ninos = ma.CargarDatosNinoPorPadre(idPadre);

                if (ninos == null || ninos.Count == 0)
                {
                    MessageBox.Show("No se encontraron niños asociados con este padre.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener referencia a la clase con el ListBox y cargar los datos
                DatosPersonalesNinos datosNinosForm = new DatosPersonalesNinos(); // Asumiendo que esta es la clase con el ListBox

                foreach (var nino in ninos)
                {
                    datosNinosForm.LbNinos.Items.Add($"Nombre: {nino.Nombre}, Apellidos: {nino.Apellidos}, DNI: {nino.DNI}, Fecha de Nacimiento: {nino.FechaNacimiento.ToShortDateString()}, Edad: {nino.Edad}");
                }

                // Mostrar el formulario
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
                // Obtener el ID del usuario desde la sesión
                int idUsuario = int.Parse(SesionIniciada.IdUsuario);

                // Crear una instancia de MongoDBAdapter
                MariaDbAdapter ma = new MariaDbAdapter();

                // Verificar si el usuario existe en la base de datos
                Usuario usuarioExistente = ma.ObtenerUsuarioPorId(idUsuario);

                if (usuarioExistente == null)
                {
                    MessageBox.Show("No se encontró el usuario en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Modificar los datos del usuario con los valores de los TextBox
                usuarioExistente.Nombre = txUsuario.Text;
                usuarioExistente.Apellidos = txApellidos.Text;
                usuarioExistente.DNI = txDNI.Text;
                usuarioExistente.Correo = txCorreo.Text;
                usuarioExistente.Contrasena = EncriptarSHA256(TxContrasena.Text);
                usuarioExistente.Telefono = txTelefono.Text;

                // Actualizar el usuario en la base de datos
                var usuariosCollection = ConBD2.GetCollection<Usuario>("Usuarios");
                var filtro = Builders<Usuario>.Filter.Eq(u => u.Id, idUsuario);
                var actualizacion = Builders<Usuario>.Update
                    .Set(u => u.Nombre, usuarioExistente.Nombre)
                    .Set(u => u.Apellidos, usuarioExistente.Apellidos)
                    .Set(u => u.DNI, usuarioExistente.DNI)
                    .Set(u => u.Correo, usuarioExistente.Correo)
                    .Set(u => u.Contrasena, usuarioExistente.Contrasena)
                    .Set(u => u.Telefono, usuarioExistente.Telefono)
                    .Set(u => u.Direccion, usuarioExistente.Direccion);

                var resultado = usuariosCollection.UpdateOne(filtro, actualizacion);

                if (resultado.ModifiedCount > 0)
                {
                    MessageBox.Show("Los datos han sido actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la información.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        /// <summary>
        /// Evento para guardar los datos actualizados de un Monitor.
        /// </summary>
        private void BtnGuardarMoni_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del usuario desde la sesión
                int idUsuario = int.Parse(SesionIniciada.IdUsuario);

                // Crear una instancia de MongoDBAdapter
                MariaDbAdapter ma = new MariaDbAdapter();

                // Verificar si el usuario existe en la base de datos
                UsuarioMonitor usuarioExistente = ma.ObtenerUsuarioPorIdMoni(idUsuario);

                if (usuarioExistente == null)
                {
                    MessageBox.Show("No se encontró el usuario en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Modificar los datos del usuario con los valores de los TextBox
                usuarioExistente.Nombre = txUsuario.Text;
                usuarioExistente.Apellidos = txApellidos.Text;
                usuarioExistente.DNI = txDNI.Text;
                usuarioExistente.Correo = txCorreo.Text;
                usuarioExistente.Contrasena = EncriptarSHA256(TxContrasena.Text);
                usuarioExistente.Telefono = txTelefono.Text;

                // Actualizar el usuario en la base de datos
                var usuariosCollection = ConBD2.GetCollection<Usuario>("Usuarios");
                var filtro = Builders<Usuario>.Filter.Eq(u => u.Id, idUsuario);
                var actualizacion = Builders<Usuario>.Update
                    .Set(u => u.Nombre, usuarioExistente.Nombre)
                    .Set(u => u.Apellidos, usuarioExistente.Apellidos)
                    .Set(u => u.DNI, usuarioExistente.DNI)
                    .Set(u => u.Correo, usuarioExistente.Correo)
                    .Set(u => u.Contrasena, usuarioExistente.Contrasena)
                    .Set(u => u.Telefono, usuarioExistente.Telefono)
                    .Set(u => u.Direccion, usuarioExistente.Direccion);

                var resultado = usuariosCollection.UpdateOne(filtro, actualizacion);

                if (resultado.ModifiedCount > 0)
                {
                    MessageBox.Show("Los datos han sido actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar la información.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
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

