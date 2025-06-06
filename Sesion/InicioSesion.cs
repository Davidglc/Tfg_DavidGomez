using MongoDB.Bson;
using TFG_DavidGomez.Clases;
using TFG_DavidGomez.Clases.Adaptador;
using TFG_DavidGomez.Clases.Conexion.TFG_DavidGomez;
using TFG_DavidGomez.Sesion;
using System.Drawing.Drawing2D;

namespace TFG_DavidGomez
{

    /// <summary>
    /// Clase que representa el formulario de inicio de sesi�n para los usuarios del sistema.
    /// Permite el acceso basado en credenciales y redirige a diferentes vistas seg�n el rol del usuario.
    /// </summary>
    public partial class InicioSesion : Form
    {

        /// <summary>
        /// Constructor por defecto de la clase.
        /// Inicializa los componentes del formulario y asigna eventos.
        /// </summary>
        public InicioSesion()
        {
            InitializeComponent();
            RedondearBoton(btnInicioSesion, 20);
            RedondearBoton(btnRegistrarse, 20);
            this.FormClosed += CerrarAplicacion;
        }


        /// <summary>
        /// Evento que se ejecuta al hacer clic en el bot�n "Registrarse".
        /// Abre el formulario de registro de usuarios.
        /// </summary>
        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Registrarse registroForm = new Registrarse();
            registroForm.Show();
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


        /// <summary>
        /// Evento que se ejecuta al hacer clic en el bot�n "Iniciar Sesi�n".
        /// Valida las credenciales y redirige al formulario correspondiente seg�n el rol del usuario.
        /// </summary>
        /// 

        private void btnInicioSesion_Click(object sender, EventArgs e)
        {
            string usuario = txUsuario.Text.Trim();
            string contrase�a = TxContrasena.Text.Trim();

            MariaDbAdapter ma = new MariaDbAdapter(); // Adaptador para MariaDB

            var (accesoValido, idUsuario, rol) = ma.VerificarAccesoConRol(usuario, contrase�a);

            if (accesoValido)
            {
                // Guardar los datos de la sesi�n
                SesionIniciada.IdUsuario = idUsuario;
                SesionIniciada.NombreUsuario = usuario;
                SesionIniciada.Rol = rol;

                MessageBox.Show($"Bienvenido, {usuario}.");

                if (rol == "monitor")
                {
                    try
                    {
                        // Obtener la actividad correspondiente al monitor por la fecha actual
                        var actividad = ma.ObtenerActividadPorDia(DateTime.Now);

                        if (actividad == null)
                        {
                            MessageBox.Show("No se encontr� una actividad asignada al monitor para hoy.", "Sin actividad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Obtener detalles de la actividad
                        string nombreActividad = actividad["nombre"].ToString();
                        int idActividad = Convert.ToInt32(actividad["id"]);
                        var materiales = ma.ObtenerMaterialesPorActividad(idActividad);
                        var ninos = ma.ObtenerNinosPorActividad(idActividad);

                        var nombresNinos = ninos.Select(n => $"Nombre: {n.Nombre}, Apellidos: {n.Apellidos}, Edad: {n.Edad}").ToList();

                        // Mostrar formulario monitor
                        MonitorForm monitorForm = new MonitorForm(
                            DateTime.Now,
                            nombreActividad,
                            materiales,
                            nombresNinos
                        );

                        this.Hide();
                        monitorForm.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar datos del monitor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (rol == "padre")
                {
                    Eleccion Eleccion = new Eleccion();
                    this.Hide();
                    Eleccion.Show();
                }
                else if (rol == "admin")
                {
                    try
                    {
                        // Obtener la actividad correspondiente al d�a actual
                        var actividad = ma.ObtenerActividadPorDia(DateTime.Today);

                        if (actividad != null)
                        {
                            // Extraer datos de la actividad
                            string nombreActividad = actividad["nombre"].ToString();
                            int idActividad = Convert.ToInt32(actividad["id"]);

                            var materiales = ma.ObtenerMaterialesPorActividad(idActividad);
                            var ninos = ma.ObtenerNinosPorActividad(idActividad);

                            var nombresNinos = ninos.Select(n => $"Nombre: {n.Nombre}, Apellidos: {n.Apellidos}, Edad: {n.Edad}").ToList();

                            // Mostrar formulario Monitor con datos reales
                            MonitorForm monitorForm = new MonitorForm(
                                DateTime.Today,
                                nombreActividad,
                                materiales,
                                nombresNinos
                            );

                            // Mostrar opciones de admin tambi�n
                            monitorForm.a�adirMonitorToolStripMenuItem.Visible = true;
                            monitorForm.modificarActividadToolStripMenuItem.Visible = true;

                            this.Hide();
                            monitorForm.Show();
                        }
                        else
                        {
                            // No hay actividad: cargar modo panel administraci�n
                            MonitorForm monitorForm = new MonitorForm(
                                DateTime.Today,
                                "Panel de Administraci�n",
                                new List<string>(),
                                new List<string>()
                            );

                            monitorForm.a�adirMonitorToolStripMenuItem.Visible = true;
                            monitorForm.modificarActividadToolStripMenuItem.Visible = true;

                            this.Hide();
                            monitorForm.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar datos del administrador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Usuario o contrase�a incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void btnInicioSesion_Click(object sender, EventArgs e)
        // {
        //    string usuario = txUsuario.Text.Trim();
        //    string contrase�a = TxContrasena.Text.Trim();
        //    MongoDBAdapter ma = new MongoDBAdapter();

        //    var (accesoValido, idUsuario, rol) = ma.VerificarAccesoConRol(usuario, contrase�a);

        //    if (accesoValido)
        //    {
        //        // Guardar los datos de la sesi�n
        //        SesionIniciada.IdUsuario = idUsuario; // Almacena el ID del usuario desde la base de datos
        //        SesionIniciada.NombreUsuario = usuario;
        //        SesionIniciada.Rol = rol;

        //        MessageBox.Show($"Bienvenido, {usuario}.");

        //        // Redirigir seg�n el rol
        //        if (rol == "Monitor")
        //        {
        //            try
        //            {
        //                // Obtener la actividad correspondiente al monitor por la fecha actual
        //                BsonDocument actividad = ma.ObtenerActividadPorDia(DateTime.Now); // Obtener la actividad del d�a actual

        //                if (actividad == null)
        //                {
        //                    MessageBox.Show("No se encontr� una actividad asociada al monitor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return;
        //                }

        //                // Obtener el nombre de la actividad desde el BsonDocument
        //                string nombreActividad = actividad.GetValue("Nombre").AsString;

        //                // Obtener los materiales de la actividad
        //                var materiales = ma.ObtenerMaterialesPorActividad(actividad["_id"].AsObjectId);

        //                // Obtener los ni�os inscritos en la actividad
        //                var ninos = ma.ObtenerNinosPorActividad(actividad["_id"].AsObjectId);
        //                var nombresNinos = ninos.Select(n => $"Nombre: {n.Nombre}, Apellidos: {n.Apellidos}, Edad: {n.Edad}").ToList();

        //                // Crear y mostrar el formulario del monitor
        //                MonitorForm monitorForm = new MonitorForm(
        //                    DateTime.Now,
        //                    nombreActividad,
        //                    materiales,
        //                    nombresNinos
        //                );
        //                this.Hide();
        //                monitorForm.Show();
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show($"Error al cargar el formulario del monitor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //        else if (rol == "Padre")
        //        {
        //            PadresForm padresForm = new PadresForm();
        //            this.Hide();
        //            padresForm.Show();
        //        }
        //        else if (rol == "Admin")
        //        {

        //            // Obtener la actividad correspondiente al monitor por la fecha actual
        //            BsonDocument actividad = ma.ObtenerActividadPorDia(DateTime.Now); // Obtener la actividad del d�a actual

        //            if (actividad == null)
        //            {
        //                MessageBox.Show("No se encontr� una actividad asociada al monitor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }

        //            // Obtener el nombre de la actividad desde el BsonDocument
        //            string nombreActividad = actividad.GetValue("Nombre").AsString;

        //            // Obtener los materiales de la actividad
        //            var materiales = ma.ObtenerMaterialesPorActividad(actividad["_id"].AsObjectId);

        //            // Obtener los ni�os inscritos en la actividad
        //            var ninos = ma.ObtenerNinosPorActividad(actividad["_id"].AsObjectId);
        //            var nombresNinos = ninos.Select(n => $"Nombre: {n.Nombre}, Apellidos: {n.Apellidos}, Edad: {n.Edad}").ToList();

        //            // Crear y mostrar el formulario del monitor
        //            MonitorForm monitorForm = new MonitorForm(
        //                DateTime.Now,
        //                nombreActividad,
        //                materiales,
        //                nombresNinos
        //            );
        //            monitorForm.a�adirMonitorToolStripMenuItem.Visible = true;
        //            this.Hide();
        //            monitorForm.Show();
        //        }

        //    }
        //    else
        //    {
        //        MessageBox.Show("Usuario o contrase�a incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        /// <summary>
        /// Verifica si un objeto es una instancia de <see cref="InicioSesion"/> y lo muestra como un di�logo.
        /// </summary>

        public void VerificarInstancia()
        {
            object obj = new InicioSesion();

            if (obj is InicioSesion ini)
            {
                Console.WriteLine("El objeto es una instancia de PadresForm.");
                ini.ShowDialog();
            }
            else
            {
                Console.WriteLine("El objeto no es una instancia de PadresForm.");
            }
        }


        /// <summary>
        /// Limpia los campos de texto del formulario.
        /// </summary>
        public void LimpiarCampos()
        {
            txUsuario.Text = string.Empty; // Limpia el campo del usuario
            TxContrasena.Text = string.Empty; // Limpia el campo de la contrase�a
        }


        /// <summary>
        /// Evento que se ejecuta al redimensionar el formulario.
        /// Centra la posici�n de ciertos elementos en el formulario.
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            // Calcular la posici�n central
            int x = (this.ClientSize.Width - this.Width) / 2;
            int y = (this.ClientSize.Height - this.Height) / 2;

            // Mover el ListBox al centro
            this.Location = new Point(x, y);
        }

        /// <summary>
        /// Evento que se ejecuta al cerrar el formulario.
        /// Finaliza la aplicaci�n.
        /// </summary>

        private void CerrarAplicacion(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void TxContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el sonido al presionar Enter
                btnInicioSesion.PerformClick(); // Ejecuta el bot�n
            }
        }

    }

}

