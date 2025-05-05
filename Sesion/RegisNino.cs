using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TFG_DavidGomez.Clases;
using TFG_DavidGomez.Clases.Conexion;
using TFG_DavidGomez.Clases.Conexion.TFG_DavidGomez;



namespace TFG_DavidGomez.Sesion

/// <summary>
/// Clase que representa el formulario para registrar y gestionar información de niños en la aplicación.
/// Permite registrar nuevos niños, validando sus datos y asociándolos al usuario (padre) actual.
/// Incluye funcionalidades para mostrar la lista de niños registrados.
/// </summary>
{
    public partial class RegisNino : Form
    {
        private PadresForm _padresForm;

        /// <summary>
        /// Constructor de la clase.
        /// Inicializa el formulario y configura la visibilidad de elementos del formulario según las necesidades.
        /// </summary>
        public RegisNino()
        {
            InitializeComponent();
            RedondearBoton(btn_Aceptar, 20);
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
        /// Evento para manejar el clic en el botón de registro de un niño.
        /// Valida los datos introducidos, verifica la existencia del niño en la base de datos y lo registra.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los datos del formulario
                string nombre = txUsuario.Text.Trim();
                string dni = txDNI.Text.Trim();
                string apellidos = txApellidos.Text.Trim();
                DateTime fechaNacimiento;
                int edad;

                // Validar formato de fecha de nacimiento
                if (!DateTime.TryParseExact(txFnac.Text.Trim(), "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNacimiento))
                {
                    MessageBox.Show("Por favor, introduce la fecha de nacimiento en el formato válido (año/mes/día, por ejemplo: 2024/01/15).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato del DNI (solo una letra al final)
                if (!System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8}[A-Za-z]$"))
                {
                    MessageBox.Show("El DNI debe contener 8 números seguidos de una sola letra al final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar edad
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

                // Buscar si el niño ya existe en la base de datos
                IMongoDatabase _database = ConBD2.ObtenerConexionActiva();
                var ninosCollection = _database.GetCollection<BsonDocument>("Ninos");

                var filtroExistente = Builders<BsonDocument>.Filter.Eq("DNI", dni);
                var ninoExistente = ninosCollection.Find(filtroExistente).FirstOrDefault();

                if (ninoExistente != null)
                {
                    MessageBox.Show("El niño ya existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear un BsonDocument para el niño
                var nuevoNino = new BsonDocument
                    {
                        { "_id", ObjectId.GenerateNewId() }, // Generar un nuevo ObjectId
                        { "Nombre", nombre },
                        { "DNI", dni },
                        { "Apellidos", apellidos },
                        { "FechaNacimiento", fechaNacimiento }, // Almacenar como DateTime
                        { "Edad", edad }
                    };

                // Buscar al padre en la base de datos
                string idPadre = SesionIniciada.IdUsuario; // Implementa este método para obtener el ID del padre actual
                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("No se encontró al padre asociado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var padresCollection = _database.GetCollection<BsonDocument>("Usuarios");

                // Actualizar al padre añadiendo el niño a su lista de hijos
                var filtroPadre = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(idPadre));
                var actualizacion = Builders<BsonDocument>.Update.Push("Hijos", nuevoNino);
                padresCollection.UpdateOne(filtroPadre, actualizacion);

                // Añadir el niño a la colección "Ninos"
                var nuevoNino2 = new BsonDocument
                    {
                        { "_id", ObjectId.GenerateNewId() }, // Generar un nuevo ObjectId
                        { "Nombre", nombre },
                        { "DNI", dni },
                        { "Apellidos", apellidos },
                        { "FechaNacimiento", fechaNacimiento }, // Almacenar como DateTime
                        { "Edad", edad },
                        { "IdPadre", ObjectId.Parse(idPadre)}
                    };

                ninosCollection.InsertOne(nuevoNino2);

                // Mostrar mensaje de éxito
                MessageBox.Show("Niño agregado correctamente al padre.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cerrar el formulario o limpiar los campos
                this.Close();
            }
            catch (Exception ex)
            {
                // Manejar errores
                MessageBox.Show($"Error al agregar el niño: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Verifica si un objeto es una instancia de RegisNino y lo muestra.
        /// </summary>


        /// <summary>
        /// Carga los datos de los niños asociados al padre actual y los muestra en un ListBox.
        /// </summary>
        public void CargarDatosNinos()
        {
            try
            {
                PadresForm pf = new PadresForm();
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
                pf.LbNinos.Items.Clear();

                // Verificar si hay niños asociados al padre
                if (ninos.Count == 0)
                {
                    pf.LbNinos.Items.Add("No hay niños registrados.");
                    return;
                }

                // Agregar los datos de los niños al ListBox
                foreach (var nino in ninos)
                {
                    // Crear una representación legible para mostrar en el ListBox
                    string datosNino = $"Nombre: {nino["Nombre"]}, DNI: {nino["DNI"]}, Apellidos: {nino["Apellidos"]}, Fecha de nacimiento: {nino["FechaNacimiento"]:yyyy-MM-dd}, Edad: {nino["Edad"]}";
                    pf.LbNinos.Items.Add(datosNino);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos de los niños: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Generar un número aleatorio de 8 cifras
            Random rnd = new Random();
            int numeroDNI = rnd.Next(10000000, 99999999);

            // Calcular la letra del DNI
            string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            char letra = letras[numeroDNI % 23];

            // Formar el DNI completo
            string dniFicticio = numeroDNI.ToString() + letra;

            // Asignar al campo de texto
            txDNI.Text = dniFicticio;

            // Informar al usuario
            MessageBox.Show($"Se ha generado un DNI provisional válido: {dniFicticio}", "DNI generado automáticamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
