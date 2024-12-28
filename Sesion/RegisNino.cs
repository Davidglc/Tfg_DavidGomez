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
using System.Windows.Forms.VisualStyles;
using TFG_DavidGomez.Clases;
using TFG_DavidGomez.Clases.Conexion;
using TFG_DavidGomez.Clases.Conexion.TFG_DavidGomez;

namespace TFG_DavidGomez.Sesion
{
    public partial class RegisNino : Form
    {
        public RegisNino()
        {
            InitializeComponent();
        }

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

                // Validar fecha de nacimiento
                if (!DateTime.TryParse(txFnac.Text.Trim(), out fechaNacimiento))
                {
                    MessageBox.Show("Por favor, introduce una fecha de nacimiento válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                IMongoDatabase _database = ConBD2.ObtenerConexionActiva();
                var padresCollection = _database.GetCollection<BsonDocument>("Usuarios");

                // Actualizar al padre añadiendo el niño a su lista de hijos
                var filtro = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(idPadre));
                var actualizacion = Builders<BsonDocument>.Update.Push("ninos", nuevoNino);
                padresCollection.UpdateOne(filtro, actualizacion);

                // Mostrar mensaje de éxito
                MessageBox.Show("Niño agregado correctamente al padre.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Nino n = new Nino(nombre, dni, apellidos, fechaNacimiento, edad);
                PadresForm pf = new PadresForm();
                pf.AgregarNiño(n);

                // Cerrar el formulario o limpiar los campos
                this.Close();
            }
            catch (Exception ex)
            {
                // Manejar errores
                MessageBox.Show($"Error al agregar el niño: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public void VerificarInstancia()
        {
            object obj = new RegisNino();

            if (obj is RegisNino RegisNi)
            {
                Console.WriteLine("El objeto es una instancia de PadresForm.");
                RegisNi.Show();
            }
            else
            {
                Console.WriteLine("El objeto no es una instancia de PadresForm.");
            }
        }

        public void CargarDatosNino(Nino nino)
        {
            button1.Visible = false;
            button2.Visible = true;
            // Asignar los valores del objeto Nino a los campos del formulario
            
            txUsuario.Text = nino.Nombre;
            txDNI.Text = nino.DNI; // Usaremos el ID como DNI (si aplica)
            txApellidos.Text = nino.Apellidos;
            txFnac.Text = nino.FechaNacimiento.ToString("yyyy-MM-dd");
            txEdad.Text = nino.Edad.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que todos los campos estén llenos
                if (string.IsNullOrEmpty(txUsuario.Text) ||
                    string.IsNullOrEmpty(txDNI.Text) ||
                    string.IsNullOrEmpty(txApellidos.Text) ||
                    string.IsNullOrEmpty(txFnac.Text) ||
                    string.IsNullOrEmpty(txEdad.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Convertir los datos del formulario a un objeto Nino
                var nino = new Nino(
                    txUsuario.Text.Trim(),
                    txDNI.Text.Trim(),
                    txApellidos.Text.Trim(),
                    DateTime.Parse(txFnac.Text.Trim()),
                    int.Parse(txEdad.Text.Trim())
                )
                {
                    IdPadre = ObjectId.Parse(SesionIniciada.IdUsuario) // Asignar el ID del padre desde la sesión
                };

                // Obtener la colección de niños desde la base de datos
                var ninosCollection = ConexionBD.GetCollection<Nino>("ninos");

                // Crear un filtro para buscar si el niño ya existe por DNI
                var filtro = Builders<Nino>.Filter.And(
                    Builders<Nino>.Filter.Eq(n => n.IdPadre, nino.IdPadre), // ID del padre
                    Builders<Nino>.Filter.Eq(n => n.Nombre, txUsuario.Text.Trim()) // Nombre del niño
                );

                // Verificar si el niño ya existe
                var existente = ninosCollection.Find(filtro).FirstOrDefault();

                if (existente != null)
                {
                    // Actualizar los datos del niño existente
                    var update = Builders<Nino>.Update
                        .Set(n => n.Apellidos, nino.Apellidos)
                        .Set(n => n.FechaNacimiento, nino.FechaNacimiento)
                        .Set(n => n.Edad, nino.Edad);

                    var resultado = ninosCollection.UpdateOne(filtro, update);

                    if (resultado.ModifiedCount > 0)
                    {
                        MessageBox.Show("Datos del niño actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se realizaron cambios en los datos del niño.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Insertar un nuevo niño si no existe
                    ninosCollection.InsertOne(nino);
                    MessageBox.Show("Datos del niño guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Limpiar los campos del formulario después de guardar
                txUsuario.Clear();
                txDNI.Clear();
                txApellidos.Clear();
                txFnac.Clear();
                txEdad.Clear();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("El formato de algunos datos no es válido. Por favor, verifique e intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los datos del niño: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}
