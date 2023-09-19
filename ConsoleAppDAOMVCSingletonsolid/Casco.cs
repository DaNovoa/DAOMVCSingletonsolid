using System;

namespace ConsoleAppDAOMVCSingletonSolid
{
    public class Casco
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public char Talla { get; set; }
        public string Marca { get; set; }
        public int Unidades { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }

        public Casco(int id, string cedula, string apellido, string nombre, char talla, string marca, int unidades, decimal precio, DateTime fecha)
        {
            Id = id;
            Cedula = cedula ?? throw new ArgumentNullException(nameof(cedula));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Talla = talla;
            Marca = marca ?? throw new ArgumentNullException(nameof(marca));
            Unidades = unidades;
            Precio = precio;
            Fecha = fecha;
        }

        public Casco(string cedula, string apellido, string nombre, char talla, string marca, int unidades, decimal precio, DateTime fecha)
        {
            Cedula = cedula ?? throw new ArgumentNullException(nameof(cedula));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Talla = talla;
            Marca = marca ?? throw new ArgumentNullException(nameof(marca));
            Unidades = unidades;
            Precio = precio;
            Fecha = fecha;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nCédula: {Cedula}\nApellido: {Apellido}\nNombre: {Nombre}\nTalla: {Talla}\nMarca: {Marca}\nUnidades: {Unidades}\nPrecio: {Precio:C}\nFecha: {Fecha.ToShortDateString()}";
        }
    }
}
