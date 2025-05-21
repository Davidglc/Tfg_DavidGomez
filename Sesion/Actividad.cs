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
            CargarActividades();          

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

        string rutaImagen = "";
        byte[] imagenBytes = null;

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imágenes (*.jpg;*.png)|*.jpg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaImagen = ofd.FileName;
                    imagenBytes = File.ReadAllBytes(rutaImagen);

                    Image img = Image.FromFile(rutaImagen);
                    pn_Img.BackgroundImage = new Bitmap(img, pn_Img.Size);
                    pn_Img.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string fechaTexto = txtFecha.Text.Trim();
            string descripcion = string.Join(",", lbDescripcion.Items.Cast<string>());

            if (!DateTime.TryParse(fechaTexto, out DateTime fecha))
            {
                MessageBox.Show("Fecha inválida.");
                return;
            }

            if (string.IsNullOrEmpty(nombre) || lbDescripcion.Items.Count == 0 || imagenBytes == null)
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            string query = "INSERT INTO Actividades (nombre, descripcion, fecha, imagen) VALUES (@nombre, @descripcion, @fecha, @imagen)";

            ConMDB con = new ConMDB();
            con.AbrirConexion();

            using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@imagen", imagenBytes);

                cmd.ExecuteNonQuery();
            }

            con.CerrarConexion();
            MessageBox.Show("Actividad guardada.");
            CargarActividades();
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtFecha.Clear();
            lbDescripcion.Items.Clear();
            pn_Img.BackgroundImage = null;
            imagenBytes = null;
        }

        private void CargarActividades()
        {
            dgvActividades.Rows.Clear();
            dgvActividades.Columns.Clear();

            dgvActividades.Columns.Add("Id", "ID");
            dgvActividades.Columns["Id"].Visible = false;
            dgvActividades.Columns.Add("Nombre", "Nombre");
            dgvActividades.Columns.Add("Fecha", "Fecha");

            // 🟡 Añadir columna oculta para imagen
            var colImg = new DataGridViewImageColumn
            {
                Name = "Imagen",
                HeaderText = "Imagen",
                Visible = false // Ocultar visualmente
            };
            dgvActividades.Columns.Add(colImg);

            string query = "SELECT id, nombre, fecha, imagen FROM Actividades";

            ConMDB con = new ConMDB();
            con.AbrirConexion();

            using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvActividades.Rows.Add(
                            reader["id"].ToString(),
                            reader["nombre"].ToString(),
                            Convert.ToDateTime(reader["fecha"]).ToString("yyyy-MM-dd"),
                            reader["imagen"] is DBNull ? null : (byte[])reader["imagen"]
                        );
                    }
                }
            }

            con.CerrarConexion();
        }



        private void dgvActividades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvActividades.Rows[e.RowIndex];

                // Nombre y Fecha
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                txtFecha.Text = fila.Cells["Fecha"].Value?.ToString();

                // ID para la descripción
                string id = fila.Cells["Id"].Value?.ToString();
                lbDescripcion.Items.Clear();

                string query = "SELECT descripcion FROM Actividades WHERE id = @id";
                ConMDB con = new ConMDB();
                con.AbrirConexion();

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string descripcion = reader["descripcion"].ToString();
                            const int maxCharsPerLine = 60;

                            for (int i = 0; i < descripcion.Length; i += maxCharsPerLine)
                            {
                                string linea = descripcion.Substring(i, Math.Min(maxCharsPerLine, descripcion.Length - i));
                                lbDescripcion.Items.Add(linea.Trim());
                            }

                        }
                    }
                }

                con.CerrarConexion();

                // Imagen (ya debe estar cargada desde la base de datos como columna oculta "Imagen")
                object valorImagen = fila.Cells["Imagen"].Value;
                if (valorImagen != null && valorImagen != DBNull.Value && valorImagen is byte[] imgBytes)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(imgBytes))
                        {
                            pn_Img.BackgroundImage = new Bitmap(Image.FromStream(ms), pn_Img.Size);
                            pn_Img.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                    catch
                    {
                        pn_Img.BackgroundImage = null; // Evita excepción si los bytes están corruptos
                    }
                }
                else
                {
                    pn_Img.BackgroundImage = null;
                }
            }
        }




    }
}
