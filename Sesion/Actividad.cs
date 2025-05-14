using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TFG_DavidGomez.Clases;
using TFG_DavidGomez.Clases.Adaptador;
using TFG_DavidGomez.Clases.Conexion;

namespace TFG_DavidGomez.Sesion
{
    public partial class Actividad : Form
    {
        private Actividades actividadActual;
        private List<Nino> listaNinos;

        public Actividad()
        {
            InitializeComponent();
        }

        public void CargarDatos(Actividades actividad)
        {
            actividadActual = actividad;

            lb_nombre.Text = actividad.Nombre;
            lb_Fecha.Text = actividad.Fecha.ToShortDateString();
            lb_des.Text = actividad.Descripcion;

            if (actividad.Imagen != null)
            {
                using (var ms = new MemoryStream(actividad.Imagen))
                {
                    pn_Img.BackgroundImage = new Bitmap(ms);
                    pn_Img.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }

            CargarNinosDelPadre();
        }

        private void CargarNinosDelPadre()
        {
            cbNinos.Items.Clear();

            if (!int.TryParse(SesionIniciada.IdUsuario, out int idPadre))
            {
                MessageBox.Show("ID de padre inválido. Requiere sesión válida.");
                return;
            }

            MariaDbAdapter adapter = new MariaDbAdapter();
            listaNinos = adapter.CargarDatosNinoPorPadre(idPadre);

            if (listaNinos != null)
            {
                foreach (var n in listaNinos)
                {
                    cbNinos.Items.Add($"{n.Nombre} {n.Apellidos}");
                }
            }
        }

        public void AñadirImagenActividad(int idActividad, string rutaImagen)
        {
            byte[] imagenBytes = File.ReadAllBytes(rutaImagen);
            string query = "UPDATE Actividades SET imagen = @imagen WHERE id = @id";

            ConMDB con = new ConMDB();
            try
            {
                con.AbrirConexion();

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@imagen", imagenBytes);
                    cmd.Parameters.AddWithValue("@id", idActividad);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar imagen: " + ex.Message);
            }
            finally
            {
                con.CerrarConexion();
            }
        }

        private void btnApuntar_Click(object sender, EventArgs e)
        {
            if (cbNinos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un niño para inscribir.");
                return;
            }

            Nino ninoSeleccionado = listaNinos[cbNinos.SelectedIndex];

            if (!int.TryParse(SesionIniciada.IdUsuario, out int idPadre))
            {
                MessageBox.Show("Sesión no válida.");
                return;
            }

            ConMDB con = new ConMDB();
            try
            {
                con.AbrirConexion();

                // Verificar si ya está inscrito
                string checkQuery = @"SELECT COUNT(*) FROM Inscripciones 
                                      WHERE id_padre = @padre AND id_nino = @nino AND id_actividad = @actividad";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con.ObtenerConexion()))
                {
                    checkCmd.Parameters.AddWithValue("@padre", idPadre);
                    checkCmd.Parameters.AddWithValue("@nino", ninoSeleccionado.Id);
                    checkCmd.Parameters.AddWithValue("@actividad", actividadActual.Id);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Este niño ya está inscrito en esta actividad.");
                        return;
                    }
                }

                // Insertar inscripción
                string insertQuery = @"INSERT INTO Inscripciones 
                    (id_actividad, id_nino, id_padre, fecha_inscripcion)
                    VALUES (@actividad, @nino, @padre, @fecha)";

                using (MySqlCommand cmd = new MySqlCommand(insertQuery, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@actividad", actividadActual.Id);
                    cmd.Parameters.AddWithValue("@nino", ninoSeleccionado.Id);
                    cmd.Parameters.AddWithValue("@padre", idPadre);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Today);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Niño inscrito correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inscribir: " + ex.Message);
            }
            finally
            {
                con.CerrarConexion();
            }
        }
    }
}
