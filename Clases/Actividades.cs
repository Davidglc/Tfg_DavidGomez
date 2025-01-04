using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace TFG_DavidGomez.Clases
{
    public class Actividades
    {
        /// <summary>
        /// Identificador único de la actividad.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Nombre de la actividad.
        /// </summary>
        [BsonElement("Nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción de la actividad.
        /// </summary>
        [BsonElement("Descripcion")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha y hora en la que se llevará a cabo la actividad.
        /// </summary>
        [BsonElement("FechaHora")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FechaHora { get; set; }

        /// <summary>
        /// Duración de la actividad en minutos.
        /// </summary>
        [BsonElement("Duracion")]
        public int Duracion { get; set; }

        /// <summary>
        /// Identificador del monitor encargado de la actividad.
        /// </summary>
        [BsonElement("IdMonitor")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId IdMonitor { get; set; }

        /// <summary>
        /// Lista de materiales necesarios para la actividad.
        /// </summary>
        [BsonElement("Materiales")]
        public List<string> Materiales { get; set; }

        /// <summary>
        /// Constructor de la clase Actividades.
        /// </summary>
        public Actividades(
            string nombre,
            string descripcion,
            DateTime fechaHora,
            int duracion,
            int plazasMaximas,
            int plazasOcupadas,
            List<string> materiales)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            FechaHora = fechaHora;
            Duracion = duracion;
            Materiales = materiales;
        }

        /// <summary>
        /// Representación en cadena de la actividad.
        /// </summary>
        public override string ToString()
        {
            return $"Actividad: {Nombre}, Descripcion: {Descripcion}, FechaHora: {FechaHora}, " +
                   $"Duracion: {Duracion} min, IdMonitor: {IdMonitor}";
        }

        /// <summary>
        /// Verifica si dos objetos de tipo Actividades son iguales.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Actividades actividad && Id.Equals(actividad.Id);
        }

        /// <summary>
        /// Genera un código hash para la actividad.
        /// </summary>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
