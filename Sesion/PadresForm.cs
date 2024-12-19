using MongoDB.Bson;
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
    }
}
