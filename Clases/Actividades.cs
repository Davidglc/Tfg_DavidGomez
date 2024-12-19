using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG_DavidGomez.Clases
{
    public class Actividades
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FechaHora { get; set; }

        public int Duracion { get; set; }
        public int PlazasMaximas { get; set; }
        public int PlazasOcupadas { get; set; }
        public ObjectId IdMonitor { get; set; }
        public string[] Materiales { get; set; }

        public Actividades(string nombre, string descripcion, DateTime fechaHora, int duracion, int plazasMaximas, int plazasOcupadas, ObjectId idMonitor, string[] materiales)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            FechaHora = fechaHora;
            Duracion = duracion;
            PlazasMaximas = plazasMaximas;
            PlazasOcupadas = plazasOcupadas;
            IdMonitor = idMonitor;
            Materiales = materiales;
        }

        public override string ToString()
        {
            return $"Actividad: {Nombre}, Descripcion: {Descripcion}, FechaHora: {FechaHora}, Duracion: {Duracion} min, Plazas: {PlazasOcupadas}/{PlazasMaximas}, IdMonitor: {IdMonitor}";
        }

        public override bool Equals(object obj)
        {
            return obj is Actividades actividad && Id.Equals(actividad.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
