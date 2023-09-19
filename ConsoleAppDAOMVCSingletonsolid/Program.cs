using System;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string action;
            int id;

            ICascoDao dao = CascoDAOFactory.CrearCascoDAO();
            CascoView vista = new CascoView();
            CascoService cascoService = new CascoService(dao);
            CascoController controller = new CascoController(cascoService, vista);

            while (true)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("[L]istar | [R]egistrar | [A]ctualizar | [E]liminar | [C]alcular Valor Total | [S]alir | [H]elp: ");
                action = Console.ReadLine()?.ToUpper();

                if (!string.IsNullOrEmpty(action))
                {
                    try
                    {
                        switch (action)
                        {
                            case "L":
                                controller.ListarCascos();
                                break;
                            case "R":
                                Casco nuevoCasco = InputCasco();
                                controller.RegistrarCasco(nuevoCasco);
                                break;
                            case "A":
                                id = InputId();
                                controller.VerCasco(id);
                                Console.WriteLine("------------------------");
                                Console.WriteLine("Ingrese los nuevos datos");
                                Console.WriteLine("------------------------");
                                string nuevaCedula = InputString("Ingrese la nueva cédula del casco: ");
                                string nuevoNombre = InputString("Ingrese el nuevo nombre del casco: ");
                                string nuevoApellido = InputString("Ingrese el nuevo apellido del casco: ");
                                char nuevaTalla = InputChar("Ingrese la nueva talla del casco: ");
                                string nuevaMarca = InputString("Ingrese la nueva marca del casco: ");
                                int nuevasUnidades = InputInt("Ingrese las nuevas unidades disponibles del casco: ");
                                decimal nuevoPrecio = InputDecimal("Ingrese el nuevo precio del casco: ");
                                DateTime nuevaFecha = InputDate("Ingrese la nueva fecha (YYYY-MM-DD) del casco: ");
                                controller.ActualizarCasco(id, nuevaCedula, nuevoNombre, nuevoApellido, nuevaTalla, nuevaMarca, nuevasUnidades, nuevoPrecio, nuevaFecha);
                                controller.VerCasco(id);
                                break;
                            case "E":
                                id = InputId();
                                controller.EliminarCasco(id);
                                break;
                            case "S":
                                return;
                            case "H":
                                MostrarAyuda();
                                break;
                            case "C":
                                string valorTotalDecimal = cascoService.CalcularValorTotal();
                                Console.WriteLine("Valor Total: " + valorTotalDecimal);
                                break;
                            default:
                                Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                                break;
                        }
                    }
                    catch (DAOException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }
        }

        private static void MostrarAyuda()
        {
            Console.WriteLine("Opciones disponibles:");
            Console.WriteLine("[L]istar: Muestra la lista de cascos.");
            Console.WriteLine("[R]egistrar: Registra un nuevo casco.");
            Console.WriteLine("[A]ctualizar: Actualiza un casco existente.");
            Console.WriteLine("[E]liminar: Elimina un casco.");
            Console.WriteLine("[C]alcular Valor Total: Calcula el valor total de los cascos.");
            Console.WriteLine("[S]alir: Sale del programa.");
            Console.WriteLine("[H]elp: Muestra esta ayuda.");
        }

        private static Casco InputCasco()
        {
            string cedula = InputString("Ingrese el número de cédula del cliente: ");
            string nombre = InputString("Ingrese el nombre del cliente: ");
            string apellido = InputString("Ingrese el apellido del cliente: ");
            char talla = InputChar("Ingrese la talla del casco (un carácter): ");
            string marca = InputString("Ingrese la marca del casco: ");
            int unidades = InputInt("Ingrese las unidades solicitadas: ");
            decimal precio = InputDecimal("Ingrese el precio del casco: ");
            DateTime fecha = InputDate("Ingrese la fecha (YYYY-MM-DD): ");
            return new Casco(cedula, apellido, nombre, talla, marca, unidades, precio, fecha);
        }

        private static int InputId()
        {
            int id;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese un valor entero para el ID del casco: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return id;
        }

        private static string InputString(string message)
        {
            string s;
            while (true)
            {
                Console.WriteLine(message);
                s = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(s) && s.Length >= 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("La longitud de la cadena debe ser >= 2");
                }
            }
            return s;
        }
                

        private static int InputInt(string message)
        {
            int value;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (int.TryParse(Console.ReadLine(), out value))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return value;
        }

        private static decimal InputDecimal(string message)
        {
            decimal value;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (decimal.TryParse(Console.ReadLine(), out value))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return value;
        }
        private static char InputChar(string message)
        {
            char character;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    string input = Console.ReadLine();
                    if (input.Length == 1)
                    {
                        character = input[0];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Debe ingresar un solo carácter.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de carácter.");
                }
            }
            return character;
        }


        private static DateTime InputDate(string message)
        {
            DateTime date;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de fecha (YYYY-MM-DD)");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de fecha (YYYY-MM-DD)");
                }
            }
            return date;
        }
    }
}
