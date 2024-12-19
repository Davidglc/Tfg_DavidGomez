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
            // Obtener los datos ingresados por el usuario
            string usuario = txUsuario.Text.Trim();
            string contraseña = txUsuario.Text.Trim();

            // Verificar que los campos no estén vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, completa todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var adapter = new MongoDBAdapter();

                var (accesoValido, rol) = adapter.VerificarAccesoConRol(usuario, contraseña);

                if (accesoValido)
                {
                    MessageBox.Show($"Bienvenido, {usuario}. Tu rol es {rol}.");

                    if (rol == "Monitor")
                    {
                        MonitorForm mf = new MonitorForm();
                        mf.VerificarInstancia();
                    }   
                    else if (rol == "Padre")
                    {
                        PadresForm padresForm = new PadresForm();
                        padresForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Rol no reconocido. Contacte con el administrador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error durante el inicio de sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
