using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TFG_DavidGomez.Clases.Conexion.TFG_DavidGomez;

namespace TFG_DavidGomez.Clases.Adaptador
{
    internal class MongoDBAdapter
    {
        private readonly IMongoDatabase _database;

        public MongoDBAdapter()
        {
            // Usar la conexión existente de la clase ConexionBD
            _database = ConexionBD.ObtenerConexionActiva();
        }

        /// <summary>
        /// Obtiene la actividad programada para un día específico.
        /// </summary>
        /// <param name="dia">Fecha de la actividad.</param>
        /// <returns>Actividad programada para la fecha.</returns>
        public BsonDocument ObtenerActividadPorDia(DateTime dia)
        {
            var actividadesCollection = ConexionBD.GetCollection<BsonDocument>("actividades");

            // Filtrar por fecha específica
            var filtro = Builders<BsonDocument>.Filter.Eq("fecha", dia.Date);

            // Buscar la actividad del día
            var actividad = actividadesCollection.Find(filtro).FirstOrDefault();

            return actividad;
        }

        /// <summary>
        /// Obtiene los niños inscritos en una actividad programada para un día específico.
        /// </summary>
        /// <param name="dia">Fecha de la actividad.</param>
        /// <returns>Lista de nombres de niños inscritos.</returns>
        public List<string> ObtenerNinosPorDia(DateTime dia)
        {
            var inscripcionesCollection = ConexionBD.GetCollection<BsonDocument>("inscripciones");
            var ninosCollection = ConexionBD.GetCollection<BsonDocument>("ninos");

            // Filtrar por la fecha de la actividad
            var filtro = Builders<BsonDocument>.Filter.Eq("fecha", dia.Date);

            // Obtener las inscripciones del día
            var inscripciones = inscripcionesCollection.Find(filtro).ToList();

            var nombresNinos = new List<string>();

            // Obtener información de cada niño a partir de las inscripciones
            foreach (var inscripcion in inscripciones)
            {
                var idNino = inscripcion.GetValue("id_nino").AsObjectId;

                // Buscar al niño en la colección de niños
                var filtroNino = Builders<BsonDocument>.Filter.Eq("_id", idNino);
                var nino = ninosCollection.Find(filtroNino).FirstOrDefault();

                if (nino != null)
                {
                    nombresNinos.Add(nino.GetValue("nombre").AsString + " " + nino.GetValue("apellidos").AsString);
                }
            }

            return nombresNinos;
        }

        /// <summary>
        /// Obtiene los materiales necesarios para la actividad programada hoy.
        /// </summary>
        /// <param name="dia">Fecha de la actividad.</param>
        /// <returns>Lista de materiales requeridos.</returns>
        public List<string> ObtenerMaterialesDeHoy(DateTime dia)
        {
            var actividadesCollection = ConexionBD.GetCollection<BsonDocument>("actividades");

            // Filtrar por la fecha de la actividad
            var filtro = Builders<BsonDocument>.Filter.Eq("fecha", dia.Date);

            // Buscar la actividad del día
            var actividad = actividadesCollection.Find(filtro).FirstOrDefault();

            if (actividad != null && actividad.Contains("materiales"))
            {
                var materiales = actividad.GetValue("materiales").AsBsonArray;

                // Convertir los materiales a una lista de strings
                var listaMateriales = new List<string>();
                foreach (var material in materiales)
                {
                    listaMateriales.Add(material.AsString);
                }

                return listaMateriales;
            }

            return new List<string> { "No hay materiales para esta actividad." };
        }

        /// <summary>
        /// Verifica las credenciales del usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Nombre de usuario.</param>
        /// <param name="contraseña">Contraseña del usuario.</param>
        /// <returns>True si las credenciales son válidas, false de lo contrario.</returns>
        public (bool, string) VerificarAccesoConRol(string usuario, string contraseña)
        {
            var usuariosCollection = ConexionBD.GetCollection<BsonDocument>("usuarios");

            // Crear el filtro para buscar un usuario que coincida con el nombre de usuario y la contraseña
            var filtro = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("usuario", usuario),
                Builders<BsonDocument>.Filter.Eq("contraseña", contraseña)
            );

            // Buscar el usuario en la colección
            var resultado = usuariosCollection.Find(filtro).FirstOrDefault();

            // Verificar si el usuario existe
            if (resultado != null)
            {
                // Obtener el rol del usuario (asumimos que el campo en la base de datos se llama 'rol')
                string rol = resultado.Contains("rol") ? resultado.GetValue("rol").AsString : "Desconocido";

                return (true, rol); // Credenciales válidas y rol encontrado
            }

            return (false, null); // Credenciales no válidas
        }
    }
}
