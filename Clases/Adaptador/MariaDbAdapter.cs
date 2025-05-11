using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using TFG_DavidGomez.Clases.Conexion;

namespace TFG_DavidGomez.Clases.Adaptador
{

    /// <summary>
    /// Clase que actúa como adaptador para interactuar con la base de datos MongoDB.
    /// Proporciona métodos para manejar actividades, inscripciones, usuarios y otros datos relacionados.
    /// </summary>
    internal class MariaDbAdapter
    {
        private readonly ConMDB con;

        public MariaDbAdapter()
        {
            con = new ConMDB();
        }

        // Método para encriptar la contraseña con SHA-256
        private string EncriptarSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public (bool, string, string) VerificarAccesoConRol(string usuario, string contraseña)
        {
            try
            {
                con.AbrirConexion();
                using (MySqlCommand command = new MySqlCommand("SELECT id, contrasena, tipo FROM Usuarios WHERE nombre = @nombre", con.ObtenerConexion()))
                {
                    command.Parameters.AddWithValue("@nombre", usuario);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string contrasenaAlmacenada = reader.GetString("contrasena");
                            string contrasenaIngresadaEncriptada = EncriptarSHA256(contraseña);

                            if (contrasenaAlmacenada == contrasenaIngresadaEncriptada)
                            {
                                string idUsuario = reader["id"].ToString();
                                string rol = reader["tipo"].ToString();

                                return (true, idUsuario, rol);
                            }
                        }
                    }
                }
                return (false, null, null); // Usuario o contraseña incorrectos
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en VerificarAccesoConRol (MariaDB): " + ex.Message);
                return (false, null, null);
            }
            finally
            {
                con.CerrarConexion();
            }
        }

        public DataRow ObtenerActividadPorDia(DateTime dia)
        {
            try
            {
                con.AbrirConexion();

                string query = "SELECT * FROM Actividades WHERE fecha >= @inicio AND fecha < @fin LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@inicio", dia.Date);
                    cmd.Parameters.AddWithValue("@fin", dia.Date.AddDays(1));

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                            return dt.Rows[0];
                        else
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la actividad por día: " + ex.Message);
                return null;
            }
        }


        public List<string> ObtenerNinosPorDia(DateTime dia)
        {
            var resultado = new List<string>();


            try
            {
                con.AbrirConexion();

                // Consulta para obtener los niños inscritos en esa fecha con sus padres
                string query = @"
                    SELECT Ninos.nombre AS nombre_nino, Ninos.dni AS dni_nino, Usuarios.nombre AS nombre_padre
                    FROM Inscripciones
                    INNER JOIN Ninos ON Inscripciones.id_nino = Ninos.id
                    INNER JOIN Usuarios ON Inscripciones.id_padre = Usuarios.id
                    WHERE Inscripciones.fecha_inscripcion = @fecha;
                    ";

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@fecha", dia.Date);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreNino = reader["nombre_nino"].ToString();
                            string dniNino = reader["dni_nino"]?.ToString() ?? "Sin DNI";
                            string nombrePadre = reader["nombre_padre"].ToString();

                            resultado.Add($"Niño: {nombreNino} ({dniNino}) - Padre: {nombrePadre}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener niños por día: " + ex.Message);
            }
            finally
            {
                con.CerrarConexion();
            }

            return resultado;
        }

        public List<string> ObtenerMaterialesDeHoy(DateTime dia)
        {
            var materiales = new List<string>();

            try
            {
                con.AbrirConexion();

                string query = @"SELECT materiales FROM Actividades WHERE fecha = @fecha LIMIT 1;";

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@fecha", dia.Date);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string materialesStr = reader["materiales"]?.ToString();

                            if (!string.IsNullOrEmpty(materialesStr))
                            {
                                // Separar los materiales por comas y agregarlos a la lista
                                materiales = materialesStr.Split(',').Select(m => m.Trim()).ToList();
                            }
                        }
                    }
                }

                if (materiales.Count == 0)
                    materiales.Add("No hay materiales para esta actividad.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener materiales: " + ex.Message);
                materiales.Clear();
                materiales.Add("Error al obtener materiales.");
            }
            finally
            {
                con.CerrarConexion();
            }

            return materiales;
        }

        public string ObtenerNinoPorFecha(DateTime fecha)
        {
            string resultado = "";
            var con = new ConMDB();

            try
            {
                con.AbrirConexion();

                string query = @"
                    SELECT n.nombre, n.apellidos
                    FROM Inscripciones i
                    JOIN Ninos n ON i.id_nino = n.id
                    WHERE i.fecha_inscripcion = @fecha
                    LIMIT 1;
                ";

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@fecha", fecha.Date);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            string apellidos = reader["apellidos"].ToString();
                            resultado = $"{nombre} {apellidos}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener niño por fecha: " + ex.Message);
            }
            finally
            {
                con.CerrarConexion();
            }

            return resultado;
        }


        public Nino CargarDatosNino(int idNino)
        {
            Nino nino = null;

            try
            {
                con.AbrirConexion();

                string query = @"
                    SELECT id, nombre, apellidos, fecha_nacimiento, dni, id_padre
                    FROM Ninos
                    WHERE id = @idNino;
                ";

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idNino", idNino);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nino = new Nino
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Apellidos = reader.IsDBNull(reader.GetOrdinal("apellidos")) ? "" : reader.GetString("apellidos"),
                                FechaNacimiento = reader.GetDateTime("fecha_nacimiento"),
                                DNI = reader.IsDBNull(reader.GetOrdinal("dni")) ? null : reader.GetString("dni"),
                                IdPadre = reader.IsDBNull(reader.GetOrdinal("id_padre")) ? 0 : reader.GetInt32("id_padre")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar datos del niño: " + ex.Message);
            }
            finally
            {
                con.CerrarConexion();
            }

            return nino;
        }

        public List<Nino> CargarDatosNinoPorPadre(int idPadre)
        {
            List<Nino> ninos = new List<Nino>();

            try
            {
                ConMDB con = new ConMDB();
                con.AbrirConexion();

                string query = "SELECT id, nombre, apellidos, fecha_nacimiento, dni, id_padre FROM Ninos WHERE id_padre = @idPadre";
                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idPadre", idPadre);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Nino nino = new Nino
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Apellidos = reader.IsDBNull(reader.GetOrdinal("apellidos")) ? "" : reader.GetString("apellidos"),
                                FechaNacimiento = reader.GetDateTime("fecha_nacimiento"),
                                DNI = reader.IsDBNull(reader.GetOrdinal("dni")) ? null : reader.GetString("dni"),
                                IdPadre = reader.GetInt32("id_padre")
                            };

                            ninos.Add(nino);
                        }
                    }
                }

                con.CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar niños por padre: {ex.Message}");
            }

            return ninos;
        }

        public bool VerificarInscripcion(int idNino, int idActividad)
        {
            try
            {
                ConMDB con = new ConMDB();
                con.AbrirConexion();

                string query = "SELECT id FROM Inscripciones WHERE id_nino = @idNino AND id_actividad = @idActividad";
                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idNino", idNino);
                    cmd.Parameters.AddWithValue("@idActividad", idActividad);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.Read(); // true si hay algún resultado, false si no
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en VerificarInscripcion: " + ex.Message);
                return false;
            }
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            try
            {
                ConMDB con = new ConMDB();
                con.AbrirConexion();

                string query = "SELECT * FROM Usuarios WHERE id = @idUsuario";
                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Apellidos = reader.GetString("apellidos"),
                                DNI = reader.GetString("dni"),
                                Correo = reader.GetString("correo"),
                                Contrasena = reader.GetString("contrasena"),
                                Rol = reader.GetString("tipo"),
                                FechaRegistro = reader.GetDateTime("fecha_registro"),
                                Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? "" : reader.GetString("telefono"),
                                Direccion = reader.IsDBNull(reader.GetOrdinal("direccion")) ? "" : reader.GetString("direccion")
                            };
                        }
                    }
                }

                return null; // Usuario no encontrado
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuario por ID: " + ex.Message);
                return null;
            }
        }

        public UsuarioMonitor ObtenerUsuarioPorIdMoni(int idUsuario)
        {
            try
            {
                ConMDB con = new ConMDB();
                con.AbrirConexion();

                string query = "SELECT * FROM Usuarios WHERE id = @idUsuario AND tipo = 'monitor'";
                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime fechaRegistro = reader.IsDBNull(reader.GetOrdinal("fecha_registro"))
                                ? DateTime.MinValue
                                : reader.GetDateTime("fecha_registro");

                            var usuario = new UsuarioMonitor(
                                reader.GetString("nombre"),
                                reader.GetString("apellidos"),
                                reader.GetString("dni"),
                                reader.GetString("correo"),
                                reader.GetString("contrasena"),
                                reader.GetString("tipo"),
                                fechaRegistro,
                                reader.IsDBNull(reader.GetOrdinal("telefono")) ? "" : reader.GetString("telefono"),
                                reader.IsDBNull(reader.GetOrdinal("direccion")) ? "" : reader.GetString("direccion")
                            );

                            return usuario;
                        }
                    }
                }

                Console.WriteLine($"No se encontró un monitor con ID: {idUsuario}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener monitor: {ex.Message}");
                return null;
            }
        }

        public List<Nino> ObtenerNinosPorActividad(int idActividad)
        {
            var ninos = new List<Nino>();

            try
            {
                ConMDB con = new ConMDB();
                con.AbrirConexion();

                // Obtener IDs de niños inscritos en la actividad
                var idNinos = new List<int>();
                string queryInscripciones = "SELECT id_nino FROM Inscripciones WHERE id_actividad = @idActividad";

                using (MySqlCommand cmd = new MySqlCommand(queryInscripciones, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idActividad", idActividad);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idNinos.Add(reader.GetInt32("id_nino"));
                        }
                    }
                }

                if (idNinos.Count == 0)
                {
                    Console.WriteLine("No hay niños inscritos en esta actividad.");
                    return ninos;
                }

                // Obtener los datos de los niños usando los IDs
                string queryNinos = $"SELECT * FROM Ninos WHERE id IN ({string.Join(",", idNinos)})";
                using (MySqlCommand cmd = new MySqlCommand(queryNinos, con.ObtenerConexion()))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nino = new Nino
                        {
                            Id = reader.GetInt32("id"),
                            Nombre = reader.GetString("nombre"),
                            Apellidos = reader.IsDBNull(reader.GetOrdinal("apellidos")) ? "" : reader.GetString("apellidos"),
                            FechaNacimiento = reader.GetDateTime("fecha_nacimiento"),
                            DNI = reader.IsDBNull(reader.GetOrdinal("dni")) ? null : reader.GetString("dni"),
                            IdPadre = reader.IsDBNull(reader.GetOrdinal("id_padre")) ? 0 : reader.GetInt32("id_padre")
                        };

                        ninos.Add(nino);
                    }
                }

                return ninos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Nino>();
            }
        }

        public List<string> ObtenerMaterialesPorActividad(int idActividad)
        {
            var materiales = new List<string>();

            try
            {
                ConMDB con = new ConMDB();
                con.AbrirConexion();

                string query = "SELECT materiales FROM Actividades WHERE id = @idActividad";

                using (MySqlCommand cmd = new MySqlCommand(query, con.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@idActividad", idActividad);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(reader.GetOrdinal("materiales")))
                        {
                            string materialesStr = reader.GetString("materiales");
                            materiales = materialesStr.Split(',').Select(m => m.Trim()).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener materiales: {ex.Message}");
            }

            return materiales;
        }

        public List<Actividades> ObtenerActividadesDelMes(DateTime mes)
        {
            List<Actividades> actividades = new List<Actividades>();

            string query = @"SELECT id, nombre, descripcion, materiales, fecha, imagen, id_usuario 
                     FROM Actividades 
                     WHERE MONTH(fecha) = @mes AND YEAR(fecha) = @anio";

            try
            {
                using (var connection = con.ObtenerConexion())
                {
                    con.AbrirConexion();

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@mes", mes.Month);
                        cmd.Parameters.AddWithValue("@anio", mes.Year);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Actividades actividad = new Actividades
                                {
                                    Id = reader.GetInt32("id"),
                                    Nombre = reader.GetString("nombre"),
                                    Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? "" : reader.GetString("descripcion"),
                                    Materiales = reader.IsDBNull(reader.GetOrdinal("materiales")) ? new List<string>() : reader.GetString("materiales").Split(',').ToList(),
                                    Fecha = reader.GetDateTime("fecha"),
                                    Imagen = reader.IsDBNull(reader.GetOrdinal("imagen")) ? null : (byte[])reader["imagen"],
                                    IdUsuario = reader.IsDBNull(reader.GetOrdinal("id_usuario")) ? 0 : reader.GetInt32("id_usuario")
                                };

                                actividades.Add(actividad);
                            }
                        }
                    }

                    con.CerrarConexion();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener actividades del mes: " + ex.Message);
            }

            return actividades;
        }


    }

}

