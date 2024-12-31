using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG_DavidGomez.Clases
{
    public class Inscripcion
    {
        [BsonId]
        public ObjectId Id { get; set; } // ID único de la inscripción

        [BsonElement("id_actividad")]
        public ObjectId IdActividad { get; set; } // ID de la actividad

        [BsonElement("id_padre")]
        public ObjectId IdPadre { get; set; } // ID del padre

        [BsonElement("id_nino")]
        public ObjectId IdNino { get; set; } // ID del niño inscrito

        [BsonElement("fecha")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Fecha { get; set; } // Fecha de la inscripción

        public Inscripcion(ObjectId id, ObjectId idPadre, ObjectId idActividad, ObjectId idNino, DateTime fecha)
        {
            Id = id;
            IdPadre = idPadre;
            IdActividad = idActividad;
            IdNino = idNino;
            Fecha = fecha;
        }

        public Inscripcion()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Inscripcion inscripcion &&
                   Id.Equals(inscripcion.Id) &&
                   IdActividad.Equals(inscripcion.IdActividad) &&
                   IdPadre.Equals(inscripcion.IdPadre) &&
                   IdNino.Equals(inscripcion.IdNino) &&
                   Fecha == inscripcion.Fecha;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, IdActividad, IdPadre, IdNino, Fecha);
        }
    }


}
