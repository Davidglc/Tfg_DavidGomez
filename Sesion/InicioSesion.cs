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
            registroForm.VerificarInstancia();
        }

        private void btnInicioSesion_Click(object sender, EventArgs e)
        {
            string usuario = txUsuario.Text.Trim();
            string contraseña = TxContrasena.Text.Trim();

            var adapter = new MongoDBAdapter();
            var (accesoValido, idUsuario, rol) = adapter.VerificarAccesoConRol(usuario, contraseña);

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
                    MonitorForm monitorForm = new MonitorForm();
                    monitorForm.Show();
                }
                else if (rol == "Padre")
                {
                    PadresForm padresForm = new PadresForm();
                    padresForm.Show();
                }

                this.Hide(); // Ocultar el formulario de inicio de sesión
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
    }

}

