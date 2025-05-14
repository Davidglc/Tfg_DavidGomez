using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace TFG_DavidGomez.Clases.Conexion
{
    internal class ConMDB
    {
        private MySqlConnection conexion;
        string cadenaConexion = "Server=localhost;Port=3306;Database=ludotecas;User ID=ludotecas;Password=org;";

        public ConMDB()
        {
            
            conexion = new MySqlConnection(cadenaConexion);
        }

        public MySqlConnection ObtenerConexion()
        {
            return conexion;
        }

        public void AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
                conexion.Open();
        }

        public void CerrarConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();
        }

    }
}
