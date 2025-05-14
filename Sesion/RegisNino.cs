using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
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

                // Validar edad
                if (!int.TryParse(txEdad.Text.Trim(), out edad))
                {
                    MessageBox.Show("Por favor, introduce un valor numérico válido para la edad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar campos requeridos
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos))
                {
                    MessageBox.Show("Por favor, complete los campos requeridos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar formato del DNI si se proporciona
                if (!string.IsNullOrWhiteSpace(dni) && !System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8}[A-Za-z]$"))
                {
                    MessageBox.Show("El DNI debe contener 8 números seguidos de una sola letra al final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener ID del padre desde la sesión
                if (!int.TryParse(SesionIniciada.IdUsuario, out int idPadre))
                {
                    MessageBox.Show("No se encontró un padre válido para asociar al niño.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ConMDB conexion = new ConMDB();
                conexion.AbrirConexion();

                // Verificar si ya existe un niño con el mismo DNI (solo si no es nulo)
                if (!string.IsNullOrEmpty(dni))
                {
                    string checkQuery = "SELECT COUNT(*) FROM Ninos WHERE dni = @dni";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conexion.ObtenerConexion()))
                    {
                        checkCmd.Parameters.AddWithValue("@dni", dni);
                        int existe = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (existe > 0)
                        {
                            MessageBox.Show("Ya existe un niño con el mismo DNI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            conexion.CerrarConexion();
                            return;
                        }
                    }
                }

                // Insertar el nuevo niño en la tabla Ninos
                string insertQuery = "INSERT INTO Ninos (nombre, apellidos, fecha_nacimiento, dni, edad, id_padre) VALUES (@nombre, @apellidos, @fechaNacimiento, @dni, @edad, @idPadre)";
                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conexion.ObtenerConexion()))
                {
                    insertCmd.Parameters.AddWithValue("@nombre", nombre);
                    insertCmd.Parameters.AddWithValue("@apellidos", apellidos);
                    insertCmd.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
                    insertCmd.Parameters.AddWithValue("@dni", string.IsNullOrEmpty(dni) ? (object)DBNull.Value : dni);
                    insertCmd.Parameters.AddWithValue("@edad", edad);
                    insertCmd.Parameters.AddWithValue("@idPadre", idPadre);

                    insertCmd.ExecuteNonQuery();
                }

                conexion.CerrarConexion();

                // Mostrar éxito
                MessageBox.Show("Niño añadido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el niño: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Verifica si un objeto es una instancia de RegisNino y lo muestra.
        /// </summary>


        /// <summary>
        /// Carga los datos de los niños asociados al padre actual y los muestra en un ListBox.
        /// </summary>
        //public void CargarDatosNinos()
        //{
        //    try
        //    {
        //        PadresForm pf = new PadresForm();
        //        // Obtener el ID del padre desde la sesión iniciada
        //        string idPadre = SesionIniciada.IdUsuario;
        //        if (string.IsNullOrEmpty(idPadre))
        //        {
        //            MessageBox.Show("No se encontró un padre asociado a la sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        // Conectar a MongoDB y buscar los niños del padre
        //        IMongoDatabase _database = ConBD2.ObtenerConexionActiva();
        //        var ninosCollection = _database.GetCollection<BsonDocument>("Ninos");

        //        // Filtrar por el ID del padre
        //        var filtro = Builders<BsonDocument>.Filter.Eq("IdPadre", ObjectId.Parse(idPadre));
        //        var ninos = ninosCollection.Find(filtro).ToList();

        //        // Limpiar el ListBox antes de agregar nuevos elementos
        //        pf.LbNinos.Items.Clear();

        //        // Verificar si hay niños asociados al padre
        //        if (ninos.Count == 0)
        //        {
        //            pf.LbNinos.Items.Add("No hay niños registrados.");
        //            return;
        //        }

        //        // Agregar los datos de los niños al ListBox
        //        foreach (var nino in ninos)
        //        {
        //            // Crear una representación legible para mostrar en el ListBox
        //            string datosNino = $"Nombre: {nino["Nombre"]}, DNI: {nino["DNI"]}, Apellidos: {nino["Apellidos"]}, Fecha de nacimiento: {nino["FechaNacimiento"]:yyyy-MM-dd}, Edad: {nino["Edad"]}";
        //            pf.LbNinos.Items.Add(datosNino);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ocurrió un error al cargar los datos de los niños: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void label5_Click(object sender, EventArgs e)
        {
            // Asignar un valor nulo (vacío) al campo del DNI
            txDNI.Text = string.Empty;

            // Informar al usuario que se dejará sin DNI
            MessageBox.Show("Este niño no tendrá DNI registrado. Se dejará como valor nulo en la base de datos.",
                "DNI no obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
