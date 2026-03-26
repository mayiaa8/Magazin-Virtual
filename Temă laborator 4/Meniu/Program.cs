using System;
using System.Collections.Generic; // Avem nevoie de asta pentru List<>
using LibrarieModele;
using NivelStocareDate;

namespace Meniu
{
    class Program
    {
        static AdministrareProduse admin = new AdministrareProduse();

        static void Main()
        {
            string optiune;

            do
            {
                Console.WriteLine("\n--- MENIU MINIMARKET ---");
                Console.WriteLine("1. Adaugare produs");
                Console.WriteLine("2. Afisare produse");
                Console.WriteLine("3. Cautare produs");
                Console.WriteLine("4. Vanzare produs");
                Console.WriteLine("X. Iesire");

                Console.WriteLine("\nAlegeti o optiune:");
                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "1":
                        Adaugare();
                        break;

                    case "2":
                        List<Produs> listaProduse = admin.GetProduse();
                        AfisareProduse(listaProduse);
                        break;

                    case "3":
                        Cautare();
                        break;

                    case "4":
                        Vanzare();
                        break;

                    case "X":
                        Console.WriteLine("Aplicatia se inchide...");
                        break;

                    default:
                        Console.WriteLine("Optiune inexistenta!");
                        break;
                }

            } while (optiune != "X");
        }

        public static void AfisareProduse(List<Produs> produse)
        {
            Console.WriteLine("Produsele din magazin sunt:");
            if (produse.Count == 0)
            {
                Console.WriteLine("Nu exista produse in stoc.");
                return;
            }

            foreach (Produs p in produse)
            {
                Console.WriteLine(p.Info());
            }
        }

        static void Adaugare()
        {
            Console.Write("Introduceti numele produsului: ");
            string n = Console.ReadLine();

            Console.Write("Introduceti pretul: ");
            double p = double.Parse(Console.ReadLine());

            Console.Write("Introduceti cantitatea: ");
            int c = int.Parse(Console.ReadLine());

            Console.Write("Categorie (1-Aliment, 2-Bautura, 3-Dulciuri): ");
            CategorieProdus cat = (CategorieProdus)int.Parse(Console.ReadLine());

            OptiuniProdus optiuni = OptiuniProdus.Bio | OptiuniProdus.Perisabil;

            admin.AddProdus(new Produs(0, n, cat, p, c, optiuni));
            Console.WriteLine("Produs adaugat cu succes.");
        }

        static void Cautare()
        {
            Console.Write("Introduceti numele produsului cautat: ");
            string n = Console.ReadLine();

            Produs res = admin.CautaProdus(n);

            if (res != null)
            {
                Console.WriteLine("Produs gasit: " + res.Info());
            }
            else
            {
                Console.WriteLine("Produsul nu a fost gasit in minimarket.");
            }
        }

        static void Vanzare()
        {
            Console.Write("Nume produs pentru vanzare: ");
            string n = Console.ReadLine();

            Console.Write("Cantitate dorita: ");
            int c = int.Parse(Console.ReadLine());

            string mesajRezultat = admin.Vinde(n, c);
            Console.WriteLine(mesajRezultat);
        }
    }
}