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
        }

        private void añadirNiñoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisNino formAnadirNino = new RegisNino();

            formAnadirNino.VerificarInstancia();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            selectedDate = e.Start;  // Fecha seleccionada
            mdba = new MongoDBAdapter();

            // Obtener la actividad para la fecha seleccionada
            BsonDocument actividad = mdba.ObtenerActividadPorDia(selectedDate);

            // Limpiar los items anteriores
            Actividades.Items.Clear();

            // Verificar si se encontró alguna actividad
            if (actividad != null)
            {
                // Si se encontró una actividad, mostrar la información en el ListBox o el control que uses
                Actividades.Items.Add($"Actividad: {actividad["nombre"]}");  // Ajusta según el campo que contenga el nombre de la actividad
                Actividades.Items.Add($"Descripción: {actividad["descripcion"]}");  // Ajusta según el campo que contenga la descripción
                Actividades.Items.Add($"Fecha: {actividad["fecha"]}");  // Ajusta según el campo de fecha
            }
            else
            {
                // Si no se encuentra ninguna actividad para esa fecha
                Actividades.Items.Add("No hay actividad para este día.");
            }
        }

        public void AgregarNiño(Nino Niño)
        {
            LbNinos.Items.Add(Niño);
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

        private void btnApuntar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del padre desde la sesión
                string idPadre = SesionIniciada.IdUsuario;

                mdba = new MongoDBAdapter();

                BsonDocument Actividad = mdba.ObtenerActividadPorDia(selectedDate);
                String idActividad = Actividad.GetValue("_id").ToString();

                // Validar que el ID de la actividad no esté vacío
                if (string.IsNullOrEmpty(idActividad))
                {
                    MessageBox.Show("Por favor, complete el campo de la actividad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el documento de inscripción para la base de datos
                var inscripcion = new BsonDocument
                    {
                        { "id_padre", ObjectId.Parse(idPadre) },       // ID del padre desde la sesión
                        { "id_actividad", ObjectId.Parse(idActividad) } // ID de la actividad
                    };

                // Insertar el documento en la colección "inscripciones"
                var inscripcionesCollection = ConexionBD.GetCollection<BsonDocument>("inscripciones");
                inscripcionesCollection.InsertOne(inscripcion);

                // Confirmar al usuario que la inscripción fue exitosa
                MessageBox.Show("Inscripción realizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (InvalidOperationException ex)
            {
                // Error si la sesión no está iniciada
                MessageBox.Show($"Error: {ex.Message}. Inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                // Error si el ID de la actividad no tiene un formato válido
                MessageBox.Show("Por favor, ingrese un ID de actividad válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Cualquier otro error
                MessageBox.Show($"Ocurrió un error al realizar la inscripción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Obtener el ID del padre desde la sesión
                string idPadre = SesionIniciada.IdUsuario;


                // Validar si la sesión está iniciada
                if (string.IsNullOrEmpty(idPadre))
                {
                    MessageBox.Show("La sesión no está iniciada. Por favor, inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear instancia del adaptador para la base de datos
                var mdba = new MongoDBAdapter();

                // Obtener la actividad seleccionada
                selectedDate = monthCalendar1.SelectionRange.Start;
                var actividad = mdba.ObtenerActividadPorDia(selectedDate);

                // Validar que existe una actividad para la fecha seleccionada
                if (actividad == null)
                {
                    MessageBox.Show("No hay actividades programadas para la fecha seleccionada.", "Actividad no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el ID de la actividad
                string idActividad = actividad.GetValue("_id").ToString();

                // Validar que el ID de la actividad no esté vacío
                if (string.IsNullOrEmpty(idActividad))
                {
                    MessageBox.Show("No se pudo obtener el ID de la actividad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener la colección "inscripciones"
                var inscripcionesCollection = ConexionBD.GetCollection<BsonDocument>("inscripciones");

                // Crear el filtro para eliminar la inscripción del padre a la actividad
                var filtro = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("id_padre", ObjectId.Parse(idPadre)),
                    Builders<BsonDocument>.Filter.Eq("id_actividad", ObjectId.Parse(idActividad))
                );

                // Eliminar la inscripción
                var resultado = inscripcionesCollection.DeleteOne(filtro);

                // Validar si se eliminó algún documento
                if (resultado.DeletedCount > 0)
                {
                    MessageBox.Show("Desapuntado correctamente de la actividad.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró una inscripción para desapuntar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (InvalidOperationException ex)
            {
                // Error si la sesión no está iniciada
                MessageBox.Show($"Error: {ex.Message}. Inicie sesión nuevamente.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException ex)
            {
                // Error si el ID no tiene un formato válido
                MessageBox.Show("Por favor, asegúrese de que los IDs sean válidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Cualquier otro error
                MessageBox.Show($"Ocurrió un error al desapuntar de la actividad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
