using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TFG_DavidGomez.Clases
{
    public class Usuario
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FechaRegistro { get; set; }

        public Usuario(string nombre, string apellidos, string dni, string email, string contrasena, string rol, DateTime fechaRegistro)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Dni = dni;
            Email = email;
            Contrasena = contrasena;
            Rol = rol;
            FechaRegistro = fechaRegistro;
        }

        public override string ToString()
        {
            return $"Usuario: {Nombre} {Apellidos}, DNI: {Dni}, Email: {Email}, Rol: {Rol}, FechaRegistro: {FechaRegistro}";
        }

        public override bool Equals(object obj)
        {
            return obj is Usuario usuario && Id.Equals(usuario.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
