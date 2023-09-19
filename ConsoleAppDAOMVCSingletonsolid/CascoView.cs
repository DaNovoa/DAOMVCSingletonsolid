using System;
using System.Collections.Generic;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class CascoView
    {
        public void MostrarCasco(Casco casco)
        {
            Console.WriteLine("Datos del Casco:");
            Console.WriteLine("------------");
            Console.WriteLine(casco.ToString());
        }

        public void MostrarCascos(List<Casco> cascos)
        {
            if (cascos.Count == 0)
            {
                Console.WriteLine("No hay cascos para mostrar.");
                return;
            }

            Console.WriteLine("Lista de Cascos:");
            foreach (Casco casco in cascos)
            {
                Console.WriteLine("------------");
                Console.WriteLine(casco.ToString());
            }
        }
    }
}
