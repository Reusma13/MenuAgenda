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

    }
}