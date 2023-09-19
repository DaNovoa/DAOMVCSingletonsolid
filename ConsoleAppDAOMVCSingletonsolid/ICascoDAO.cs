using System.Collections.Generic;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public interface ICascoDao
    {
        bool RegistrarCasco(Casco casco);
        List<Casco> ObtenerCascos();
        bool ActualizarCasco(Casco casco);
        bool EliminarCasco(int id);
        Casco ObtenerCascoPorId(int id);
    }
}
