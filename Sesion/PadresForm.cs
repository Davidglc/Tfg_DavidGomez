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
using TFG_DavidGomez.Clases.Conexion.TFG_DavidGomez;
using TFG_DavidGomez.Sesion;


namespace TFG_DavidGomez
{

    /// <summary>
    /// Clase que representa el formulario para gestionar la interacción de los padres en la aplicación.
    /// Permite realizar tareas como registrar niños, inscribirlos en actividades, consultar actividades y editar datos personales.
    /// </summary>

    public partial class PadresForm : Form
    {

        MongoDBAdapter mdba;
        public DateTime selectedDate;


        /// <summary>
        /// Constructor de la clase PadresForm.
        /// Inicializa el formulario y carga los datos de los niños asociados al usuario.
        /// </summary>
        public PadresForm()
        {
            mdba = new MongoDBAdapter();
            InitializeComponent();
            CargarDatosNinos();
            CargarInscripciones();
            //this.FormClosed += CerrarAplicacion;
        }


        /// <summary>
        /// Evento para añadir un nuevo niño desde el formulario RegisNino.
        /// Actualiza la lista de niños después de la operación.
        /// </summary>
        private void añadirNiñoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisNino formAnadirNino = new RegisNino();
            formAnadirNino.FormClosed += FormAnadirNino_FormClosed;
            formAnadirNino.Show();
        }

        private void FormAnadirNino_FormClosed(object? sender, FormClosedEventArgs e)
        {
            CargarDatosNinos();
        }


        /// <summary>
        /// Agrega un niño a la lista de visualización en el ListBox.
        /// </summary>
        /// <param name="nino">Instancia del niño a agregar.</param>
        public void AgregarNiño(Nino nino)
        {
            if (nino == null)
            {
                MessageBox.Show("El niño no puede ser nulo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Formatear los datos del niño en una línea para mostrar en el ListBox
            string infoNino = $"Nombre: {nino.Nombre}, DNI: {nino.DNI}, Edad: {nino.Edad}, Fecha Nac: {nino.FechaNacimiento.ToShortDateString()}";

            // Agregar la información al ListBox
            LbNinos.Items.Add(infoNino);

            MessageBox.Show("Niño agregado correctamente al ListBox.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Verifica si la instancia de PadresForm es válida y, de ser así, la muestra.
        /// </summary>
        public void VerificarInstancia()
        {
            object obj = new PadresForm();

            if (obj is PadresForm PadresForm)
            {
                Console.WriteLine("El objeto proporcionado es de tipo MonitorForm.");
                PadresForm.Show();
            }
            else
            {
                Console.WriteLine("El objeto proporcionado no es de tipo MonitorForm.");
            }
        }

        /// <summary>
        /// Inscribe a un niño seleccionado en una actividad seleccionada en una fecha específica.
        /// </summary>
        private void btnApuntar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que haya un niño seleccionado en el ListBox
                if (LbNinos.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, seleccione un niño de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el texto del niño seleccionado
                string ninoSeleccionadoText = LbNinos.SelectedItem.ToString();
                var datosNino = ninoSeleccionadoText.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                if (datosNino.Length < 4)
                {
                    MessageBox.Show("El formato del niño seleccionado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombreSeleccionado = datosNino[0].Replace("Nombre: ", "").Trim();
                DateTime selectedDate = monthCalendar1.SelectionStart.Date;

                if (selectedDate == DateTime.MinValue)
                {
                    MessageBox.Show("Por favor, seleccione una fecha válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string idPadre = SesionIniciada.IdUsuario;
                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("Por favor, inicie sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ObjectId idPadreObj = ObjectId.Parse(idPadre);
                MongoDBAdapter mdba = new MongoDBAdapter();
                List<Nino> ninosDelPadre = mdba.CargarDatosNinoPorPadre(idPadreObj);
                Nino ninoSeleccionado = ninosDelPadre.FirstOrDefault(n => n.Nombre == nombreSeleccionado);

                if (ninoSeleccionado == null)
                {
                    MessageBox.Show("No se encontró al niño en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                BsonDocument actividad = mdba.ObtenerActividadPorDia(selectedDate);
                if (actividad == null)
                {
                    MessageBox.Show("No hay actividad en la fecha seleccionada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string idActividad = actividad.GetValue("_id").ToString();
                DateTime fechaActividad = actividad.GetValue("Fecha").ToUniversalTime();
                var inscripcionesCollection = ConBD2.GetCollection<BsonDocument>("Inscripciones");

                var filtroInscripcion = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("id_padre", idPadreObj),
                    Builders<BsonDocument>.Filter.Eq("id_actividad", ObjectId.Parse(idActividad)),
                    Builders<BsonDocument>.Filter.Eq("id_nino", ninoSeleccionado.Id),
                    Builders<BsonDocument>.Filter.Eq("fecha", fechaActividad)
                );

                bool yaInscrito = inscripcionesCollection.Find(filtroInscripcion).Any();

                if (yaInscrito)
                {
                    MessageBox.Show($"El niño '{ninoSeleccionado.Nombre}' ya está inscrito en esta actividad.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (fechaActividad < DateTime.Now.Date)
                {
                    MessageBox.Show("No puedes inscribir a un niño en actividades pasadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var inscripcion = new BsonDocument
                {
                    { "id_padre", idPadreObj },
                    { "id_actividad", ObjectId.Parse(idActividad) },
                    { "id_nino", ninoSeleccionado.Id },
                    { "fecha", fechaActividad }
                };

                inscripcionesCollection.InsertOne(inscripcion);

                MessageBox.Show($"Niño '{ninoSeleccionado.Nombre}' inscrito correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Marcar el día como inscrito
                MarcarDiaInscrito(selectedDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MarcarDiaInscrito(DateTime fecha)
        {
            if (!monthCalendar1.BoldedDates.Contains(fecha))
            {
                monthCalendar1.AddBoldedDate(fecha);
                monthCalendar1.UpdateBoldedDates();
            }
        }

        /// <summary>
        /// Cierra la sesión actual y abre la pantalla de inicio de sesión.
        /// </summary>
        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SesionIniciada si = new SesionIniciada();
            si.CerrarSesion();
            this.Close();
            InicioSesion inicioSesion = new InicioSesion();
            inicioSesion.ShowDialog();
        }

        /// <summary>
        /// Desapunta a un niño seleccionado de una actividad específica.
        /// </summary>
        /// 
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string idPadre = SesionIniciada.IdUsuario;

                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("La sesión no está iniciada. Por favor, inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (LbNinos.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, seleccione un niño de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ninoSeleccionadoText = LbNinos.SelectedItem.ToString();
                var datosNino = ninoSeleccionadoText.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                if (datosNino.Length < 4)
                {
                    MessageBox.Show("El formato del niño seleccionado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombreSeleccionado = datosNino[0].Replace("Nombre: ", "").Trim();

                DateTime selectedDate = monthCalendar1.SelectionRange.Start;

                if (selectedDate == DateTime.MinValue)
                {
                    MessageBox.Show("Por favor, seleccione una fecha válida del calendario.", "Fecha no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var mdba = new MongoDBAdapter();
                BsonDocument actividad = mdba.ObtenerActividadPorDia(selectedDate);

                if (actividad == null)
                {
                    MessageBox.Show("No hay actividades programadas para la fecha seleccionada.", "Actividad no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string idActividad = actividad.GetValue("_id").ToString();
                if (string.IsNullOrEmpty(idActividad))
                {
                    MessageBox.Show("No se pudo obtener el ID de la actividad.", "Error de Actividad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<Nino> ninos = mdba.CargarDatosNinoPorPadre(ObjectId.Parse(idPadre));
                Nino ninoSeleccionado = ninos.FirstOrDefault(n => n.Nombre == nombreSeleccionado);

                if (ninoSeleccionado == null)
                {
                    MessageBox.Show("No se encontró al niño seleccionado en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var inscripcionesCollection = ConBD2.GetCollection<BsonDocument>("Inscripciones");

                var filtroInscripcion = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("id_actividad", ObjectId.Parse(idActividad)),
                    Builders<BsonDocument>.Filter.Eq("id_nino", ninoSeleccionado.Id),
                    Builders<BsonDocument>.Filter.Gte("fecha", selectedDate.Date),
                    Builders<BsonDocument>.Filter.Lt("fecha", selectedDate.Date.AddDays(1))
                );

                var inscripcionNino = inscripcionesCollection.Find(filtroInscripcion).FirstOrDefault();

                if (inscripcionNino == null)
                {
                    MessageBox.Show("No se encontró inscripción para el niño seleccionado en la actividad para la fecha indicada.", "Error al desapuntar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fechaActividad = inscripcionNino.GetValue("fecha").ToUniversalTime();
                if (fechaActividad < DateTime.Now.Date)
                {
                    MessageBox.Show($"No se puede desapuntar al niño '{ninoSeleccionado.Nombre}' porque la actividad ya ha pasado (Fecha: {fechaActividad.ToShortDateString()}).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🟨 Confirmación antes de eliminar
                DialogResult result = MessageBox.Show($"¿Estás seguro de que deseas desapuntar al niño '{ninoSeleccionado.Nombre}' de la actividad del {fechaActividad.ToShortDateString()}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // 🔥 Eliminar inscripción
                    var resultado = inscripcionesCollection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", inscripcionNino.GetValue("_id").AsObjectId));

                    if (resultado.DeletedCount > 0)
                    {
                        MessageBox.Show($"El niño '{ninoSeleccionado.Nombre}' ha sido desapuntado correctamente de la actividad seleccionada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 🔁 Desmarcar la fecha
                        DesmarcarDiaInscrito(selectedDate);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la inscripción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // El usuario canceló la acción
                    MessageBox.Show("Desapuntado cancelado.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar desapuntar de la actividad: {ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DesmarcarDiaInscrito(DateTime fecha)
        {
            if (monthCalendar1.BoldedDates.Contains(fecha))
            {
                monthCalendar1.RemoveBoldedDate(fecha);
                monthCalendar1.UpdateBoldedDates();
            }
        }


        //private void button2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string idPadre = SesionIniciada.IdUsuario;

        //        if (string.IsNullOrEmpty(idPadre))
        //        {
        //            MessageBox.Show("La sesión no está iniciada. Por favor, inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        if (LbNinos.SelectedItem == null)
        //        {
        //            MessageBox.Show("Por favor, seleccione un niño de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        string ninoSeleccionadoText = LbNinos.SelectedItem.ToString();
        //        var datosNino = ninoSeleccionadoText.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

        //        if (datosNino.Length < 4)
        //        {
        //            MessageBox.Show("El formato del niño seleccionado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        string nombreSeleccionado = datosNino[0].Replace("Nombre: ", "").Trim();

        //        DateTime selectedDate = monthCalendar1.SelectionRange.Start;

        //        if (selectedDate == DateTime.MinValue)
        //        {
        //            MessageBox.Show("Por favor, seleccione una fecha válida del calendario.", "Fecha no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        var mdba = new MongoDBAdapter();
        //        BsonDocument actividad = mdba.ObtenerActividadPorDia(selectedDate);

        //        if (actividad == null)
        //        {
        //            MessageBox.Show("No hay actividades programadas para la fecha seleccionada.", "Actividad no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        string idActividad = actividad.GetValue("_id").ToString();

        //        if (string.IsNullOrEmpty(idActividad))
        //        {
        //            MessageBox.Show("No se pudo obtener el ID de la actividad.", "Error de Actividad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        List<Nino> ninos = mdba.CargarDatosNinoPorPadre(ObjectId.Parse(idPadre));

        //        Nino ninoSeleccionado = ninos.FirstOrDefault(n => n.Nombre == nombreSeleccionado);

        //        if (ninoSeleccionado == null)
        //        {
        //            MessageBox.Show("No se encontró al niño seleccionado en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        var inscripcionesCollection = ConBD2.GetCollection<BsonDocument>("Inscripciones");

        //        // Crear el filtro para la actividad y la fecha
        //        var filtroInscripcion = Builders<BsonDocument>.Filter.And(
        //            Builders<BsonDocument>.Filter.Eq("id_actividad", ObjectId.Parse(idActividad)),
        //            Builders<BsonDocument>.Filter.Eq("id_nino", ninoSeleccionado.Id),
        //            Builders<BsonDocument>.Filter.Gte("fecha", selectedDate.Date),
        //            Builders<BsonDocument>.Filter.Lt("fecha", selectedDate.Date.AddDays(1))
        //        );

        //        // Obtener la inscripción que coincide con el filtro
        //        var inscripcionNino = inscripcionesCollection.Find(filtroInscripcion).FirstOrDefault();

        //        if (inscripcionNino == null)
        //        {
        //            MessageBox.Show("No se encontró inscripción para el niño seleccionado en la actividad para la fecha indicada.", "Error al desapuntar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        // Verificar si la fecha de la actividad ya pasó
        //        DateTime fechaActividad = inscripcionNino.GetValue("fecha").ToUniversalTime();
        //        if (fechaActividad < DateTime.Now.Date)
        //        {
        //            MessageBox.Show($"No se puede desapuntar al niño '{ninoSeleccionado.Nombre}' porque la actividad ya ha pasado (Fecha: {fechaActividad.ToShortDateString()}).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        // Eliminar la inscripción
        //        var resultado = inscripcionesCollection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", inscripcionNino.GetValue("_id").AsObjectId));

        //        if (resultado.DeletedCount > 0)
        //        {
        //            MessageBox.Show($"El niño '{ninoSeleccionado.Nombre}' ha sido desapuntado correctamente de la actividad seleccionada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show("No se pudo eliminar la inscripción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ocurrió un error al intentar desapuntar de la actividad: {ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        /// <summary>
        /// Abre un formulario para editar los datos personales del usuario actual.
        /// </summary>

        private void editarDatosPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del usuario desde la sesión
                var id = ObjectId.Parse(SesionIniciada.IdUsuario);

                // Crear una instancia de MongoDBAdapter
                MongoDBAdapter ma = new MongoDBAdapter();

                // Obtener el usuario desde la base de datos
                Usuario u = ma.ObtenerUsuarioPorId(id);

                // Validar si el usuario fue encontrado
                if (u == null)
                {
                    MessageBox.Show("No se encontró información para este usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mostrar los datos personales del usuario
                DatosPersonales dp = new DatosPersonales(u);
                dp.VerificarInstancia2(u);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos personales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Maneja el evento de cambio de fecha en el calendario.
        /// Actualiza las actividades mostradas según la fecha seleccionada.
        /// </summary>
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Obtener la fecha seleccionada del calendario
            DateTime selectedDate = e.Start.Date;

            // Crear instancia del adaptador para MongoDB
            MongoDBAdapter mdba = new MongoDBAdapter();

            // Consultar actividades para la fecha seleccionada
            BsonDocument actividad = mdba.ObtenerActividadPorDia(selectedDate);

            // Limpiar el ListBox de actividades
            Actividades.Items.Clear();

            // Mostrar información de la actividad (si existe)
            if (actividad != null)
            {
                Actividades.Items.Add($"Actividad: {actividad["Nombre"]}");  // Campo "Nombre"

                // Formatear descripción para que se ajuste en varias líneas
                string descripcion = actividad["Descripcion"].ToString();
                string descripcionFormateada = FormatearTexto(descripcion, 40); // Ajusta el ancho deseado

                Actividades.Items.Add("Descripción:");
                foreach (var linea in descripcionFormateada.Split('\n'))
                {
                    Actividades.Items.Add($"  {linea}"); // Añadir cada línea con sangría opcional
                }

                Actividades.Items.Add($"Fecha: {((DateTime)actividad["Fecha"]).ToShortDateString()}"); // Formatear fecha
            }
            else
            {
                Actividades.Items.Add("No hay actividad para este día.");
            }
        }



        /// <summary>
        /// Formatea un texto largo en múltiples líneas ajustándose a un ancho especificado.
        /// </summary>
        /// <param name="texto">Texto a formatear.</param>
        /// <param name="ancho">Ancho máximo de las líneas.</param>
        /// <returns>Texto formateado con saltos de línea.</returns>

        private string FormatearTexto(string texto, int ancho)
        {
            StringBuilder resultado = new StringBuilder();
            int posicion = 0;

            while (posicion < texto.Length)
            {
                // Determinar la longitud de la línea, asegurándose de que no exceda el límite del texto
                int longitudLinea = Math.Min(ancho, texto.Length - posicion);

                // Buscar el último espacio dentro del rango permitido
                int corte = texto.LastIndexOf(' ', posicion + longitudLinea - 1, longitudLinea);

                // Si no hay espacio, cortar directamente al límite de la línea
                if (corte < posicion)
                {
                    corte = posicion + longitudLinea;
                }

                // Asegurarse de que el corte no exceda los límites del texto
                corte = Math.Min(corte, texto.Length);

                // Añadir la línea al resultado
                resultado.AppendLine(texto.Substring(posicion, corte - posicion).Trim());

                // Mover la posición al carácter siguiente después del corte
                posicion = corte + 1;
            }

            return resultado.ToString().Trim();
        }


        /// <summary>
        /// Carga los datos de los niños asociados al usuario actual desde la base de datos.
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos de los niños: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VistaPreviaInscripcion()
        {
            try
            {
                if (LbNinos.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, seleccione un niño de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener los datos del niño seleccionado
                string ninoSeleccionadoText = LbNinos.SelectedItem.ToString();
                var datosNino = ninoSeleccionadoText.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                if (datosNino.Length < 4)
                {
                    MessageBox.Show("El formato del niño seleccionado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombreSeleccionado = datosNino[0].Replace("Nombre: ", "").Trim();
                DateTime selectedDate = monthCalendar1.SelectionStart.Date;

                if (selectedDate == DateTime.MinValue)
                {
                    MessageBox.Show("Por favor, seleccione una fecha válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string idPadre = SesionIniciada.IdUsuario;
                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("Por favor, inicie sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ObjectId idPadreObj = ObjectId.Parse(idPadre);
                MongoDBAdapter mdba = new MongoDBAdapter();

                // Obtener el niño desde la base de datos usando el ID del padre
                List<Nino> ninosDelPadre = mdba.CargarDatosNinoPorPadre(idPadreObj);
                Nino ninoSeleccionado = ninosDelPadre.FirstOrDefault(n => n.Nombre == nombreSeleccionado);

                if (ninoSeleccionado == null)
                {
                    MessageBox.Show("No se encontró al niño en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar la actividad por la fecha
                BsonDocument actividad = mdba.ObtenerActividadPorDia(selectedDate);
                if (actividad == null)
                {
                    MessageBox.Show("No hay actividad en la fecha seleccionada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ObjectId idActividad = actividad.GetValue("_id").AsObjectId;

                // Usar el método limpio para verificar inscripción
                bool estaInscrito = mdba.VerificarInscripcion(ninoSeleccionado.Id, idActividad);

                if (estaInscrito)
                {
                    MessageBox.Show($"El niño '{ninoSeleccionado.Nombre}' está inscrito en la actividad del {selectedDate.ToShortDateString()}.", "Vista Previa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"El niño '{ninoSeleccionado.Nombre}' no está inscrito en la actividad del {selectedDate.ToShortDateString()}.", "Vista Previa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar la inscripción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_VP_Click(object sender, EventArgs e)
        {
            VistaPreviaInscripcion();
        }

        private void CargarInscripciones()
        {
            try
            {
                string idPadre = SesionIniciada.IdUsuario;

                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("La sesión no está iniciada. Por favor, inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ObjectId idPadreObj = ObjectId.Parse(idPadre);
                MongoDBAdapter mdba = new MongoDBAdapter();

                // Obtener todas las inscripciones del padre
                var inscripcionesCollection = ConBD2.GetCollection<BsonDocument>("Inscripciones");

                var filtro = Builders<BsonDocument>.Filter.Eq("id_padre", idPadreObj);
                var inscripciones = inscripcionesCollection.Find(filtro).ToList();

                // Limpiar fechas previas
                monthCalendar1.RemoveAllBoldedDates();

                foreach (var inscripcion in inscripciones)
                {
                    if (inscripcion.Contains("fecha"))
                    {
                        DateTime fecha = inscripcion.GetValue("fecha").ToUniversalTime().Date;
                        monthCalendar1.AddBoldedDate(fecha);
                    }
                }

                monthCalendar1.UpdateBoldedDates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar inscripciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void CerrarAplicacion(object sender, FormClosedEventArgs e)
        //{
        //    Application.Exit();
        //}
    }
}
