using System;
using System.Collections.Generic;

namespace TFG_DavidGomez.Clases
{
    /// <summary>
    /// Representa un usuario en el sistema (padre, monitor o administrador).
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DNI del usuario.
        /// </summary>
        public string DNI { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Contraseña del usuario (encriptada).
        /// </summary>
        public string Contrasena { get; set; }

        /// <summary>
        /// Rol del usuario (padre, monitor o admin).
        /// </summary>
        public string Rol { get; set; }

        /// <summary>
        /// Fecha de registro del usuario.
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Teléfono del usuario.
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Dirección del usuario.
        /// </summary>
        public string Direccion { get; set; }

        public string Correo { get; set; }

        /// <summary>
        /// Lista de hijos (niños asociados). Solo usada en lógica de aplicación.
        /// </summary>
        public List<Nino> Hijos { get; set; } = new List<Nino>();

        // === Constructores ===

        public Usuario() { }

        public Usuario(string nombre, string apellidos, string dni, string correo, string contrasena, string rol, DateTime fechaRegistro, string telefono, string direccion)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dni;
            Contrasena = contrasena;
            Rol = rol;
            FechaRegistro = fechaRegistro;
            Telefono = telefono;
            Direccion = direccion;
            Correo = correo;
        }

        // === Métodos ===

        public override string ToString()
        {
            return $"Nombre: {Nombre}, Apellidos: {Apellidos}, DNI: {DNI}, Rol: {Rol}, Correo: {Correo}, Teléfono: {Telefono}, Dirección: {Direccion}";
        }

        public override bool Equals(object obj)
        {
            return obj is Usuario usuario && Id == usuario.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void AnadirNino(Nino nuevoNino)
        {
            if (nuevoNino == null)
                throw new ArgumentNullException(nameof(nuevoNino));
            Hijos.Add(nuevoNino);
        }
    }
}
