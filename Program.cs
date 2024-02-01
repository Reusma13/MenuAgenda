using System.IO;
using System.Text.RegularExpressions;
using Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MenuAgenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MostrarMenu();
        }
        static void OrdenarAgenda()
        {
            var lineas = File.ReadLines("agenda.txt")
                .Select(linea => new
                {
                    Datos = linea.Split(';'),
                    Nombre = linea.Split(';')[0]
                })
                .OrderBy(usuario => usuario.Nombre)
                .Select(usuario => string.Join(";", usuario.Datos))
                .ToList();

            File.WriteAllLines("agenda.txt", lineas);
            Console.WriteLine("La agenda a sido ordenada.");
            RecuperarUsuariSegons();
        }


    }
}