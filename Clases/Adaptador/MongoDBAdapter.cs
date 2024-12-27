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
            var actividadesCollection = _database.GetCollection<BsonDocument>("actividades");
            var filtro = Builders<BsonDocument>.Filter.Eq("fecha", dia.Date);
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
            var usuariosCollection = _database.GetCollection<BsonDocument>("usuarios");
            var filtro = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("usuario", usuario),
                Builders<BsonDocument>.Filter.Eq("contraseña", contraseña)
            );

            var resultado = usuariosCollection.Find(filtro).FirstOrDefault();

            if (resultado != null)
            {
                string idUsuario = resultado.GetValue("_id").ToString();
                string rol = resultado.Contains("rol") ? resultado.GetValue("rol").AsString : "Desconocido";
                return (true, idUsuario, rol);
            }

            return (false, null, null);
        }

        /// <summary>
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
            var ninosCollection = _database.GetCollection<Nino>("ninos");
            var filtro = Builders<Nino>.Filter.Eq(n => n.IdPadre, idPadre);
            return ninosCollection.Find(filtro).ToList();
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        public Usuario ObtenerUsuarioPorId(ObjectId idUsuario)
        {
            var usuariosCollection = _database.GetCollection<Usuario>("usuarios");
            var filtro = Builders<Usuario>.Filter.Eq(u => u.Id, idUsuario);
            return usuariosCollection.Find(filtro).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene los niños inscritos en una actividad por su ID.
        /// </summary>
        public List<Nino> ObtenerNinosPorActividad(ObjectId idActividad)
        {
            var inscripcionesCollection = _database.GetCollection<Inscripcion>("inscripciones");
            var filtroInscripciones = Builders<Inscripcion>.Filter.Eq(inscripcion => inscripcion.IdActividad, idActividad);
            var inscripciones = inscripcionesCollection.Find(filtroInscripciones).ToList();

            var idsNinos = inscripciones.Select(inscripcion => inscripcion.IdNino).Distinct().ToList();
            var ninosCollection = _database.GetCollection<Nino>("ninos");
            var filtroNinos = Builders<Nino>.Filter.In(nino => nino.Id, idsNinos);

            return ninosCollection.Find(filtroNinos).ToList();
        }

        /// <summary>
        /// Obtiene los materiales requeridos por una actividad.
        /// </summary>
        public List<string> ObtenerMaterialesPorActividad(ObjectId idActividad)
        {
            var actividadesCollection = _database.GetCollection<Actividades>("actividades");
            var filtroActividad = Builders<Actividades>.Filter.Eq(a => a.Id, idActividad);
            var actividad = actividadesCollection.Find(filtroActividad).FirstOrDefault();

            return actividad?.Materiales ?? new List<string>();
        }
    }
}
