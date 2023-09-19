using ConsoleAppDAOMVCSingletonSolid;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDAOMVCSingletonsolid
{
    public class CascoDaoImpl : ICascoDao
    {
        private const string INSERT_QUERY = "INSERT INTO cascos (comprador, apellido, cedula, talla, marca, unidades, precio, fecha) VALUES (@comprador, @apellido, @cedula, @talla, @marca, @unidades, @precio, @fecha)";
        private const string SELECT_ALL_QUERY = "SELECT * FROM cascos ORDER BY idcasco";
        private const string UPDATE_QUERY = "UPDATE cascos SET comprador=@comprador, apellido=@apellido, cedula=@cedula, talla=@talla, marca=@marca, unidades=@unidades, precio=@precio, fecha=@fecha WHERE idcasco=@id";
        private const string DELETE_QUERY = "DELETE FROM cascos WHERE idcasco=@id";
        private const string SELECT_BY_ID_QUERY = "SELECT * FROM cascos WHERE idcasco=@id";

        private readonly MySqlConnection _connection;

        public CascoDaoImpl(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public bool ActualizarCasco(Casco casco)
        {
            bool actualizado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(UPDATE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@comprador", casco.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", casco.Apellido);
                    cmd.Parameters.AddWithValue("@cedula", casco.Cedula);
                    cmd.Parameters.AddWithValue("@talla", casco.Talla);
                    cmd.Parameters.AddWithValue("@marca", casco.Marca);
                    cmd.Parameters.AddWithValue("@unidades", casco.Unidades);
                    cmd.Parameters.AddWithValue("@precio", casco.Precio);
                    cmd.Parameters.AddWithValue("@fecha", casco.Fecha);
                    cmd.Parameters.AddWithValue("@id", casco.Id);
                    cmd.ExecuteNonQuery();
                    actualizado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al actualizar el casco", ex);
            }
            finally
            {
                _connection.Close();
            }

            return actualizado;
        }

        public bool EliminarCasco(int id)
        {
            bool eliminado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(DELETE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al eliminar el casco", ex);
            }
            finally
            {
                _connection.Close();
            }

            return eliminado;
        }

        public Casco ObtenerCascoPorId(int id)
        {
            Casco casco = null;

            try
            {

                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_BY_ID_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            casco = CrearCascoDesdeDataReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener el casco por ID", ex);
            }
            finally
            {
                _connection.Close();
            }

            return casco;
        }

        public List<Casco> ObtenerCascos()
        {
            using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
            {
                try
                {
                    ProveState();

                    List<Casco> listaCascos = new List<Casco>();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Casco casco = CrearCascoDesdeDataReader(reader);
                            listaCascos.Add(casco);
                        }
                    }

                    return listaCascos;
                }
                catch (MySqlException ex)
                {
                    throw new DAOException("Error al obtener los cascos", ex);
                }
            }
        }

        private Casco CrearCascoDesdeDataReader(MySqlDataReader reader)
        {
            int id = reader.IsDBNull(reader.GetOrdinal("idcasco")) ? 0 : reader.GetInt32("idcasco");
            string cedula = reader.GetString("cedula");
            string nombre = reader.GetString("comprador");
            string apellido = reader.GetString("apellido");
            char talla = reader.GetChar("talla");
            string marca = reader.GetString("marca");
            int unidades = reader.GetInt32("unidades");
            decimal precio = reader.GetDecimal("precio");
            DateTime fecha = reader.GetDateTime("fecha");
            return new Casco(id, cedula, nombre, apellido, talla, marca, unidades, precio, fecha);
        }


        public bool RegistrarCasco(Casco casco)
        {
            using (MySqlCommand cmd = new MySqlCommand(INSERT_QUERY, _connection))
            {
                try
                {
                    ProveState();

                    cmd.Parameters.AddWithValue("@comprador", casco.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", casco.Apellido);
                    cmd.Parameters.AddWithValue("@cedula", casco.Cedula);
                    cmd.Parameters.AddWithValue("@talla", casco.Talla);
                    cmd.Parameters.AddWithValue("@marca", casco.Marca);
                    cmd.Parameters.AddWithValue("@unidades", casco.Unidades);
                    cmd.Parameters.AddWithValue("@precio", casco.Precio);
                    cmd.Parameters.AddWithValue("@fecha", casco.Fecha);

                    cmd.ExecuteNonQuery();

                    casco.Id = (int)cmd.LastInsertedId;

                    return true;
                }
                catch (MySqlException ex)
                {
                    throw new DAOException("Error al registrar el casco", ex);
                }
            }
        }

        private void ProveState()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
    }
}
