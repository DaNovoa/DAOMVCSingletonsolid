using System;
using System.Collections.Generic;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class CascoService
    {
        private readonly ICascoDao dao;

        public CascoService(ICascoDao dao)
        {
            this.dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public bool RegistrarCasco(Casco casco)
        {
            try
            {
                return dao.RegistrarCasco(casco);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al registrar el casco: " + e.Message);
                return false;
            }
        }

        public bool ActualizarCasco(Casco casco)
        {
            try
            {
                return dao.ActualizarCasco(casco);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el casco: " + e.Message);
                return false;
            }
        }

        public bool EliminarCasco(int id)
        {
            try
            {
                return dao.EliminarCasco(id);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al eliminar el casco: " + e.Message);
                return false;
            }
        }

        public List<Casco> ObtenerCascos()
        {
            try
            {
                return dao.ObtenerCascos();
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener los cascos: " + e.Message);
                return new List<Casco>();
            }
        }

        public Casco ObtenerCascoPorId(int id)
        {
            try
            {
                return dao.ObtenerCascoPorId(id);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener el casco por ID: " + e.Message);
                return null;
            }
        }

        public string CalcularValorTotal()
        {
            List<Casco> cascos = ObtenerCascos();
            decimal total = 0;

            foreach (Casco casco in cascos)
            {
                total += (casco.Unidades * casco.Precio);
            }

            return total.ToString("C");
        }

    }
}
