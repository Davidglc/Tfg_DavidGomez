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
    public class UsuarioMonitor
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string DNI { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FechaRegistro { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }


        public UsuarioMonitor(string nombre, string apellidos, string dni, string correo, string contrasena, string rol, DateTime fechaRegistro, string telefono,string direccion)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dni;
            Correo = correo;
            Contrasena = contrasena;
            Rol = rol;
            FechaRegistro = fechaRegistro;
            Telefono = telefono;
            Direccion = direccion;
        }


        public override string ToString()
        {
             return $"Nombre: {Nombre}, Apellidos: {Apellidos}, DNI: {DNI}, Correo: {Correo}, Contraseña: {Contrasena}, Rol: {Rol}, Fecha de Registro: {FechaRegistro.ToShortDateString()}, Teléfono: {Telefono}, Dirección: {Direccion}";
        }

        public override bool Equals(object obj)
        {
            return obj is UsuarioMonitor usuario && Id.Equals(usuario.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
