using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace TFG_DavidGomez.Clases.Conexion
{
    public class ConBD2
    {
        private static IMongoDatabase database;
        private static MongoClient client;

        // URI de conexión para MongoDB Atlas
        private const string connectionUri = "mongodb+srv://davidbasketyague:F1zBaN8Z176mHXuL@clusterludo.jr1kr.mongodb.net/?retryWrites=true&w=majority&appName=ClusterLudo";

        // Constructor estático para inicializar la conexión
        public ConBD2()
        {
            InicializarConexion();
        }

        private static void InicializarConexion()
        {
            try
            {
                // Configurar MongoDB con la URI
                var settings = MongoClientSettings.FromConnectionString(connectionUri);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);

                // Crear el cliente y conectar al servidor
                client = new MongoClient(settings);

                // Obtener la base de datos
                database = client.GetDatabase("Ludotecas");

                // Ping para verificar la conexión
                var result = database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Conexión exitosa a MongoDB Atlas. Respuesta de ping: " + result.ToJson());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con MongoDB Atlas: " + ex.Message);
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
            if (database == null)
                throw new InvalidOperationException("La base de datos no está disponible.");
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
