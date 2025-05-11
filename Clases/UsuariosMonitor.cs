using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TFG_DavidGomez.Clases
{
    /// <summary>
    /// Representa a un usuario monitor en el sistema.
    /// </summary>
    public class UsuarioMonitor
    {
        /// <summary>
        /// Identificador único del usuario monitor en la base de datos.
        /// </summary>

        public int Id { get; set; }

        /// <summary>
        /// Nombre del usuario monitor.
        /// </summary>

        public string Nombre { get; set; }

        /// <summary>
        /// Apellidos del usuario monitor.
        /// </summary>

        public string Apellidos { get; set; }

        /// <summary>
        /// DNI del usuario monitor.
        /// </summary>

        public string DNI { get; set; }

        /// <summary>
        /// Correo electrónico del usuario monitor.
        /// </summary>

        public string Correo { get; set; }

        /// <summary>
        /// Contraseña del usuario monitor.
        /// </summary>

        public string Contrasena { get; set; }

        /// <summary>
        /// Rol del usuario monitor (Ej. "Monitor").
        /// </summary>

        public string Rol { get; set; }

        /// <summary>
        /// Fecha en la que se registró el usuario monitor en el sistema.
        /// </summary>

        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Teléfono del usuario monitor.
        /// </summary>
 
        public string Telefono { get; set; }

        /// <summary>
        /// Dirección del usuario monitor.
        /// </summary>

        public string Direccion { get; set; }


        /// <summary>
        /// Constructor para crear un nuevo usuario monitor.
        /// </summary>
        /// <param name="nombre">Nombre del usuario monitor.</param>
        /// <param name="apellidos">Apellidos del usuario monitor.</param>
        /// <param name="dni">DNI del usuario monitor.</param>
        /// <param name="correo">Correo electrónico del usuario monitor.</param>
        /// <param name="contrasena">Contraseña del usuario monitor.</param>
        /// <param name="rol">Rol del usuario monitor.</param>
        /// <param name="fechaRegistro">Fecha de registro del usuario monitor.</param>
        /// <param name="telefono">Teléfono del usuario monitor.</param>
        /// <param name="direccion">Dirección del usuario monitor.</param>
        public UsuarioMonitor(string nombre, string apellidos, string dni, string correo, string contrasena, string rol, DateTime fechaRegistro, string telefono, string direccion)
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

        /// <summary>
        /// Genera una representación en cadena del objeto UsuarioMonitor.
        /// </summary>
        /// <returns>Cadena con la información del usuario monitor.</returns>
        public override string ToString()
        {
            return $"Nombre: {Nombre}, Apellidos: {Apellidos}, DNI: {DNI}, Correo: {Correo}, Contraseña: {Contrasena}, Rol: {Rol}, Fecha de Registro: {FechaRegistro.ToShortDateString()}, Teléfono: {Telefono}, Dirección: {Direccion}";
        }

        /// <summary>
        /// Compara dos objetos UsuarioMonitor para verificar si son iguales.
        /// </summary>
        /// <param name="obj">Objeto a comparar.</param>
        /// <returns>True si los objetos son iguales, de lo contrario, False.</returns>
        public override bool Equals(object obj)
        {
            return obj is UsuarioMonitor usuario && Id.Equals(usuario.Id);
        }

        /// <summary>
        /// Genera un código hash único para el usuario monitor.
        /// </summary>
        /// <returns>Código hash único.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
