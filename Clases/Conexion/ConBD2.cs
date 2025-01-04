using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace TFG_DavidGomez.Clases.Conexion
{
    /// <summary>
    /// Clase para manejar la conexión con la base de datos MongoDB Atlas.
    /// Proporciona métodos para inicializar la conexión, obtener la base de datos y acceder a colecciones específicas.
    /// </summary>
    public class ConBD2
    {
        // Instancia de la base de datos MongoDB
        private static IMongoDatabase database;

        // Cliente para conectarse al servidor MongoDB
        private static MongoClient client;

        // URI de conexión para el clúster de MongoDB Atlas
        private const string connectionUri = "mongodb+srv://davidbasketyague:F1zBaN8Z176mHXuL@clusterludo.jr1kr.mongodb.net/?retryWrites=true&w=majority&appName=ClusterLudo";

        /// <summary>
        /// Constructor estático para inicializar la conexión con MongoDB Atlas.
        /// Llama al método <see cref="InicializarConexion"/> para establecer la conexión.
        /// </summary>
        public ConBD2()
        {
            InicializarConexion();
        }

        /// <summary>
        /// Método estático para inicializar la conexión con MongoDB Atlas.
        /// Configura el cliente, obtiene la base de datos y verifica la conexión mediante un comando de ping.
        /// </summary>
        private static void InicializarConexion()
        {
            try
            {
                // Configurar los ajustes del cliente MongoDB utilizando la URI de conexión
                var settings = MongoClientSettings.FromConnectionString(connectionUri);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);

                // Crear el cliente de MongoDB
                client = new MongoClient(settings);

                // Obtener la base de datos "Ludotecas"
                database = client.GetDatabase("Ludotecas");

                // Ejecutar un comando de ping para verificar la conexión con el servidor
                var result = database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Conexión exitosa a MongoDB Atlas. Respuesta de ping: " + result.ToJson());
            }
            catch (Exception ex)
            {
                // Capturar y mostrar cualquier error al conectar con MongoDB
                Console.WriteLine("Error al conectar con MongoDB Atlas: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la instancia de la base de datos MongoDB.
        /// Lanza una excepción si la base de datos no está disponible.
        /// </summary>
        /// <returns>La instancia de <see cref="IMongoDatabase"/> conectada.</returns>
        public static IMongoDatabase GetDatabase()
        {
            if (database == null)
                throw new InvalidOperationException("La base de datos no está disponible.");
            return database;
        }

        /// <summary>
        /// Obtiene una colección específica de la base de datos.
        /// </summary>
        /// <typeparam name="T">El tipo de documentos que contiene la colección.</typeparam>
        /// <param name="nombreColeccion">El nombre de la colección.</param>
        /// <returns>La colección especificada de tipo <see cref="IMongoCollection{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">Se lanza si la base de datos no está disponible.</exception>
        public static IMongoCollection<T> GetCollection<T>(string nombreColeccion)
        {
            if (database == null)
                throw new InvalidOperationException("La base de datos no está disponible.");
            return database.GetCollection<T>(nombreColeccion);
        }

        /// <summary>
        /// Verifica si hay una conexión activa con MongoDB y la devuelve.
        /// Si no hay conexión activa, intenta restablecer la conexión automáticamente.
        /// </summary>
        /// <returns>La instancia de <see cref="IMongoDatabase"/> conectada.</returns>
        /// <exception cref="InvalidOperationException">Se lanza si no se puede establecer la conexión.</exception>
        public static IMongoDatabase ObtenerConexionActiva()
        {
            // Verificar si la base de datos o el cliente están inicializados
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
