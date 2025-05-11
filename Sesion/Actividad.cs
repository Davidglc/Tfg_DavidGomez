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

namespace TFG_DavidGomez.Sesion
{
    public partial class Actividad : Form
    {
        public Actividad()
        {
            InitializeComponent();
        }

        public void CargarDatos(Actividades actividad)
        {
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
        }

    }
}
