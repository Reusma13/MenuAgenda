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
        static void RecuperarUsuari()
        {
            char trobarUsuari = 'S';
            while (trobarUsuari != 'N' && trobarUsuari != 'n')
            {
                Console.Clear();
                Console.Write("Quin usuari vols trobar? ");
                string nomUsuari = Console.ReadLine();

                var linea = File.ReadLines("agenda.txt")
                    .Select(linea => linea.Split(';')[0]).ToList(); // Ponemos que esta separado por ; i que el nombre de usuario es el primero

                bool trobat = linea.Contains(nomUsuari);

                if (trobat)
                {
                    Console.WriteLine($"Usuari: {nomUsuari} trobat.");
                    trobarUsuari = 'N';
                }
                else
                {
                    Console.Write("Usuari no trobat. Vols trobar un altre usuari? (S/N)");
                    trobarUsuari = Convert.ToChar(Console.ReadLine());
                }
            }


        }
        static string RecuperarUsuari(string nomUsuari)
        {
            char trobarUsuari = 'S';
            bool trobat;
            while (trobarUsuari != 'N' && trobarUsuari != 'n')
            {
                var linea = File.ReadLines("agenda.txt")
                    .Select(linea => linea.Split(';')[0]).ToList(); // Ponemos que esta separado por ; i que el nombre de usuario es el primero

                trobat = linea.Contains(nomUsuari);

                if (trobat)
                {
                    trobarUsuari = 'N';
                }
                else
                {
                    Console.Write("Usuari no trobat. Vols trobar un altre usuari? (S/N)");
                    trobarUsuari = Convert.ToChar(Console.ReadLine());
                }
            }
            return nomUsuari;
        }

    }
}