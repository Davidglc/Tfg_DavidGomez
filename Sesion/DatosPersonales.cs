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

namespace TFG_DavidGomez.Sesion
{
    public partial class DatosPersonales : Form
    {
        public DatosPersonales()
        {
            InitializeComponent();
        }

        public DatosPersonales(Usuario usuario)
        {
            InitializeComponent();

            // Asignar los datos a los controles
            txUsuario.Text = usuario.Nombre;
            txDNI.Text = usuario.Dni;
            TxContrasena.Text = usuario.Contrasena;
            txApellidos.Text = usuario.Apellidos;
            txCorreo.Text = usuario.Email;
        }

        private void datosNiñosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string idPadreTexto = SesionIniciada.IdUsuario;

                if (string.IsNullOrEmpty(idPadreTexto))
                {
                    MessageBox.Show("No hay una sesión activa. Por favor, inicie sesión.", "Sesión no válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ObjectId idPadre = ObjectId.Parse(idPadreTexto);

                MongoDBAdapter ma = new MongoDBAdapter();
                List<Nino> ninos = ma.CargarDatosNinoPorPadre(idPadre);

                if (ninos == null || ninos.Count == 0)
                {
                    MessageBox.Show("No se encontraron niños asociados con este padre.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RegisNino regisNino = new RegisNino();
                regisNino.CargarDatosNino(ninos[0]);
                regisNino.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void VerificarInstancia()
        {
            object obj = new DatosPersonales();

            if (obj is DatosPersonales dp)
            {
                Console.WriteLine("El objeto proporcionado es de tipo MonitorForm.");
                dp.Show();
            }
            else
            {
                Console.WriteLine("El objeto proporcionado no es de tipo MonitorForm.");
            }
        }

        public void VerificarInstancia2(Usuario u)
        {
            object obj = new DatosPersonales(u);

            if (obj is DatosPersonales dp)
            {
                Console.WriteLine("El objeto proporcionado es de tipo MonitorForm.");
                dp.Show();
            }
            else
            {
                Console.WriteLine("El objeto proporcionado no es de tipo MonitorForm.");
            }
        }

    }
}

