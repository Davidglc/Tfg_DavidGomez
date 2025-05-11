using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace TFG_DavidGomez.Clases
{
    /// <summary>
    /// Representa a un niño con sus atributos personales.
    /// </summary>
    public class Nino
    {
        /// <summary>
        /// Identificador único del niño.
        /// Este campo es la clave primaria en la base de datos de MongoDB.
        ///// </summary>
        //[BsonId]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del niño.
        /// </summary>
        //[BsonElement("Nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// DNI del niño, utilizado para su identificación.
        ///// </summary>
        //[BsonElement("DNI")]
        public string DNI { get; set; }

        /// <summary>
        /// Apellidos del niño.
        ///// </summary>
        //[BsonElement("Apellidos")]
        public string Apellidos { get; set; }

        /// <summary>
        /// Fecha de nacimiento del niño.
        /// El atributo está marcado con la opción <see cref="BsonDateTimeOptions"/> para asegurar que se guarde correctamente la fecha con la zona horaria local.
        ///// </summary>
        //[BsonElement("FechaNacimiento")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FechaNacimiento { get; set; }

        /// <summary>
        /// Edad del niño, que se calcula en base a la fecha de nacimiento.
        ///// </summary>
        //[BsonElement("Edad")]
        public int Edad { get; set; }

        /// <summary>
        /// Identificador del padre del niño.
        /// Este campo es una referencia al objeto <see cref="Padre"/> que contiene la información del padre.
        /// </summary>
        //[BsonElement("IdPadre")]
        //[BsonRepresentation(BsonType.ObjectId)]
        public int IdPadre { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="Nino"/>. Inicializa un nuevo objeto de tipo Nino con los valores proporcionados.
        /// </summary>
        /// <param name="nombre">Nombre del niño.</param>
        /// <param name="dni">DNI del niño.</param>
        /// <param name="apellidos">Apellidos del niño.</param>
        /// <param name="fechaNacimiento">Fecha de nacimiento del niño.</param>
        /// <param name="edad">Edad del niño.</param>
        public Nino(string nombre, string dni, string apellidos, DateTime fechaNacimiento, int edad)
        {
            this.Nombre = nombre;
            this.DNI = dni;
            this.Apellidos = apellidos;
            this.FechaNacimiento = fechaNacimiento;
            this.Edad = edad;
        }

        /// <summary>
        /// Constructor vacío para inicialización sin parámetros.
        /// </summary>
        public Nino() { }

        /// <summary>
        /// Compara dos objetos <see cref="Nino"/> para verificar si son iguales.
        /// Se considera que son iguales si tienen el mismo identificador <see cref="Id"/>.
        /// </summary>
        /// <param name="obj">El objeto con el que se va a comparar.</param>
        /// <returns>True si los objetos son iguales, de lo contrario, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is Nino nino && Id.Equals(nino.Id);
        }

        /// <summary>
        /// Obtiene el valor del hash del objeto <see cref="Nino"/> basado en su identificador.
        /// </summary>
        /// <returns>Valor hash del objeto.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
