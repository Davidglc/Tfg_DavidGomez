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
        public (bool, string, string) VerificarAccesoConRol(string usuario, string contraseña)
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
                // Obtener el ID y el rol del usuario
                string idUsuario = resultado.GetValue("_id").ToString(); // ID del usuario
                string rol = resultado.Contains("rol") ? resultado.GetValue("rol").AsString : "Desconocido";

                return (true, idUsuario, rol); // Credenciales válidas, ID y rol encontrados
            }

            return (false, null, null); // Credenciales no válidas
        }

        public Nino CargarDatosNino(ObjectId idNino)
        {
            try
            {
                // Obtener la colección de niños desde la base de datos
                var ninosCollection = ConexionBD.GetCollection<Nino>("ninos");

                // Crear un filtro para buscar por ID
                var filtro = Builders<Nino>.Filter.Eq(n => n.Id, idNino);

                // Buscar el documento del niño
                var nino = ninosCollection.Find(filtro).FirstOrDefault();

                // Verificar si se encontró el niño
                if (nino == null)
                {
                    MessageBox.Show("No se encontró ningún niño con el ID proporcionado.", "Niño no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                return nino;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("El formato del ID del niño no es válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos del niño: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<Nino> CargarDatosNinoPorPadre(ObjectId idPadre)
        {
            try
            {
                // Obtener la colección de niños desde la base de datos
                var ninosCollection = ConexionBD.GetCollection<Nino>("ninos");

                // Crear un filtro para buscar niños relacionados con el ID del padre
                var filtro = Builders<Nino>.Filter.Eq(n => n.IdPadre, idPadre);

                // Buscar todos los niños que coincidan con el filtro
                var ninos = ninosCollection.Find(filtro).ToList();

                // Validar si no se encontraron niños
                if (ninos == null || ninos.Count == 0)
                {
                    MessageBox.Show("No se encontraron niños asociados con este padre.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return new List<Nino>();
                }

                return ninos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos de los niños: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Nino>();
            }
        }

        public Usuario ObtenerUsuarioPorId(ObjectId idUsuario)
        {
            try
            {
                // Obtener la colección de usuarios desde la base de datos
                var usuariosCollection = ConexionBD.GetCollection<Usuario>("usuarios");

                // Crear un filtro para buscar el usuario por su ID
                var filtro = Builders<Usuario>.Filter.Eq(u => u.Id, idUsuario);

                // Buscar el usuario que coincida con el filtro
                var usuario = usuariosCollection.Find(filtro).FirstOrDefault();

                // Validar si no se encontró el usuario
                if (usuario == null)
                {
                    MessageBox.Show("No se encontró un usuario con este ID.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                return usuario;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al obtener el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public List<Nino> ObtenerNinosPorActividad(ObjectId idActividad)
        {
            try
            {
                // Obtener la colección de inscripciones
                var inscripcionesCollection = ConexionBD.GetCollection<Inscripcion>("inscripciones");

                // Crear un filtro para obtener las inscripciones relacionadas con la actividad
                var filtroInscripciones = Builders<Inscripcion>.Filter.Eq(inscripcion => inscripcion.IdActividad, idActividad);
                var inscripciones = inscripcionesCollection.Find(filtroInscripciones).ToList();

                if (inscripciones == null || !inscripciones.Any())
                {
                    return new List<Nino>(); // No hay inscripciones para esta actividad
                }

                // Extraer los IDs de los niños desde las inscripciones
                var idsNinos = inscripciones.Select(inscripcion => inscripcion.IdNino).Distinct().ToList();

                if (!idsNinos.Any())
                {
                    return new List<Nino>(); // No hay niños asociados a las inscripciones
                }

                // Obtener la colección de niños
                var ninosCollection = ConexionBD.GetCollection<Nino>("ninos");

                // Crear un filtro para obtener los niños por sus IDs
                var filtroNinos = Builders<Nino>.Filter.In(nino => nino.Id, idsNinos);

                // Buscar los niños en la base de datos
                var ninos = ninosCollection.Find(filtroNinos).ToList();

                return ninos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los niños inscritos en la actividad: {ex.Message}");
            }
        }

        public List<string> ObtenerMaterialesPorActividad(ObjectId idActividad)
        {
            try
            {
                // Obtener la colección de actividades
                var actividadesCollection = ConexionBD.GetCollection<Actividades>("actividades");

                // Crear un filtro para buscar la actividad con el ID especificado
                var filtroActividad = Builders<Actividades>.Filter.Eq(a => a.Id, idActividad);

                // Buscar la actividad en la base de datos
                var actividad = actividadesCollection.Find(filtroActividad).FirstOrDefault();

                if (actividad == null)
                {
                    throw new Exception("No se encontró la actividad especificada.");
                }

                // Devolver la lista de materiales asociados con la actividad
                return actividad.Materiales ?? new List<string>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los materiales de la actividad: {ex.Message}");
            }
        }

    }
}
