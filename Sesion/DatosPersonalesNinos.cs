using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TFG_DavidGomez.Clases;
using TFG_DavidGomez.Clases.Conexion;

namespace TFG_DavidGomez.Sesion
{

    /// <summary>
    /// Formulario para gestionar los datos personales de los niños.
    /// Permite cargar, visualizar y actualizar la información de los niños asociados a un padre en una base de datos MongoDB.
    /// </summary>
    /// 
    public partial class DatosPersonalesNinos : Form
    {
        private int idPadre;
        private List<Nino> listaNinos;

        public DatosPersonalesNinos(int idPadre)
        {

            
            InitializeComponent();
            this.idPadre = idPadre;
            RedondearBoton(btn_Aceptar, 20);
            this.WindowState = FormWindowState.Maximized;
            dgvNinos.AutoGenerateColumns = false;
            ConfigurarGrid();
            CargarNinos();
        }

        private void RedondearBoton(Button btn, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(btn.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(btn.Width - radio, btn.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, btn.Height - radio, radio, radio, 90, 90);
            path.CloseAllFigures();
            btn.Region = new Region(path);
        }

        private void ConfigurarGrid()
        {
            dgvNinos.Columns.Clear();

            dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Visible = false });
            dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Apellidos", HeaderText = "Apellidos" });
            dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DNI", HeaderText = "DNI" });
            dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaNacimiento", HeaderText = "Fecha de nacimiento" });
            dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Edad", HeaderText = "Edad" });

            // Dimensiones más contenidas
            dgvNinos.Width = 500;
            dgvNinos.Height = 120;

            // Centrar horizontalmente en el formulario
            dgvNinos.Left = (this.ClientSize.Width - dgvNinos.Width) / 2;
            // Alinearlo con la parte superior de los TextBoxes (o ajusta a tu gusto)
            dgvNinos.Top = lblUsuario.Top;

            dgvNinos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvNinos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        //private void ConfigurarGrid()
        //{
        //    dgvNinos.Columns.Clear();

        //    dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Visible = false });
        //    dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Nombre" });
        //    dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Apellidos", HeaderText = "Apellidos" });
        //    dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DNI", HeaderText = "DNI" });
        //    dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FechaNacimiento", HeaderText = "Fecha de nacimiento" });
        //    dgvNinos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Edad", HeaderText = "Edad" });

        //    //dgvNinos.Dock = DockStyle.None;
        //    dgvNinos.Width = 600;
        //    dgvNinos.Height = 150;

        //    // Posicionar manualmente al lado de los TextBox (ajusta según el ancho del grupo de controles)
        //    dgvNinos.Left = 60; // <-- Aquí decides el desplazamiento lateral
        //    dgvNinos.Top = lblUsuario.Top;

        //    dgvNinos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //    dgvNinos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //}



        private void CargarNinos()
        {
            listaNinos = new List<Nino>();
            ConMDB con = new ConMDB();
            con.AbrirConexion();

            string query = "SELECT id, nombre, apellidos, dni, fecha_nacimiento, edad FROM Ninos WHERE id_padre = @id_padre";
            using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
            {
                cmd.Parameters.AddWithValue("@id_padre", idPadre);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaNinos.Add(new Nino
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre"),
                            Apellidos = reader.GetString("apellidos"),
                            DNI = reader.IsDBNull(reader.GetOrdinal("dni")) ? null : reader.GetString("dni"),
                            FechaNacimiento = reader.GetDateTime("fecha_nacimiento"),
                            Edad = reader.GetInt32("edad")
                        });
                    }
                }
            }
            EstilizarTabla(dgvNinos);
            con.CerrarConexion();
            dgvNinos.DataSource = null;
            dgvNinos.DataSource = listaNinos;
        }

        private void dgvNinos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < listaNinos.Count)
            {
                var nino = listaNinos[e.RowIndex];
                txUsuario.Text = nino.Nombre;
                txApellidos.Text = nino.Apellidos;
                txDNI.Text = nino.DNI;
                txFnac.Text = nino.FechaNacimiento.ToString("yyyy/MM/dd");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvNinos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un niño para editar.");
                return;
            }

            int index = dgvNinos.SelectedRows[0].Index;
            var nino = listaNinos[index];

            string nombre = txUsuario.Text.Trim();
            string apellidos = txApellidos.Text.Trim();
            string dni = txDNI.Text.Trim().ToUpper();
            DateTime fechaNacimiento;

            if (!DateTime.TryParseExact(txFnac.Text.Trim(), "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNacimiento))
            {
                MessageBox.Show("Formato de fecha inválido (yyyy/MM/dd).");
                return;
            }

            // ✅ Calcular edad automáticamente
            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }

            if (!string.IsNullOrEmpty(dni) && !System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8}[A-Z]$"))
            {
                MessageBox.Show("El DNI debe tener 8 cifras seguidas de una letra mayúscula (ejemplo: 12345678A).");
                return;
            }

            ConMDB con = new ConMDB();
            con.AbrirConexion();

            if (!string.IsNullOrEmpty(dni))
            {
                string dniNiñosQuery = "SELECT COUNT(*) FROM Ninos WHERE dni = @dni AND id != @id";
                using (MySqlCommand cmd = new MySqlCommand(dniNiñosQuery, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);
                    cmd.Parameters.AddWithValue("@id", nino.Id);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("El DNI ya está en uso por otro niño.");
                        con.CerrarConexion();
                        return;
                    }
                }

                string dniUsuariosQuery = "SELECT COUNT(*) FROM Usuarios WHERE dni = @dni";
                using (MySqlCommand cmd = new MySqlCommand(dniUsuariosQuery, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("El DNI ya está registrado en otro usuario.");
                        con.CerrarConexion();
                        return;
                    }
                }
            }

            string updateQuery = @"UPDATE Ninos SET 
                nombre = @nombre, 
                apellidos = @apellidos, 
                dni = @dni, 
                fecha_nacimiento = @fechaNacimiento, 
                edad = @edad 
                WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(updateQuery, con.ObtenerConexion()))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellidos", apellidos);
                cmd.Parameters.AddWithValue("@dni", string.IsNullOrEmpty(dni) ? DBNull.Value : (object)dni);
                cmd.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
                cmd.Parameters.AddWithValue("@edad", edad);
                cmd.Parameters.AddWithValue("@id", nino.Id);

                int filas = cmd.ExecuteNonQuery();
                if (filas > 0)
                {
                    MessageBox.Show("Niño actualizado correctamente.");
                    CargarNinos();
                }
                else
                {
                    MessageBox.Show("No se actualizó ningún registro.");
                }
            }

            con.CerrarConexion();
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
    }

}

