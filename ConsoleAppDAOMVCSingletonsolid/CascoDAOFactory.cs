using ConsoleAppDAOMVCSingletonsolid;
using MySql.Data.MySqlClient;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class CascoDAOFactory
    {
        public static ICascoDao CrearCascoDAO()
        {
            using (MySqlConnection connection = Conexion.Instance.AbrirConexion())
            {
                return new CascoDaoImpl(connection);
            }
        }
    }
}


