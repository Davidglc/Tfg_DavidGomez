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
            // Asumiendo que txFnac.Text es una fecha en formato válido (ejemplo: "dd/MM/yyyy" o "yyyy-MM-dd")
            try
            {
                string nombre = txUsuario.Text.Trim();
                string DNI = txDNI.Text.Trim();
                string apellidos = txApellidos.Text.Trim();
                DateTime fechaNacimiento;
                int edad;

                if (DateTime.TryParse(txFnac.Text.Trim(), out fechaNacimiento))
                {
                    if (int.TryParse(txEdad.Text.Trim(), out edad))
                    {
                        Nino n = new Nino(nombre, DNI, apellidos, fechaNacimiento, edad);
                        PadresForm pf = new PadresForm();
                        pf.VerificarInstancia();
                        pf.AgregarNiño(n);
                        MessageBox.Show("Niño creado correctamente.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, introduce un valor numérico válido para la edad.");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, introduce una fecha de nacimiento válida.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el niño: {ex.Message}");
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
