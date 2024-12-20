    using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG_DavidGomez.Clases
{
    public class Nino
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public ObjectId IdPadre { get; set; }
        public ObjectId[] Actividades { get; set; }

        public Nino(string nombre, string apellidos, DateTime FechaNacimiento, int edad)
        {
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.FechaNacimiento = FechaNacimiento;
            this.Edad = edad;
            
        }

        public override bool Equals(object obj)
        {
            return obj is Nino nino && Id.Equals(nino.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
