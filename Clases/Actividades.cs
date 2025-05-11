using System;
using System.Collections.Generic;

namespace TFG_DavidGomez.Clases
{
    public class Actividades
    {
        /// <summary>
        /// Identificador único de la actividad (clave primaria).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la actividad.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción de la actividad.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Lista de materiales representada como una cadena de texto separada por comas.
        /// </summary>
        public List<string> Materiales { get; set; }


        /// <summary>
        /// Fecha de realización de la actividad.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Imagen asociada a la actividad (almacenada como BLOB en la BD).
        /// </summary>
        public byte[] Imagen { get; set; }

        /// <summary>
        /// ID del usuario (monitor o admin) que creó la actividad.
        /// </summary>
        public int? IdUsuario { get; set; }

        /// <summary>
        /// Constructor vacío.
        /// </summary>
        public Actividades() { }

        /// <summary>
        /// Constructor completo.
        /// </summary>
        public Actividades(string nombre, string descripcion, List<string> materiales, DateTime fecha, byte[] imagen, int? idUsuario)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Materiales = materiales;
            Fecha = fecha;
            Imagen = imagen;
            IdUsuario = idUsuario;
        }

        public override string ToString()
        {
            return $"Actividad: {Nombre}, Fecha: {Fecha.ToShortDateString()}, Creador ID: {IdUsuario}";
        }

        public override bool Equals(object obj)
        {
            return obj is Actividades actividad && Id == actividad.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
