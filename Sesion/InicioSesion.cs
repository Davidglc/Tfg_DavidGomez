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
            // Abrir directamente el formulario de registro
            Registrarse registroForm = new Registrarse();
            registroForm.ShowDialog();
        }
    }
}
