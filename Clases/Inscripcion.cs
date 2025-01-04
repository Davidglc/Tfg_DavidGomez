using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace TFG_DavidGomez.Clases
{
    /// <summary>
    /// Representa una inscripción de un niño a una actividad.
    /// </summary>
    public class Inscripcion
    {
        /// <summary>
        /// Identificador único de la inscripción.
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; } // ID único de la inscripción

        /// <summary>
        /// Identificador de la actividad a la que se inscribe el niño.
        /// </summary>
        [BsonElement("id_actividad")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId IdActividad { get; set; } // ID de la actividad

        /// <summary>
        /// Identificador del padre que realiza la inscripción.
        /// </summary>
        [BsonElement("id_padre")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId IdPadre { get; set; } // ID del padre

        /// <summary>
        /// Identificador del niño inscrito en la actividad.
        /// </summary>
        [BsonElement("id_nino")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId IdNino { get; set; } // ID del niño inscrito

        /// <summary>
        /// Fecha en la que se realizó la inscripción.
        /// </summary>
        [BsonElement("fecha")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Fecha { get; set; } // Fecha de la inscripción

        /// <summary>
        /// Constructor de la clase Inscripcion con parámetros.
        /// </summary>
        /// <param name="id">ID único de la inscripción.</param>
        /// <param name="idPadre">ID del padre que inscribe.</param>
        /// <param name="idActividad">ID de la actividad a la que se inscribe.</param>
        /// <param name="idNino">ID del niño inscrito.</param>
        /// <param name="fecha">Fecha de la inscripción.</param>
        public Inscripcion(ObjectId id, ObjectId idPadre, ObjectId idActividad, ObjectId idNino, DateTime fecha)
        {
            Id = id;
            IdPadre = idPadre;
            IdActividad = idActividad;
            IdNino = idNino;
            Fecha = fecha;
        }

        /// <summary>
        /// Constructor vacío para inicialización sin parámetros.
        /// </summary>
        public Inscripcion()
        {
        }

        /// <summary>
        /// Verifica si dos inscripciones son iguales.
        /// </summary>
        /// <param name="obj">Objeto a comparar.</param>
        /// <returns>True si los objetos son iguales, de lo contrario, False.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Inscripcion inscripcion &&
                   Id.Equals(inscripcion.Id) &&
                   IdActividad.Equals(inscripcion.IdActividad) &&
                   IdPadre.Equals(inscripcion.IdPadre) &&
                   IdNino.Equals(inscripcion.IdNino) &&
                   Fecha == inscripcion.Fecha;
        }

        /// <summary>
        /// Genera un código hash único para la inscripción.
        /// </summary>
        /// <returns>Código hash único.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, IdActividad, IdPadre, IdNino, Fecha);
        }
    }
}
