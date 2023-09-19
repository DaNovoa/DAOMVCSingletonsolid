using System;
using System.Collections.Generic;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class CascoController
    {
        private CascoService cascoService;
        private CascoView vista;

        public CascoController(CascoService cascoService, CascoView vista)
        {
            this.cascoService = cascoService ?? throw new ArgumentNullException(nameof(cascoService));
            this.vista = vista ?? throw new ArgumentNullException(nameof(vista));
        }

        public void ListarCascos()
        {
            try
            {
                List<Casco> cascos = cascoService.ObtenerCascos();
                vista.MostrarCascos(cascos);
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al listar cascos: " + e.Message);
            }
        }

        public void VerCasco(int id)
        {
            try
            {
                Casco casco = cascoService.ObtenerCascoPorId(id);

                if (casco != null)
                {
                    vista.MostrarCasco(casco);
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún casco con el ID {id}.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al obtener el casco: " + e.Message);
            }
        }

        public void RegistrarCasco(Casco casco)
        {
            try
            {
                if (cascoService.RegistrarCasco(casco))
                {
                    Console.WriteLine("---------------------");
                    Console.WriteLine("--> Registro exitoso: " + casco.Id);
                    vista.MostrarCasco(casco);
                }
                else
                {
                    Console.WriteLine("Error al registrar el casco.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al registrar el casco: " + e.Message);
            }
        }

        public void ActualizarCasco(int id, string nuevaCedula, string nuevoNombre, string nuevoApellido, char nuevaTalla, string nuevaMarca, int nuevasUnidades, decimal nuevoPrecio, DateTime nuevaFecha)
        {
            try
            {
                Casco cascoExistente = cascoService.ObtenerCascoPorId(id);

                if (cascoExistente != null)
                {
                    Console.WriteLine("------------Datos originales------------");
                    Console.WriteLine(cascoExistente);

                    cascoExistente.Cedula = nuevaCedula;
                    cascoExistente.Nombre = nuevoNombre;
                    cascoExistente.Apellido = nuevoApellido;
                    cascoExistente.Talla = nuevaTalla;
                    cascoExistente.Marca = nuevaMarca;
                    cascoExistente.Unidades = nuevasUnidades;
                    cascoExistente.Precio = nuevoPrecio;
                    cascoExistente.Fecha = nuevaFecha;

                    if (cascoService.ActualizarCasco(cascoExistente))
                    {
                        Console.WriteLine("--> Actualización exitosa");
                    }
                    else
                    {
                        Console.WriteLine("Error al actualizar el casco.");
                    }
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún casco con el ID {id}.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al actualizar el casco: " + e.Message);
            }
        }

        public void EliminarCasco(int id)
        {
            try
            {
                Casco cascoAEliminar = cascoService.ObtenerCascoPorId(id);

                if (cascoAEliminar != null)
                {
                    if (cascoService.EliminarCasco(id))
                    {
                        Console.WriteLine("Casco eliminado: " + cascoAEliminar.Id);
                    }
                    else
                    {
                        Console.WriteLine("Error al eliminar el casco.");
                    }
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún casco con el ID {id}.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al eliminar el casco: " + e.Message);
            }
        }
    }
}
