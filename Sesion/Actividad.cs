﻿using MySql.Data.MySqlClient;
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
        private int? actividadSeleccionadaId = null;

        public Actividad()
        {
            InitializeComponent();
            // ConfigurarPaneles();
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
            CargarMaterialesDesdeActividad(actividad.Id);
        }

        private void CargarMaterialesDesdeActividad(int idActividad)
        {
            dbgMateriales.Columns.Clear();
            dbgMateriales.Rows.Clear();

            // Solo una columna: nombre del material
            dbgMateriales.Columns.Add("Material", "Nombre del Material");

            ConMDB con = new ConMDB();
            con.AbrirConexion();

            string query = "SELECT materiales FROM Actividades WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
            {
                cmd.Parameters.AddWithValue("@id", idActividad);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read() && !reader.IsDBNull(0))
                    {
                        string materialesTexto = reader.GetString(0);
                        string[] materiales = materialesTexto.Split(',');

                        foreach (var item in materiales)
                        {
                            string nombreMaterial = item.Trim();
                            dbgMateriales.Rows.Add(nombreMaterial);
                        }
                    }
                }
            }
            EstilizarTabla(dbgMateriales);
            con.CerrarConexion();
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
            string descripcion = txtDescripcion.Text.Trim();

            if (!DateTime.TryParse(fechaTexto, out DateTime fecha))
            {
                MessageBox.Show("Fecha inválida.");
                return;
            }

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(descripcion) || imagenBytes == null || cbNinos.SelectedItem == null)
            {
                MessageBox.Show("Completa todos los campos, incluyendo el monitor.");
                return;
            }

            // Obtener ID del monitor desde el ComboBox
            int idMonitor = ((ComboboxItem)cbNinos.SelectedItem).Value is int id ? id : 0;

            // 🧠 Obtener materiales de la tabla dbgMateriales
            string materiales = ObtenerMaterialesDesdeTabla();

            ConMDB con = new ConMDB();
            con.AbrirConexion();

            string query;
            if (actividadSeleccionadaId != null)
            {
                query = @"UPDATE Actividades 
                  SET nombre = @nombre, descripcion = @descripcion, fecha = @fecha, imagen = @imagen, id_usuario = @id_usuario, materiales = @materiales 
                  WHERE id = @id";
            }
            else
            {
                query = @"INSERT INTO Actividades (nombre, descripcion, fecha, imagen, id_usuario, materiales) 
                  VALUES (@nombre, @descripcion, @fecha, @imagen, @id_usuario, @materiales)";
            }

            using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@imagen", imagenBytes);
                cmd.Parameters.AddWithValue("@id_usuario", idMonitor);
                cmd.Parameters.AddWithValue("@materiales", materiales);

                if (actividadSeleccionadaId != null)
                    cmd.Parameters.AddWithValue("@id", actividadSeleccionadaId.Value);

                cmd.ExecuteNonQuery();
            }

            con.CerrarConexion();

            MessageBox.Show("Actividad guardada correctamente.");
            actividadSeleccionadaId = null;
            CargarActividades();
            LimpiarFormulario();
        }


        private string ObtenerMaterialesDesdeTabla()
        {
            List<string> materialesList = new List<string>();

            foreach (DataGridViewRow row in dbgMateriales.Rows)
            {
                if (row.IsNewRow) continue;

                string nombre = row.Cells[0].Value?.ToString() ?? "";

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    materialesList.Add($"{nombre}");
                }
            }

            return string.Join(", ", materialesList);
        }


        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtFecha.Clear();
            txtDescripcion.Clear();
            pn_Img.BackgroundImage = null;
            imagenBytes = null;
            dbgMateriales.Columns.Clear();
            dbgMateriales.Rows.Clear();
        }

        private void CargarActividades()
        {
            dgvActividades.Rows.Clear();
            dgvActividades.Columns.Clear();

            // ID oculto
            dgvActividades.Columns.Add("Id", "ID");
            dgvActividades.Columns["Id"].Visible = false;

            dgvActividades.Columns.Add("Nombre", "Nombre");
            dgvActividades.Columns.Add("Fecha", "Fecha");

            // Usamos una columna de texto para almacenar los bytes (sin mostrarla)
            var colImagen = new DataGridViewTextBoxColumn
            {
                Name = "Imagen",
                HeaderText = "Imagen",
                Visible = false
            };
            dgvActividades.Columns.Add(colImagen);

            string query = "SELECT id, nombre, fecha, imagen FROM Actividades";

            ConMDB con = new ConMDB();
            con.AbrirConexion();

            using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Convertimos a base64 para que no dé errores en el grid
                    string imagenBase64 = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("imagen")))
                    {
                        byte[] imgBytes = (byte[])reader["imagen"];
                        imagenBase64 = Convert.ToBase64String(imgBytes);
                    }

                    dgvActividades.Rows.Add(
                        reader["id"].ToString(),
                        reader["nombre"].ToString(),
                        Convert.ToDateTime(reader["fecha"]).ToString("yyyy-MM-dd"),
                        imagenBase64
                    );
                }
            }
            EstilizarTabla(dgvActividades);
            con.CerrarConexion();
        }

        private void dgvActividades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvActividades.Rows[e.RowIndex];

                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txtFecha.Text = fila.Cells["Fecha"].Value?.ToString() ?? "";
                actividadSeleccionadaId = Convert.ToInt32(fila.Cells["Id"].Value);
                

                string id = fila.Cells["Id"].Value?.ToString() ?? "";

                txtDescripcion.Clear(); // ← limpiar el TextBox

                ConMDB con = new ConMDB();
                con.AbrirConexion();

                string query = "SELECT descripcion, imagen, materiales FROM Actividades WHERE id = @id\r\n";

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Descripción en una sola línea para el TextBox
                            txtDescripcion.Text = reader["descripcion"].ToString();
                            string materialesTexto = reader["materiales"].ToString();
                            CargarMaterialesEnTabla(materialesTexto);


                            if (!reader.IsDBNull(reader.GetOrdinal("imagen")))
                            {
                                byte[] imagenBytes = (byte[])reader["imagen"];
                                using (MemoryStream ms = new MemoryStream(imagenBytes))
                                {
                                    Image originalImage = Image.FromStream(ms);
                                    Image resizedImage = new Bitmap(originalImage, pn_Img.Size);
                                    pn_Img.BackgroundImage = resizedImage;
                                    pn_Img.BackgroundImageLayout = ImageLayout.Stretch;
                                }
                            }
                            else
                            {
                                pn_Img.BackgroundImage = null;
                            }
                        }
                    }
                }

                con.CerrarConexion();
            }
        }

        private string ObtenerMaterialesDeActividad(string idActividad)
        {
            string materiales = "";

            ConMDB con = new ConMDB();
            con.AbrirConexion();

            string query = "SELECT materiales FROM Actividades WHERE id = @id";
            using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
            {
                cmd.Parameters.AddWithValue("@id", idActividad);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        materiales = reader["materiales"].ToString();
                    }
                }
            }

            con.CerrarConexion();
            return materiales;
        }

        private void CargarMaterialesEnTabla(string materialesTexto)
        {
            dbgMateriales.Columns.Clear();
            dbgMateriales.Rows.Clear();

            dbgMateriales.Columns.Add("Material", "Materiales");

            if (!string.IsNullOrWhiteSpace(materialesTexto))
            {
                string[] materiales = materialesTexto.Split(',');

                foreach (var item in materiales)
                {
                    dbgMateriales.Rows.Add(item.Trim());
                }
            }

            EstilizarTabla(dbgMateriales);
        }



        private void ConfigurarPaneles()
        {
            // Panel principal
            Panel panelPrincipal = new Panel { Dock = DockStyle.Fill };
            this.Controls.Add(panelPrincipal);

            // Panel izquierdo
            Panel panelIzq = new Panel { Dock = DockStyle.Left, Width = 250 };
            panelPrincipal.Controls.Add(panelIzq);

            // Panel derecho
            Panel panelDer = new Panel { Dock = DockStyle.Right, Width = 400 };
            panelPrincipal.Controls.Add(panelDer);

            // Panel inferior
            Panel panelInf = new Panel { Dock = DockStyle.Bottom, Height = 120 };
            panelPrincipal.Controls.Add(panelInf);

            // Panel centro
            Panel panelCentro = new Panel { Dock = DockStyle.Fill };
            panelPrincipal.Controls.Add(panelCentro);

            // Ahora mueve tus controles manualmente a cada panel
            // Panel izquierdo (datos de actividad)
            panelIzq.Controls.Add(lb_nombre);
            panelIzq.Controls.Add(txtNombre);
            panelIzq.Controls.Add(lb_Fecha);
            panelIzq.Controls.Add(txtFecha);
            panelIzq.Controls.Add(lb_des);
            panelIzq.Controls.Add(txtDescripcion);

            // Panel derecho (imagen y tabla)
            panelDer.Controls.Add(pn_Img);
            panelDer.Controls.Add(dgvActividades);

            // Panel inferior (botones y combo)
            panelInf.Controls.Add(btnGuardar);
            panelInf.Controls.Add(btnSeleccionarImagen);
            panelInf.Controls.Add(btnApuntar);
            panelInf.Controls.Add(cbNinos);
            panelInf.Controls.Add(lbcbNino);
        }


        public void CargarMonitoresComboBox()
        {
            cbNinos.Items.Clear(); // Asegúrate de limpiar antes

            string query = "SELECT id, nombre FROM Usuarios WHERE tipo = 'monitor'"; // Ajusta si usas otra tabla o condición

            ConMDB con = new ConMDB();
            con.AbrirConexion();

            using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string nombre = reader.GetString("nombre");

                        // Puedes almacenar el objeto completo o una tupla en ComboBox
                        cbNinos.Items.Add(new ComboboxItem { Text = nombre, Value = id });
                    }
                }
            }

            con.CerrarConexion();

            // Opcional: selecciona el primero automáticamente
            if (cbNinos.Items.Count > 0)
                cbNinos.SelectedIndex = 0;
        }

        private void EstilizarTabla(DataGridView dgv)
        {
            // Colores generales
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;
            dgv.BorderStyle = BorderStyle.None;

            // Estilo de columnas
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255); // Azul moderno
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 35;

            // Estilo de filas
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255); // Azul claro al seleccionar
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 30;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // gris suave

            // Ajuste automático
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;

            // Quitar bordes de celdas al seleccionar
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false; // Quita la columna de encabezado de filas
        }

        private void Actividad_Load(object sender, EventArgs e)
        {

        }
    }
}
