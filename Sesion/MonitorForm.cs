﻿using MongoDB.Bson;
using MySql.Data.MySqlClient;
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
using TFG_DavidGomez.Clases.Conexion;
using TFG_DavidGomez.Sesion;

namespace TFG_DavidGomez
{

    /// <summary>
    /// Clase que representa el formulario de gestión para monitores.
    /// Proporciona funcionalidades como visualizar actividades, materiales asociados, niños asignados,
    /// consultar datos personales y gestionar monitores.
    /// </summary>
    public partial class MonitorForm : Form
    {

        /// <summary>
        /// Constructor por defecto de la clase.
        /// Inicializa el formulario de Monitor.
        /// </summary>
        public MonitorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor que inicializa el formulario con información específica de una actividad.
        /// </summary>
        /// <param name="fecha">Fecha de la actividad.</param>
        /// <param name="actividad">Nombre de la actividad.</param>
        /// <param name="materiales">Lista de materiales necesarios para la actividad.</param>
        /// <param name="niños">Lista de niños inscritos en la actividad.</param>

        public MonitorForm(DateTime fecha, string actividad, List<string> materiales, List<String> niños)
        {
            InitializeComponent();

            LbFecha2.Text = fecha.ToString("dd/MM/yyyy");
            LbAtividad2.Text = actividad;
            AjustarTablaNinos();

            CargarMateriales(materiales);
            CargarNinos(niños);
        }

        private void AjustarTablaNinos()
        {
            // Establecer el tamaño de la tabla
            dataGridNinos.Width = 500;  // Puedes aumentar este valor si quieres más espacio
            dataGridNinos.Height = 200;


            // Ajustar el tamaño de las columnas si ya fueron creadas
            if (dataGridNinos.Columns.Count > 0)
            {
                dataGridNinos.Columns[0].Width = 200; // Nombre del Niño
                dataGridNinos.Columns[1].Width = 100; // Edad
                dataGridNinos.Columns[2].Width = 400; // Nombre del Padre
            }
        }


        private void CargarMateriales(List<string> materiales)
        {
            dataGridMateriales.Columns.Clear();
            dataGridMateriales.Rows.Clear();
            dataGridMateriales.Columns.Add("Material", "Material Necesario");

            foreach (var material in materiales)
            {
                dataGridMateriales.Rows.Add(material);
            }
            EstilizarTabla(dataGridMateriales);
        }


        private void CargarNinos(List<string> listaNinos)
        {
            dataGridNinos.Columns.Clear();
            dataGridNinos.Rows.Clear();

            dataGridNinos.Columns.Add("Nombre", "Nombre del Niño");
            dataGridNinos.Columns.Add("Edad", "Edad");
            dataGridNinos.Columns.Add("Padre", "Nombre del Padre");

            ConMDB con = new ConMDB();
            con.AbrirConexion();

            foreach (var info in listaNinos)
            {
                // Ejemplo de formato: "Nombre: Juan, Apellidos: Pérez, Edad: 10"
                string nombre = "", edadStr = "", nombrePadre = "No encontrado";
                int edad = 0;

                var partes = info.Split(',');

                foreach (var parte in partes)
                {
                    if (parte.Trim().StartsWith("Nombre:"))
                        nombre = parte.Replace("Nombre:", "").Trim();
                    else if (parte.Trim().StartsWith("Edad:"))
                        edadStr = parte.Replace("Edad:", "").Trim();
                }

                int.TryParse(edadStr, out edad);

                // Buscar nombre del padre según el nombre del niño y edad
                string query = @"
                    SELECT u.nombre 
                    FROM Usuarios u
                    JOIN Ninos n ON n.id_padre = u.id
                    WHERE n.nombre = @nombre ";

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@edad", edad);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nombrePadre = reader["nombre"].ToString();
                        }
                    }
                }

                dataGridNinos.Rows.Add(nombre, edad, nombrePadre);
            }
            EstilizarTabla(dataGridNinos);
            con.CerrarConexion();
        }




        /// <summary>
        /// Verifica si un objeto es una instancia de la clase <see cref="MonitorForm"/>.
        /// Si lo es, muestra el formulario.
        /// </summary>

        public void VerificarInstancia()
        {
            object obj = new MonitorForm();

            if (obj is MonitorForm monitorForm)
            {
                Console.WriteLine("El objeto proporcionado es de tipo MonitorForm.");
                monitorForm.Show();
            }
            else
            {
                Console.WriteLine("El objeto proporcionado no es de tipo MonitorForm.");
            }
        }


        /// <summary>
        /// Evento que se ejecuta al seleccionar "Cerrar sesión" en el menú.
        /// Cierra la sesión actual y redirige al formulario de inicio de sesión.
        /// </summary>
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cerrar sesión
            SesionIniciada si = new SesionIniciada();
            si.CerrarSesion();
            this.Close();

            // Crear una nueva instancia del formulario de inicio de sesión
            InicioSesion inicioSesion = new InicioSesion();

            // Limpiar los campos de usuario y contraseña
            inicioSesion.LimpiarCampos();

            // Mostrar el formulario de inicio de sesión
            inicioSesion.VerificarInstancia();

        }


        /// <summary>
        /// Evento que se ejecuta al seleccionar "Datos personales" en el menú.
        /// Carga y muestra los datos personales del monitor desde la base de datos.
        /// </summary>
        private void datosPersonalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del usuario desde la sesión
                // Obtener el ID del usuario desde la sesión
                int id = int.Parse(SesionIniciada.IdUsuario);

                // Crear una instancia de MongoDBAdapter
                MariaDbAdapter ma = new MariaDbAdapter();

                // Obtener el usuario desde la base de datos
                UsuarioMonitor u = ma.ObtenerUsuarioPorIdMoni(id);

                // Validar si el usuario fue encontrado
                if (u == null)
                {
                    MessageBox.Show("No se encontró información para este usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mostrar los datos personales del usuario
                DatosPersonales dp = new DatosPersonales(u);
                dp.VerificarInstancia2(u);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos personales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Evento que se ejecuta al seleccionar "Añadir monitor" en el menú.
        /// Abre el formulario de registro con la opción específica para registrar monitores.
        /// </summary>

        private void añadirMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registrarse registrarse = new Registrarse();
            registrarse.BtnGuardarMoni.Visible = true;
            registrarse.btnGuardar.Visible = false;
            registrarse.ShowDialog();
        }

        private void modificarActividadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Actividad a = new Actividad();
            a.btnApuntar.Visible = false;
            a.lbcbNino.Visible = false;
            a.lblMonitor.Visible = true;
            a.cbNinos.Visible = true;
            a.btnGuardar.Visible = true;
            a.txtNombre.Visible = true;
            a.txtFecha.Visible = true;
            a.txtDescripcion.Visible = true;
            a.dgvActividades.Visible = true;
            a.btnSeleccionarImagen.Visible = true;
            a.btn_Eliminar.Visible = true;
            a.btn_LimpiarCampos.Visible = true;
            a.CargarMonitoresComboBox();
            a.ShowDialog();
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

        //private void CerrarAplicacion(object sender, FormClosedEventArgs e)
        //{
        //    Application.Exit();
        //}


    }

}
