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

        public ObjectId IdActividad { get; set; } // ID de la actividad
        public ObjectId IdNino { get; set; }      // ID del niño inscrito

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Fecha { get; set; }      // Fecha de la inscripción

        public Inscripcion(ObjectId id, ObjectId idActividad, ObjectId idNino, DateTime fecha)
        {
            Id = id;
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
                   IdNino.Equals(inscripcion.IdNino) &&
                   Fecha == inscripcion.Fecha;
        }


    }

}
