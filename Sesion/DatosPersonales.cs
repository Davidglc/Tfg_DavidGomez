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
using TFG_DavidGomez.Clases.Adaptador;
using TFG_DavidGomez.Clases.Conexion;

namespace TFG_DavidGomez.Sesion
{
    public partial class DatosPersonales : Form
    {
        public DatosPersonales()
        {
            InitializeComponent();
        }

        public DatosPersonales(Usuario usuario)
        {
            InitializeComponent();

            // Asignar los datos a los controles
            txUsuario.Text = usuario.Nombre;
            txDNI.Text = usuario.DNI;
            TxContrasena.Text = usuario.Contrasena;
            txApellidos.Text = usuario.Apellidos;
            txCorreo.Text = usuario.Correo;
            txTelefono.Text = usuario.Telefono;
        }

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

                ObjectId idPadre = ObjectId.Parse(idPadreTexto);

                MongoDBAdapter ma = new MongoDBAdapter();
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del usuario desde la sesión
                var idUsuario = ObjectId.Parse(SesionIniciada.IdUsuario);

                // Crear una instancia de MongoDBAdapter
                MongoDBAdapter ma = new MongoDBAdapter();

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
                usuarioExistente.Contrasena = TxContrasena.Text;
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
                    .Set(u => u.Telefono,usuarioExistente.Telefono);

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

    }
}

