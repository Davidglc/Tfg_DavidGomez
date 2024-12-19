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
                string apellidos = txApellidos.Text.Trim();
                DateTime fechaNacimiento;
                int edad;

                if (DateTime.TryParse(txFnac.Text.Trim(), out fechaNacimiento))
                {
                    if (int.TryParse(txEdad.Text.Trim(), out edad))
                    {
                        Nino n = new Nino(nombre, apellidos, fechaNacimiento, edad);
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

    }

}
