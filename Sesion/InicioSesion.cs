using MongoDB.Bson;
using TFG_DavidGomez.Clases;
using TFG_DavidGomez.Clases.Adaptador;
using TFG_DavidGomez.Clases.Conexion.TFG_DavidGomez;
using TFG_DavidGomez.Sesion;

namespace TFG_DavidGomez
{
    public partial class InicioSesion : Form
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Registrarse registroForm = new Registrarse();
            registroForm.Show();
        }

        private void btnInicioSesion_Click(object sender, EventArgs e)
        {
            string usuario = txUsuario.Text.Trim();
            string contraseña = TxContrasena.Text.Trim();
            MongoDBAdapter ma = new MongoDBAdapter();
            
            var (accesoValido, idUsuario, rol) = ma.VerificarAccesoConRol(usuario, contraseña);

            if (accesoValido)
            {
                // Guardar los datos de la sesión
                SesionIniciada.IdUsuario = idUsuario; // Almacena el ID del usuario desde la base de datos
                SesionIniciada.NombreUsuario = usuario;
                SesionIniciada.Rol = rol;

                MessageBox.Show($"Bienvenido, {usuario}. Tu rol es {rol}.");

                // Redirigir según el rol
                if (rol == "Monitor")
                {
                    try
                    {
                        // Obtener la actividad correspondiente al monitor por la fecha actual
                        BsonDocument actividad = ma.ObtenerActividadPorDia(DateTime.Now); // Obtener la actividad del día actual

                        if (actividad == null)
                        {
                            MessageBox.Show("No se encontró una actividad asociada al monitor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Obtener el nombre de la actividad desde el BsonDocument
                        string nombreActividad = actividad.GetValue("Nombre").AsString;

                        // Obtener los materiales de la actividad
                        var materiales = ma.ObtenerMaterialesPorActividad(actividad["_id"].AsObjectId);

                        // Obtener los niños inscritos en la actividad
                        var ninos = ma.ObtenerNinosPorActividad(actividad["_id"].AsObjectId);
                        var nombresNinos = ninos.Select(n => $"{n.Nombre} {n.Apellidos}").ToList();

                        // Crear y mostrar el formulario del monitor
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
                        MessageBox.Show($"Error al cargar el formulario del monitor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }else if (rol == "Padre")
                {
                    PadresForm padresForm = new PadresForm();
                    this.Hide();
                    padresForm.Show();
                }

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        public void LimpiarCampos()
        {
            txUsuario.Text = string.Empty; // Limpia el campo del usuario
            TxContrasena.Text = string.Empty; // Limpia el campo de la contraseña
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Calcular la posición central
            int x = (this.ClientSize.Width - this.Width) / 2;
            int y = (this.ClientSize.Height - this.Height) / 2;

            // Mover el ListBox al centro
            this.Location = new Point(x, y);
        }


    }

}

