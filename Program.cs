using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MenuAgenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MostrarMenu();
        }
        static void MostrarMenu()
        {
            int opcio, numero1, numero2;
            do
            {
                Console.Clear();
                Console.WriteLine(CrearMenu());
                Console.Write("Escull una opcio: ");
                opcio = Convert.ToChar(Console.ReadLine());
                switch (opcio)
                {
                    case 'q':
                        break;
                    case 'Q':
                        break;
                    case '1':
                        Console.Clear();
                        DonarAlta();
                        break;
                    case '2':
                        Console.Clear();
                        RecuperarUsuari();
                        break;
                    case '3':
                        Console.Clear();
                        ModificarUsuari();
                        break;
                    case '4':
                        Console.Clear();
                        EliminarUsuari();
                        break;
                    case '5':
                        Console.Clear();
                        MostrarAgenda();
                        break;
                    case '6':
                        Console.Clear();
                        OrdenarAgenda();
                        break;

                }
            } while (opcio != 'Q' && opcio != 'q');

        }
        static void DonarAlta()
        {
            Console.Write("Introdueix el nom: ");
            string nom = Console.ReadLine();
            Console.Write("Introdueix el cognom: ");
            string cognom = Console.ReadLine();
            Console.Write("Introdueix el DNI: ");
            string dni = Console.ReadLine();
            Console.Write("Introdueix el telefon: ");
            string telefon = Console.ReadLine();
            Console.Write("Introdueix la data de neixament: ");
            DateTime dataNaix = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Introduce tu correo electrónico:");
            string correuElectronic = Console.ReadLine();
            ObrirFitxer(ValidarNom(nom), ValidarCognom(cognom), ValidarDni(dni), ValidarTelefon(telefon), ValidarDataNeixament(dataNaix), ValidarCorreu(correuElectronic));
            DonarDAltaUsuariSegons();
        }
        static string ValidarNom(string nom)
        {
            nom = Regex.Replace(nom, @"[^a-zA-Z]", "");
            nom = char.ToUpper(nom[0]) + nom.Substring(1).ToLower();
            return nom;
        }
        static string ValidarCognom(string cognom)
        {
            cognom = Regex.Replace(cognom, @"[^a-zA-Z]", "");
            cognom = char.ToUpper(cognom[0]) + cognom.Substring(1).ToLower();
            return cognom;
        }
        static string ValidarDni(string dni)
        {
            Console.Clear();
            bool dniValid = false;
            while (!dniValid)
            {
                var dniRegex = new Regex(@"^\d{8}[A-Z]$");
                if (!dniRegex.IsMatch(dni))
                {
                    Console.Write("Aquest DNI no es valid. Introdueix un altre DNI: ");
                    dni = Console.ReadLine();
                }
                else
                {
                    dniValid = true;
                }
            }
            return dni;
        }
        static string ValidarTelefon(string telefon)
        {
            Console.Clear();
            bool telefonValid = false;
            while (!telefonValid)
            {
                var telefonRegex = new Regex(@"^\d{9}$");
                if (!telefonRegex.IsMatch(telefon))
                {
                    Console.Write("Telefon invalid. Introdueix un altre telefon: ");
                    telefon = Console.ReadLine();
                }
                else
                    telefonValid = true;
            }
            return telefon;
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
        static string ValidarCorreu(string correu)
        {
            Console.Clear();
            bool correuValid = false;
            while (!correuValid)
            {
                var correuRegex = new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z]{3,}\.(com|es)$");
                if (!correuRegex.IsMatch(correu))
                {
                    Console.Write("Correu Electronic invalid. Introdueix un altre correu: ");
                    correu = Console.ReadLine();
                }
                else
                    correuValid = true;
            }
            return correu;
        }
        static void ObrirFitxer(string nom, string cognom, string dni, string telefon, DateTime dataNaix, string correu)
        {
            StreamWriter sW = new StreamWriter("agenda.txt", true);
            sW.WriteLine($"{nom};{cognom};{dni};{telefon};{dataNaix.ToString("d")};{correu}\r");
            sW.Close();

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
            RecuperarUsuariSegons();
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

    }
}