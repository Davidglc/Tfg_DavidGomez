using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TFG_DavidGomez.Clases
{
    /// <summary>
    /// Representa un usuario en el sistema, ya sea un padre o algún otro rol.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario en la base de datos.
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        [BsonElement("Nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        [BsonElement("Apellidos")]
        public string Apellidos { get; set; }

        /// <summary>
        /// DNI del usuario, utilizado para su identificación.
        /// </summary>
        [BsonElement("DNI")]
        public string DNI { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        [BsonElement("Correo")]
        public string Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario (en texto plano o encriptada).
        /// </summary>
        [BsonElement("Contrasena")]
        public string Contrasena { get; set; }

        /// <summary>
        /// Rol del usuario (Ej. "Padre", "Admin", etc.).
        /// </summary>
        [BsonElement("Rol")]
        public string Rol { get; set; }

        /// <summary>
        /// Fecha en la que se registró el usuario en el sistema.
        /// </summary>
        [BsonElement("FechaRegistro")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Teléfono del usuario.
        /// </summary>
        [BsonElement("Telefono")]
        public string Telefono { get; set; }

        /// <summary>
        /// Dirección del usuario.
        /// </summary>
        [BsonElement("Direccion")]
        public string Direccion { get; set; }

        /// <summary>
        /// Lista de hijos asociados al usuario. Este campo se ignora si es null.
        /// </summary>
        [BsonIgnoreIfNull]
        [BsonElement("Hijos")]
        public List<Nino> Hijos { get; set; }

        /// <summary>
        /// Constructor para crear un nuevo usuario con el rol "Padre".
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="apellidos">Apellidos del usuario.</param>
        /// <param name="dni">DNI del usuario.</param>
        /// <param name="correo">Correo electrónico del usuario.</param>
        /// <param name="contrasena">Contraseña del usuario.</param>
        /// <param name="fechaRegistro">Fecha de registro del usuario.</param>
        /// <param name="telefono">Teléfono del usuario.</param>
        /// <param name="direccion">Dirección del usuario.</param>
        public Usuario(string nombre, string apellidos, string dni, string correo, string contrasena, DateTime fechaRegistro, string telefono, string direccion)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dni;
            Correo = correo;
            Contrasena = contrasena;
            Rol = "Padre";  // Por defecto, el rol es "Padre"
            FechaRegistro = fechaRegistro;
            Telefono = telefono;
            Hijos = new List<Nino>();
            Direccion = direccion;
        }

        /// <summary>
        /// Constructor general para crear un nuevo usuario con el rol "Monitor".
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="apellidos">Apellidos del usuario.</param>
        /// <param name="dni">DNI del usuario.</param>
        /// <param name="correo">Correo electrónico del usuario.</param>
        /// <param name="contrasena">Contraseña del usuario.</param>
        /// <param name="rol">Rol del usuario.</param>
        /// <param name="fechaRegistro">Fecha de registro del usuario.</param>
        /// <param name="telefono">Teléfono del usuario.</param>
        /// <param name="direccion">Dirección del usuario.</param>
        public Usuario(string nombre, string apellidos, string dni, string correo, string contrasena, string rol, DateTime fechaRegistro, string telefono, string direccion)
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
            Hijos = new List<Nino>();
        }

        /// <summary>
        /// Genera una representación en cadena del objeto Usuario.
        /// </summary>
        /// <returns>Cadena con la información del usuario.</returns>
        public override string ToString()
        {
            if (Rol == "Padre")
            {
                return $"Nombre: {Nombre}, Apellidos: {Apellidos}, DNI: {DNI}, Correo: {Correo}, Fecha de Registro: {FechaRegistro.ToShortDateString()}, Teléfono: {Telefono}, Dirección: {Direccion}, Rol: {Rol}";
            }
            else
            {
                return $"Nombre: {Nombre}, Apellidos: {Apellidos}, DNI: {DNI}, Correo: {Correo}, Contraseña: {Contrasena}, Rol: {Rol}, Fecha de Registro: {FechaRegistro.ToShortDateString()}, Teléfono: {Telefono}, Dirección: {Direccion}";
            }
        }

        /// <summary>
        /// Compara dos objetos Usuario para verificar si son iguales.
        /// </summary>
        /// <param name="obj">Objeto a comparar.</param>
        /// <returns>True si los objetos son iguales, de lo contrario, False.</returns>
        public override bool Equals(object obj)
        {
            return obj is Usuario usuario && Id.Equals(usuario.Id);
        }

        /// <summary>
        /// Genera un código hash único para el usuario.
        /// </summary>
        /// <returns>Código hash único.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Método para añadir un nuevo niño a la lista de hijos del usuario.
        /// </summary>
        /// <param name="nuevoNino">Nuevo niño a añadir.</param>
        public void AnadirNino(Nino nuevoNino)
        {
            if (nuevoNino == null)
            {
                throw new ArgumentNullException(nameof(nuevoNino), "El niño no puede ser añadido.");
            }

            Hijos.Add(nuevoNino);
        }
    }
}
