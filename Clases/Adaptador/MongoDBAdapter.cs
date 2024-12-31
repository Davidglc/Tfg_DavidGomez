using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TFG_DavidGomez.Clases.Conexion;

namespace TFG_DavidGomez.Clases.Adaptador
{
    internal class MongoDBAdapter
    {
        private readonly IMongoDatabase _database;

        public MongoDBAdapter()
        {
            // Usar la conexión existente de la clase ConexionBDAtlas
            _database = ConBD2.ObtenerConexionActiva();
        }

        /// <summary>
        /// Obtiene la actividad programada para un día específico.
        /// </summary>
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
        /// Obtiene los niños inscritos en una actividad programada para un día específico.
        /// </summary>
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
        /// Obtiene los materiales necesarios para la actividad programada hoy.
        /// </summary>
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

        /// <summary>
        /// Verifica las credenciales del usuario en la base de datos.
        /// </summary>
        public (bool, string, string) VerificarAccesoConRol(string usuario, string contraseña)
        {
            try
            {
                // Obtener la colección "Usuarios"
                IMongoCollection<BsonDocument> usuariosCollection = _database.GetCollection<BsonDocument>("Usuarios");

                // Crear el filtro por nombre de usuario
                FilterDefinition<BsonDocument> filtroNombre = Builders<BsonDocument>.Filter.Eq("Nombre", usuario);

                // Mostrar el filtro generado
                Console.WriteLine($"Filtro generado (Nombre): {filtroNombre.ToJson()}");

                // Buscar el documento por nombre
                var resultado = usuariosCollection.Find(filtroNombre).FirstOrDefault();

                // Verificar si se encontró un usuario con ese nombre
                if (resultado != null)
                {
                    // Validar la contraseña
                    string contrasenaAlmacenada = resultado.Contains("Contrasena") ? resultado.GetValue("Contrasena").AsString : null;

                    if (contrasenaAlmacenada == contraseña)
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

        /// <summary>s
        /// Carga los datos de un niño por su ID.
        /// </summary>
        public Nino CargarDatosNino(ObjectId idNino)
        {
            var ninosCollection = _database.GetCollection<Nino>("ninos");
            var filtro = Builders<Nino>.Filter.Eq(n => n.Id, idNino);
            return ninosCollection.Find(filtro).FirstOrDefault();
        }

        /// <summary>
        /// Carga los datos de los niños relacionados con un padre.
        /// </summary>
        public List<Nino> CargarDatosNinoPorPadre(ObjectId idPadre)
        {
            var ninosCollection = _database.GetCollection<Nino>("Ninos");
            var filtro = Builders<Nino>.Filter.Eq(n => n.IdPadre, idPadre);
            return ninosCollection.Find(filtro).ToList();
        }


        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        public Usuario ObtenerUsuarioPorId(ObjectId idUsuario)
        {
            var usuariosCollection = _database.GetCollection<Usuario>("Usuarios");
            var filtro = Builders<Usuario>.Filter.Eq(u => u.Id, idUsuario);
            return usuariosCollection.Find(filtro).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene los niños inscritos en una actividad por su ID.
        /// </summary>
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
        /// Obtiene los materiales requeridos por una actividad.
        /// </summary>
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
