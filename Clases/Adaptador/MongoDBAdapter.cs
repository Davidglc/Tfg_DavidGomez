using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TFG_DavidGomez.Clases.Conexion;

namespace TFG_DavidGomez.Clases.Adaptador
{

    /// <summary>
    /// Clase que actúa como adaptador para interactuar con la base de datos MongoDB.
    /// Proporciona métodos para manejar actividades, inscripciones, usuarios y otros datos relacionados.
    /// </summary>
    internal class MongoDBAdapter
    {
        private readonly IMongoDatabase _database;


        /// <summary>
        /// Constructor que inicializa la conexión utilizando la clase <see cref="ConBD2"/>.
        /// </summary>
        public MongoDBAdapter()
        {
            // Usar la conexión existente de la clase ConexionBDAtlas
            _database = ConBD2.ObtenerConexionActiva();
        }

        /// <summary>
        /// Obtiene la actividad programada para un día específico.
        /// </summary>
        /// <param name="dia">Fecha del día para el cual se desea obtener la actividad.</param>
        /// <returns>Un documento BSON que representa la actividad, o null si no se encuentra.</returns>
        public BsonDocument ObtenerActividadPorDia(DateTime dia)
        {
            // Obtener la colección "Actividades"
            var actividadesCollection = _database.GetCollection<BsonDocument>("Actividades");

            // Crear un rango de fecha para incluir todas las actividades del día
            var inicioDia = dia.Date;
            var finDia = dia.Date.AddDays(1);

            // Crear el filtro para las actividades del día
            var filtro = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("Fecha", inicioDia),
                Builders<BsonDocument>.Filter.Lt("Fecha", finDia)
            );

            // Obtener la primera actividad que coincida
            return actividadesCollection.Find(filtro).FirstOrDefault();
        }


        /// <summary>
        /// Obtiene los nombres de los niños inscritos en una actividad programada para un día específico.
        /// </summary>
        /// <param name="dia">Fecha del día para el cual se desea obtener la lista de niños inscritos.</param>
        /// <returns>Lista de nombres de niños inscritos.</returns>
        public List<string> ObtenerNinosPorDia(DateTime dia)
        {
            var inscripcionesCollection = _database.GetCollection<BsonDocument>("inscripciones");
            var ninosCollection = _database.GetCollection<BsonDocument>("ninos");

            var filtro = Builders<BsonDocument>.Filter.Eq("fecha", dia.Date);
            var inscripciones = inscripcionesCollection.Find(filtro).ToList();
            var nombresNinos = new List<string>();

            foreach (var inscripcion in inscripciones)
            {
                var idNino = inscripcion.GetValue("id_nino").AsObjectId;
                var filtroNino = Builders<BsonDocument>.Filter.Eq("_id", idNino);
                var nino = ninosCollection.Find(filtroNino).FirstOrDefault();

                if (nino != null)
                    nombresNinos.Add($"{nino.GetValue("nombre").AsString} {nino.GetValue("apellidos").AsString}");
            }

            return nombresNinos;
        }

        /// <summary>
        /// Obtiene los materiales necesarios para la actividad programada en el día especificado.
        /// </summary>
        /// <param name="dia">Fecha del día para el cual se desea obtener los materiales.</param>
        /// <returns>Lista de materiales necesarios para la actividad.</returns>
        public List<string> ObtenerMaterialesDeHoy(DateTime dia)
        {
            var actividadesCollection = _database.GetCollection<BsonDocument>("actividades");
            var filtro = Builders<BsonDocument>.Filter.Eq("fecha", dia.Date);
            var actividad = actividadesCollection.Find(filtro).FirstOrDefault();

            if (actividad != null && actividad.Contains("materiales"))
            {
                var materiales = actividad.GetValue("materiales").AsBsonArray;
                var listaMateriales = new List<string>();

                foreach (var material in materiales)
                    listaMateriales.Add(material.AsString);

                return listaMateriales;
            }

            return new List<string> { "No hay materiales para esta actividad." };
        }

        public string ObtenerNinoPorFecha(DateTime fecha)
        {
            var inscripcionesCollection = _database.GetCollection<BsonDocument>("Inscripciones");

            // Buscar en la base de datos el niño inscrito en esa fecha
            var filtro = Builders<BsonDocument>.Filter.Eq("fecha", fecha);
            var resultado = inscripcionesCollection.Find(filtro).FirstOrDefault();

            Nino n = CargarDatosNino(((ObjectId)resultado));

            // Si encuentra un resultado, devolver el nombre del niño
            return n != null ? n.Nombre + " " + n.Apellidos : "";
        }

        /// <summary>
        /// Verifica las credenciales del usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Nombre de usuario a verificar.</param>
        /// <param name="contraseña">Contraseña asociada al usuario.</param>
        /// <returns>
        /// Una tupla con tres valores:
        /// - <c>true</c> si las credenciales son válidas, de lo contrario <c>false</c>.
        /// - El ID del usuario como cadena.
        /// - El rol del usuario.
        /// </returns>
        public (bool, string, string) VerificarAccesoConRol(string usuario, string contraseña)
        {
            try
            {
                // Obtener la colección "Usuarios"
                IMongoCollection<BsonDocument> usuariosCollection = _database.GetCollection<BsonDocument>("Usuarios");

                // Crear el filtro por nombre de usuario
                FilterDefinition<BsonDocument> filtroNombre = Builders<BsonDocument>.Filter.Eq("Nombre", usuario);

                // Mostrar el filtro generado (solo para depuración)
                Console.WriteLine($"Filtro generado (Nombre): {filtroNombre.ToJson()}");

                // Buscar el documento por nombre
                var resultado = usuariosCollection.Find(filtroNombre).FirstOrDefault();

                // Verificar si se encontró un usuario con ese nombre
                if (resultado != null)
                {
                    // Obtener la contraseña almacenada (encriptada)
                    string contrasenaAlmacenada = resultado.Contains("Contrasena") ? resultado.GetValue("Contrasena").AsString : null;

                    // Encriptar la contraseña ingresada para compararla con la almacenada
                    string contrasenaIngresadaEncriptada = EncriptarSHA256(contraseña);

                    if (contrasenaAlmacenada == contrasenaIngresadaEncriptada)
                    {
                        // Obtener el ID y el rol
                        string idUsuario = resultado.GetValue("_id").ToString();
                        string rol = resultado.Contains("Rol") ? resultado.GetValue("Rol").AsString : "Desconocido";

                        // Retornar credenciales válidas
                        return (true, idUsuario, rol);
                    }
                }

                // Retornar credenciales inválidas si el usuario o la contraseña no coinciden
                return (false, null, null);
            }
            catch (Exception ex)
            {
                // Manejar errores
                Console.WriteLine("Error en VerificarAccesoConRol: " + ex.Message);
                return (false, null, null);
            }
        }

        // Método para encriptar la contraseña con SHA-256
        private string EncriptarSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Carga los datos de un niño por su ID.
        /// </summary>
        /// <param name="idNino">ID del niño en formato <see cref="ObjectId"/>.</param>
        /// <returns>Un objeto <see cref="Nino"/> que contiene los datos del niño, o null si no se encuentra.</returns>
        public Nino CargarDatosNino(ObjectId idNino)
        {
            var ninosCollection = _database.GetCollection<Nino>("ninos");
            var filtro = Builders<Nino>.Filter.Eq(n => n.Id, idNino);
            return ninosCollection.Find(filtro).FirstOrDefault();
        }

        /// <summary>
        /// Carga los datos de los niños relacionados con un padre.
        /// </summary>
        /// <param name="idPadre">ID del padre en formato <see cref="ObjectId"/>.</param>
        /// <returns>Lista de objetos <see cref="Nino"/> relacionados con el padre.</returns>
        public List<Nino> CargarDatosNinoPorPadre(ObjectId idPadre)
        {
            var ninosCollection = _database.GetCollection<Nino>("Ninos");
            var filtro = Builders<Nino>.Filter.Eq(n => n.IdPadre, idPadre);
            return ninosCollection.Find(filtro).ToList();
        }

        public bool VerificarInscripcion(ObjectId idNino, ObjectId idActividad)
        {
            var db = ConBD2.ObtenerConexionActiva(); 
            var inscripciones = db.GetCollection<BsonDocument>("Inscripciones");

            var filtro = Builders<BsonDocument>.Filter.Eq("id_nino", idNino) &
                         Builders<BsonDocument>.Filter.Eq("id_actividad", idActividad);

            var resultado = inscripciones.Find(filtro).FirstOrDefault();

            return resultado != null;
        }


        /// <summary>
        /// Obtiene los materiales requeridos para una actividad específica por su ID.
        /// </summary>
        /// <param name="idActividad">ID de la actividad en formato <see cref="ObjectId"/>.</param>
        /// <returns>Lista de materiales necesarios para la actividad.</returns>
        public Usuario ObtenerUsuarioPorId(ObjectId idUsuario)
        {
            var usuariosCollection = _database.GetCollection<Usuario>("Usuarios");
            var filtro = Builders<Usuario>.Filter.Eq(u => u.Id, idUsuario);
            return usuariosCollection.Find(filtro).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="idUsuario">El ID del usuario como un <see cref="ObjectId"/>.</param>
        /// <returns>
        /// Un objeto <see cref="UsuarioMonitor"/> que representa al usuario, 
        /// o <c>null</c> si no se encuentra el usuario.
        /// </returns>
        /// <exception cref="Exception">Se lanza si ocurre un error durante el proceso.</exception>
        public UsuarioMonitor ObtenerUsuarioPorIdMoni(ObjectId idUsuario)
        {
            try
            {
                // Obtener la colección "Usuarios" como BsonDocument
                var usuariosCollection = _database.GetCollection<BsonDocument>("Usuarios");

                // Crear el filtro para buscar el documento
                var filtro = Builders<BsonDocument>.Filter.Eq("_id", idUsuario);

                // Buscar el documento
                var usuarioBson = usuariosCollection.Find(filtro).FirstOrDefault();

                if (usuarioBson == null)
                {
                    Console.WriteLine($"No se encontró usuario con ID: {idUsuario}");
                    return null;
                }

                // Inspeccionar el documento para verificar su estructura
                Console.WriteLine($"Documento encontrado: {usuarioBson.ToJson()}");

                // Mapear manualmente los campos al objeto UsuarioMonitor
                var fechaRegistro = usuarioBson.Contains("FechaRegistro") && usuarioBson["FechaRegistro"].IsValidDateTime
                    ? usuarioBson.GetValue("FechaRegistro").ToUniversalTime()
                    : DateTime.MinValue; // O cualquier valor predeterminado en caso de error.

                var usuario = new UsuarioMonitor(
                    usuarioBson.GetValue("Nombre").AsString,
                    usuarioBson.GetValue("Apellidos").AsString,
                    usuarioBson.GetValue("DNI").AsString,
                    usuarioBson.GetValue("Correo").AsString,
                    usuarioBson.GetValue("Contrasena").AsString,
                    usuarioBson.GetValue("Rol").AsString,
                    fechaRegistro,
                    usuarioBson.GetValue("Telefono").AsString,
                    usuarioBson.GetValue("Direccion").AsString
                );


                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuario: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los niños inscritos en una actividad por su ID.
        /// </summary>
        /// <param name="idActividad">El ID de la actividad como un <see cref="ObjectId"/>.</param>
        /// <returns>
        /// Una lista de objetos <see cref="Nino"/> que representan a los niños inscritos en la actividad.
        /// </returns>
        /// <exception cref="Exception">Se lanza si ocurre un error durante el proceso.</exception>
        public List<Nino> ObtenerNinosPorActividad(ObjectId idActividad)
        {
            try
            {
                Console.WriteLine($"Buscando inscripciones para la actividad con ID: {idActividad}");

                // Obtener la colección de inscripciones
                var inscripcionesCollection = _database.GetCollection<Inscripcion>("Inscripciones");

                // Aplicar el filtro usando el nombre correcto del campo
                var filtroInscripciones = Builders<Inscripcion>.Filter.Eq(inscripcion => inscripcion.IdActividad, idActividad);
                var inscripciones = inscripcionesCollection.Find(filtroInscripciones).ToList();

                Console.WriteLine($"Número de inscripciones encontradas: {inscripciones.Count}");
                foreach (var inscripcion in inscripciones)
                {
                    Console.WriteLine($"Inscripción encontrada - IdActividad: {inscripcion.IdActividad}, IdNino: {inscripcion.IdNino}");
                }

                // Obtener los IDs únicos de niños de las inscripciones
                var idsNinos = inscripciones.Select(inscripcion => inscripcion.IdNino).Distinct().ToList();
                Console.WriteLine($"Número de IDs de niños únicos: {idsNinos.Count}");

                // Obtener la colección de niños
                var ninosCollection = _database.GetCollection<Nino>("Ninos");
                var filtroNinos = Builders<Nino>.Filter.In(nino => nino.Id, idsNinos);

                var ninos = ninosCollection.Find(filtroNinos).ToList();
                Console.WriteLine($"Número de niños encontrados: {ninos.Count}");

                return ninos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Nino>();
            }
        }


        /// <summary>
        /// Obtiene los materiales requeridos para una actividad por su ID.
        /// </summary>
        /// <param name="idActividad">El ID de la actividad como un <see cref="ObjectId"/>.</param>
        /// <returns>
        /// Una lista de cadenas (<see cref="List{String}"/>) que representan los materiales requeridos 
        /// para la actividad. Devuelve una lista vacía si no se encuentran materiales.
        /// </returns>
        public List<string> ObtenerMaterialesPorActividad(ObjectId idActividad)
        {
            // Obtener la colección "Actividades"
            var actividadesCollection = _database.GetCollection<BsonDocument>("Actividades");

            // Crear el filtro para encontrar la actividad por su ID
            var filtroActividad = Builders<BsonDocument>.Filter.Eq("_id", idActividad);

            // Buscar la actividad en la base de datos
            var actividad = actividadesCollection.Find(filtroActividad).FirstOrDefault();

            // Verificar si se encontró la actividad y si contiene la propiedad "Materiales"
            if (actividad != null && actividad.Contains("Materiales"))
            {
                // Obtener los materiales como un array BSON y convertirlo a List<string>
                var materialesBsonArray = actividad["Materiales"].AsBsonArray;
                return materialesBsonArray.Select(m => m.AsString).ToList();
            }

            // Retornar una lista vacía si no se encuentra la actividad o no tiene materiales
            return new List<string>();
        }

    }
}
