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

namespace TFG_DavidGomez.Sesion
{

    /// <summary>
    /// Formulario para gestionar los datos personales de los niños.
    /// Permite cargar, visualizar y actualizar la información de los niños asociados a un padre en una base de datos MongoDB.
    /// </summary>
    /// 
    public partial class DatosPersonalesNinos : Form
    {
        /// <summary>
        /// Constructor de la clase.
        /// Inicializa los componentes del formulario.
        /// </summary>
        public DatosPersonalesNinos()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Carga los datos de un objeto Nino en los campos del formulario.
        /// </summary>
        /// <param name="nino">Objeto Nino con la información a mostrar.</param>
        public void CargarDatosNino(Nino nino)
        {
            txUsuario.Text = nino.Nombre;
            txDNI.Text = nino.DNI; // Usaremos el ID como DNI (si aplica)
            txApellidos.Text = nino.Apellidos;
            txFnac.Text = nino.FechaNacimiento.ToString("yyyy-MM-dd");
            txEdad.Text = nino.Edad.ToString();
        }

        /// <summary>
        /// Evento que se dispara al hacer doble clic sobre un elemento del ListBox.
        /// Carga la información del niño seleccionado en los campos del formulario.
        /// </summary>
        private void LbNinos_DoubleClick(object sender, EventArgs e)
        {
            // Verificar que haya un elemento seleccionado
            if (LbNinos.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un niño de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el texto del niño seleccionado en el ListBox
            string ninoSeleccionadoText = LbNinos.SelectedItem.ToString();

            // Dividir el texto para extraer los datos (esto dependerá del formato en el ListBox)
            var datosNino = ninoSeleccionadoText.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            if (datosNino.Length != 5) // Verificar que se reciban exactamente 5 partes
            {
                MessageBox.Show("El formato del niño seleccionado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Asignar los valores a los TextBox correspondientes
            txUsuario.Text = datosNino[0].Replace("Nombre: ", " ");
            txApellidos.Text = datosNino[1].Replace("Apellidos: ", " ");
            txDNI.Text = datosNino[2].Replace("DNI: ", " ");
            txFnac.Text = datosNino[3].Replace("Fecha de Nacimiento: ", " ");
            txEdad.Text = datosNino[4].Replace("Edad: ", " ");
        }

        /// <summary>
        /// Evento del botón para actualizar los datos del niño en la base de datos.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los datos del formulario
                string nombre = txUsuario.Text.Trim();
                string dni = txDNI.Text.Trim();
                string apellidos = txApellidos.Text.Trim();
                DateTime fechaNacimiento;
                int edad;

                // Validar la fecha de nacimiento
                if (!DateTime.TryParse(txFnac.Text.Trim(), out fechaNacimiento))
                {
                    MessageBox.Show("Por favor, introduce una fecha de nacimiento válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar la edad
                if (!int.TryParse(txEdad.Text.Trim(), out edad))
                {
                    MessageBox.Show("Por favor, introduce un valor numérico válido para la edad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar campos requeridos
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(apellidos))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener la información seleccionada del ListBox
                if (LbNinos.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecciona un niño de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Usar el DNI (o cualquier dato único del niño) para buscar su ID en la base de datos
                string datosSeleccionados = LbNinos.SelectedItem.ToString();
                string dniSeleccionado = ExtraerDNI(datosSeleccionados); // Extrae el DNI del texto seleccionado en el ListBox
                if (string.IsNullOrEmpty(dniSeleccionado))
                {
                    MessageBox.Show("No se pudo obtener el DNI del niño seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Conectar a MongoDB y buscar el ID del niño
                IMongoDatabase _database = ConBD2.ObtenerConexionActiva();
                var ninosCollection = _database.GetCollection<BsonDocument>("Ninos");
                var filtroBuscarNino = Builders<BsonDocument>.Filter.Eq("DNI", dniSeleccionado);
                var ninoEncontrado = ninosCollection.Find(filtroBuscarNino).FirstOrDefault();

                if (ninoEncontrado == null)
                {
                    MessageBox.Show("No se encontró el niño en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ObjectId idNino = ninoEncontrado["_id"].AsObjectId;

                // 1. Actualizar los datos del niño en la colección "Ninos"
                var actualizacionNino = Builders<BsonDocument>.Update
                    .Set("Nombre", nombre)
                    .Set("DNI", dni)
                    .Set("Apellidos", apellidos)
                    .Set("FechaNacimiento", fechaNacimiento)
                    .Set("Edad", edad);

                ninosCollection.UpdateOne(filtroBuscarNino, actualizacionNino);

                // 2. Actualizar los datos del niño en el array "Hijos" de la colección "Usuarios"
                string idPadre = SesionIniciada.IdUsuario; // Obtener el ID del padre actual
                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("No se encontró el padre asociado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var usuariosCollection = _database.GetCollection<BsonDocument>("Usuarios");
                var filtroPadre = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(idPadre));
                var actualizacionPadre = Builders<BsonDocument>.Update
                    .Set("Hijos.$[nino].Nombre", nombre)
                    .Set("Hijos.$[nino].DNI", dni)
                    .Set("Hijos.$[nino].Apellidos", apellidos)
                    .Set("Hijos.$[nino].FechaNacimiento", fechaNacimiento)
                    .Set("Hijos.$[nino].Edad", edad);

                var arrayFilters = new List<ArrayFilterDefinition>
                {
                    new JsonArrayFilterDefinition<BsonDocument>("{ 'nino.DNI': '" + dniSeleccionado + "' }")
                };

                var opcionesActualizacion = new UpdateOptions { ArrayFilters = arrayFilters };

                usuariosCollection.UpdateOne(filtroPadre, actualizacionPadre, opcionesActualizacion);

                // Mostrar mensaje de éxito
                MessageBox.Show("Los datos del niño se han actualizado correctamente en ambas colecciones.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar datos en el formulario o limpiar los campos
                CargarDatosNinos(); // Método para recargar los datos en el ListBox
                LimpiarCampos(); // Método para limpiar los TextBox
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Extrae el DNI de un texto seleccionado en el ListBox.
        /// </summary>
        private string ExtraerDNI(string datosSeleccionados)
        {
            // Supongamos que el DNI está en el formato "DNI: [valor] ..."
            var partes = datosSeleccionados.Split(',');
            foreach (var parte in partes)
            {
                if (parte.Trim().StartsWith("DNI:"))
                {
                    return parte.Replace("DNI:", "").Trim();
                }
            }
            return null;
        }

        /// <summary>
        /// Limpia los campos del formulario.
        /// </summary>
        private void LimpiarCampos()
        {
            txUsuario.Text = string.Empty;
            txDNI.Text = string.Empty;
            txApellidos.Text = string.Empty;
            txFnac.Text = string.Empty;
            txEdad.Text = string.Empty;
        }

        /// <summary>
        /// Carga los datos de los niños del padre en el ListBox.
        /// </summary>
        private void CargarDatosNinos()
        {
            try
            {
                // Obtener el ID del padre desde la sesión iniciada
                string idPadre = SesionIniciada.IdUsuario;
                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("No se encontró un padre asociado a la sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Conectar a MongoDB y buscar los niños del padre
                IMongoDatabase _database = ConBD2.ObtenerConexionActiva();
                var ninosCollection = _database.GetCollection<BsonDocument>("Ninos");

                // Filtrar por el ID del padre
                var filtro = Builders<BsonDocument>.Filter.Eq("IdPadre", ObjectId.Parse(idPadre));
                var ninos = ninosCollection.Find(filtro).ToList();

                // Limpiar el ListBox antes de agregar nuevos elementos
                LbNinos.Items.Clear();

                // Verificar si hay niños asociados al padre
                if (ninos.Count == 0)
                {
                    LbNinos.Items.Add("No hay niños registrados.");
                    return;
                }

                // Agregar los datos de los niños al ListBox
                foreach (var nino in ninos)
                {
                    // Crear una representación legible para mostrar en el ListBox
                    string datosNino = $"Nombre: {nino["Nombre"]}, DNI: {nino["DNI"]}, Apellidos: {nino["Apellidos"]}, Fecha de nacimiento: {nino["FechaNacimiento"]:yyyy-MM-dd}, Edad: {nino["Edad"]}";
                    LbNinos.Items.Add(datosNino);
                }
                PadresForm pf = new PadresForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos de los niños: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

}

