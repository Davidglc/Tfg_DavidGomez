using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG_DavidGomez.Clases
{
    internal class SesionIniciada
    {

        public static string IdUsuario { get; set; }
        public static string NombreUsuario { get; set; }
        public static string Rol { get; set; }

        public static bool SesionIni => !string.IsNullOrEmpty(IdUsuario);

        /// <summary>
        /// Limpia los datos de la sesión.
        /// </summary>
        public static void CerrarSesion()
        {
            IdUsuario = "";
            NombreUsuario = "";
            Rol = "";
        }

    }
}
