using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace TFG_DavidGomez.Clases.Conexion
{
    namespace TFG_DavidGomez
    {
        public class ConexionBD
        {
            private static IMongoDatabase database;
            private static MongoClient client;

            // Constructor estático para inicializar la conexión
            static ConexionBD()
            {
                InicializarConexion();
            }

            private static void InicializarConexion()
            {
                // Cambiar el connectionString dependiendo de tu configuración de Docker
                string connectionString = "mongodb://localhost:27017"; // Cambiar según tu entorno

                try
                {
                    client = new MongoClient(connectionString);
                    database = client.GetDatabase("ludotecas"); // Nombre de la base de datos
                    Console.WriteLine("Conexión exitosa a MongoDB.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar con MongoDB: " + ex.Message);
                }
            }

            // Método para obtener la base de datos
            public static IMongoDatabase GetDatabase()
            {
                if (database == null)
                    throw new InvalidOperationException("La base de datos no está disponible.");
                return database;
            }

            // Método para obtener una colección específica
            public static IMongoCollection<T> GetCollection<T>(string nombreColeccion)
            {
                return database.GetCollection<T>(nombreColeccion);
            }

            /// <summary>
            /// Método para verificar si hay una conexión activa y devolverla.
            /// Si no hay conexión activa, intenta reconectar automáticamente.
            /// </summary>
            public static IMongoDatabase ObtenerConexionActiva()
            {
                if (database == null || client == null)
                {
                    Console.WriteLine("No hay conexión activa. Reintentando conexión...");
                    InicializarConexion();

                    if (database == null)
                        throw new InvalidOperationException("No se pudo establecer la conexión con la base de datos.");
                }

                Console.WriteLine("Conexión activa encontrada.");
                return database;
            }
        }

    }


}
