using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG_DavidGomez.Clases
{
    public class Inscripciones
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId IdNino { get; set; }
        public ObjectId IdActividad { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FechaInscripcion { get; set; }

        public string Estado { get; set; }

        public Inscripciones(ObjectId idNino, ObjectId idActividad, DateTime fechaInscripcion, string estado)
        {
            IdNino = idNino;
            IdActividad = idActividad;
            FechaInscripcion = fechaInscripcion;
            Estado = estado;
        }

        public override string ToString()
        {
            return $"Inscripción: Niño {IdNino}, Actividad {IdActividad}, Fecha: {FechaInscripcion}, Estado: {Estado}";
        }

        public override bool Equals(object obj)
        {
            return obj is Inscripciones inscripcion && Id.Equals(inscripcion.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
