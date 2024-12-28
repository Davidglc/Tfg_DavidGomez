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
    public partial class PadresForm : Form
    {

        MongoDBAdapter mdba;
        public DateTime selectedDate;

        public PadresForm()
        {
            mdba = new MongoDBAdapter();
            InitializeComponent();
            CargarHijos();
        }

        private void añadirNiñoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisNino formAnadirNino = new RegisNino();

            formAnadirNino.VerificarInstancia();
        }
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

        //private void btnApuntar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Obtener el ID del padre desde la sesión
        //        string idPadre = SesionIniciada.IdUsuario;

        //        mdba = new MongoDBAdapter();

        //        BsonDocument Actividad = mdba.ObtenerActividadPorDia(selectedDate);
        //        String idActividad = Actividad.GetValue("_id").ToString();

        //        // Validar que el ID de la actividad no esté vacío
        //        if (string.IsNullOrEmpty(idActividad))
        //        {
        //            MessageBox.Show("Por favor, complete el campo de la actividad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        // Crear el documento de inscripción para la base de datos
        //        var inscripcion = new BsonDocument
        //            {
        //                { "id_padre", ObjectId.Parse(idPadre) },       // ID del padre desde la sesión
        //                { "id_actividad", ObjectId.Parse(idActividad) } // ID de la actividad
        //            };

        //        // Insertar el documento en la colección "inscripciones"
        //        var inscripcionesCollection = ConexionBD.GetCollection<BsonDocument>("inscripciones");
        //        inscripcionesCollection.InsertOne(inscripcion);

        //        // Confirmar al usuario que la inscripción fue exitosa
        //        MessageBox.Show("Inscripción realizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        // Error si la sesión no está iniciada
        //        MessageBox.Show($"Error: {ex.Message}. Inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (FormatException ex)
        //    {
        //        // Error si el ID de la actividad no tiene un formato válido
        //        MessageBox.Show("Por favor, ingrese un ID de actividad válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Cualquier otro error
        //        MessageBox.Show($"Ocurrió un error al realizar la inscripción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnApuntar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener la fecha seleccionada directamente del calendario
                DateTime selectedDate = monthCalendar1.SelectionStart.Date;

                // Verificar que se haya seleccionado una fecha válida
                if (selectedDate == DateTime.MinValue)
                {
                    MessageBox.Show("Por favor, seleccione una fecha del calendario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el ID del padre desde la sesión
                string idPadre = SesionIniciada.IdUsuario;

                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("Por favor, inicie sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MongoDBAdapter mdba = new MongoDBAdapter();

                // Buscar la actividad para la fecha seleccionada
                BsonDocument actividad = mdba.ObtenerActividadPorDia(selectedDate);

                if (actividad == null)
                {
                    MessageBox.Show("No se encontró actividad para la fecha seleccionada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string idActividad = actividad.GetValue("_id").ToString();

                // Cargar los datos de los niños asociados al padre
                List<Nino> ninos = mdba.CargarDatosNinoPorPadre(ObjectId.Parse(idPadre));

                if (ninos == null || !ninos.Any())
                {
                    MessageBox.Show("No se encontraron niños asociados a este padre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear la colección para las inscripciones
                var inscripcionesCollection = ConBD2.GetCollection<BsonDocument>("Inscripciones");

                // Insertar una inscripción por cada niño
                foreach (var nino in ninos)
                {
                    var inscripcion = new BsonDocument
                    {
                        { "id_padre", ObjectId.Parse(idPadre) },
                        { "id_actividad", ObjectId.Parse(idActividad) },
                        { "id_nino", nino.Id } // Suponiendo que "Nino" tiene una propiedad "Id" de tipo ObjectId
                    };

                    inscripcionesCollection.InsertOne(inscripcion);
                }

                MessageBox.Show("Inscripción realizada correctamente para todos los niños.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error: Inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Por favor, seleccione una actividad válida.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al realizar la inscripción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SesionIniciada si = new SesionIniciada();
            si.CerrarSesion();
            this.Close();
            InicioSesion inicioSesion = new InicioSesion();
            inicioSesion.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que el padre esté en sesión
                string idPadre = SesionIniciada.IdUsuario;

                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("La sesión no está iniciada. Por favor, inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear instancia del adaptador de base de datos
                var mdba = new MongoDBAdapter();

                // Obtener la fecha seleccionada del calendario
                DateTime selectedDate = monthCalendar1.SelectionRange.Start;

                // Validar que se haya seleccionado una fecha
                if (selectedDate == DateTime.MinValue)
                {
                    MessageBox.Show("Por favor, seleccione una fecha válida.", "Fecha no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Buscar la actividad en la base de datos según la fecha seleccionada
                BsonDocument actividad = mdba.ObtenerActividadPorDia(selectedDate);

                // Validar que existe una actividad para la fecha seleccionada
                if (actividad == null)
                {
                    MessageBox.Show("No hay actividades programadas para la fecha seleccionada.", "Actividad no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el ID de la actividad
                string idActividad = actividad.GetValue("_id").ToString();

                if (string.IsNullOrEmpty(idActividad))
                {
                    MessageBox.Show("No se pudo obtener el ID de la actividad.", "Error de Actividad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener la lista de niños asociados al padre
                List<Nino> ninos = mdba.CargarDatosNinoPorPadre(ObjectId.Parse(idPadre));

                // Crear una lista de IDs que incluye al padre y a sus niños
                var idsRelacionados = ninos.Select(n => n.Id).ToList();
                 // Agregar el ID del padre a la lista

                // Crear conexión con la colección "inscripciones"
                var inscripcionesCollection = ConBD2.GetCollection<BsonDocument>("Inscripciones");

                // Crear el filtro para identificar todas las inscripciones asociadas al padre y sus niños para la actividad seleccionada
                var filtro = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.In("id_padre", idPadre), // Verifica que el ID sea del padre o de sus niños
                    Builders<BsonDocument>.Filter.Eq("id_actividad", idActividad) // Mismo ID de actividad
                );

                // Eliminar las inscripciones
                var resultado = inscripcionesCollection.DeleteMany(filtro);

                // Validar si se eliminaron registros
                if (resultado.DeletedCount > 0)
                {
                    MessageBox.Show($"Se desapuntaron correctamente {resultado.DeletedCount} inscripciones para la actividad seleccionada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron inscripciones para la actividad seleccionada.", "Error al desapuntar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (InvalidOperationException ex)
            {
                // Capturar errores relacionados con sesiones no iniciadas
                MessageBox.Show($"Error: {ex.Message}. Por favor, inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                // Capturar errores relacionados con el formato de los IDs
                MessageBox.Show("Por favor, asegúrese de que los IDs sean válidos y correctos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro tipo de error
                MessageBox.Show($"Ocurrió un error al intentar desapuntar de la actividad: {ex.Message}", "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


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

                DatosPersonales dp = new DatosPersonales(u);
                dp.VerificarInstancia2(u);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos personales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void CargarHijos()
        {
            try
            {
                string idPadre = SesionIniciada.IdUsuario;
                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("No se encontró el padre asociado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ObjectId objectIdPadre = ObjectId.Parse(idPadre);
                MongoDBAdapter ma = new MongoDBAdapter();

                var listaNinos = ma.CargarDatosNinoPorPadre(objectIdPadre);

                LbNinos.Items.Clear();

                if (listaNinos != null && listaNinos.Any())
                {
                    foreach (var nino in listaNinos)
                    {
                        LbNinos.Items.Add($"Nombre: {nino.Nombre}, DNI: {nino.DNI}");
                        LbNinos.Items.Add($"Edad: {nino.Edad}, Fecha Nac: {nino.FechaNacimiento.ToShortDateString()}");
                        LbNinos.Items.Add(""); // Espacio entre niños
                    }
                }
                else
                {
                    LbNinos.Items.Add("No se encontraron niños para este padre.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los hijos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
