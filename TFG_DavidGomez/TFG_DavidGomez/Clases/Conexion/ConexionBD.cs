using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG_DavidGomez.Clases.Conexion
{
    public class ConexionBD
    {
        private static IMongoDatabase database;
        private static MongoClient client;

        
        static ConexionBD()
        {
            string connectionString = "mongodb://localhost:27017";
            try
            {
                client = new MongoClient(connectionString);
                database = client.GetDatabase("ludotecas"); 
                Console.WriteLine("Conexión exitosa a MongoDB.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con MongoDB: " + ex.Message);
            }
        }

        public static IMongoDatabase GetDatabase()
        {
            if (database == null)
                throw new InvalidOperationException("La base de datos no está disponible.");
            return database;
        }

        public static IMongoCollection<T> GetCollection<T>(string nombreColeccion)
        {
            return database.GetCollection<T>(nombreColeccion);
        }
    }
}
