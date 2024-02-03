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
        static DateTime ValidarDataNeixament(DateTime dataNaix)
        {
            Console.Clear();
            bool dataValida = false;
            while (!dataValida)
            {
                if (dataNaix > DateTime.Now)
                {
                    Console.Write("Data incorrecta.Introdueix un altre data de naixament: ");
                    dataNaix = Convert.ToDateTime(Console.ReadLine());
                }
                else
                {
                    dataValida = true;
                }
            }
            return dataNaix;
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
        static void RecuperarUsuariSegons()
        {
            int i = 5;
            while (i != 0)
            {
                Console.Write("\r");
                Console.Write($"Tornant al menu: {i}'s");
                Thread.Sleep(1000);
                i--;
            }
        }
        static void ModificarUsuari()
        {
            char Finalitzar = 'S';
            Console.Write("Quin usuari vols trobar? ");
            string nomUsuari = Console.ReadLine();
            while (Finalitzar != 'N' && Finalitzar != 'n')
            {
                string usuari = RecuperarUsuari(nomUsuari);
                Console.Write("Quina dada vols modificar? ");
                string dada = Console.ReadLine();

                Console.Write("Introdueix el nou valor: ");
                string nouValor = Console.ReadLine();

                var dadesUsuari = usuari.Split(';');

                switch (dada.ToLower())
                {
                    case "nom":
                        dadesUsuari[0] = nouValor;
                        break;
                    case "cognom":
                        dadesUsuari[1] = nouValor;
                        break;
                    case "dni":
                        dadesUsuari[2] = nouValor;
                        break;
                    case "telefon":
                        dadesUsuari[3] = nouValor;
                        break;
                    case "datanaixament":
                        dadesUsuari[4] = nouValor;
                        break;
                    case "correu":
                        dadesUsuari[5] = nouValor;
                        break;
                    default:
                        Console.WriteLine("Dada no existent.");
                        return;
                }
                usuari = string.Join(";", dadesUsuari);
                var lineas = File.ReadAllLines("agenda.txt").ToList();
                int index = lineas.FindIndex(linea => linea.Split(';')[0] == nomUsuari);
                lineas[index] = usuari;
                File.WriteAllLines("agenda.txt", lineas);
                Console.WriteLine($"Vols tornar a modifcar alguna dada de {nomUsuari}? (S/N)");
                Finalitzar = Convert.ToChar(Console.ReadLine());
            }
        }
        static void EliminarUsuari()
        {
            char tornarEliminarUsuari = 'S';
            string nomUsuari, usuario;
            while (tornarEliminarUsuari != 'n' && tornarEliminarUsuari != 'N')
            {
                Console.Write("Quin usuari vols eliminar? ");
                nomUsuari = Console.ReadLine();

                usuario = RecuperarUsuari(nomUsuari);

                var lineas = File.ReadAllLines("agenda.txt").ToList();
                lineas.RemoveAll(linea => linea.Split(';')[0].Equals(nomUsuari));
                File.WriteAllLines("agenda.txt", lineas.Where(linea => !string.IsNullOrWhiteSpace(linea)));

                Console.WriteLine($"Usuari {nomUsuari} eliminat amb èxit.");
                Console.Write("Vols tornar a eliminar un usuari? (S/N)");
                tornarEliminarUsuari = Convert.ToChar(Console.ReadLine());
            }

        }
        static void MostrarAgenda()
        {
            var lineas = File.ReadLines("agenda.txt")
                .Select(linea => linea.Split(';'))
                .Where(datos => datos.Length >= 4)
                .Select(datos => new
                {
                    Nombre = datos[0],
                    Telefono = datos[3]
                })
                .OrderBy(usuario => usuario.Nombre)
                .ToList();

            for (int i = 0; i < lineas.Count; i++)
            {
                Console.WriteLine($"Nombre: {lineas[i].Nombre}, Teléfono: {lineas[i].Telefono}");
            }


        }
    }