using System;
using System.Collections.Generic;
using LibrarieModele;
using NivelStocareDate;

namespace Meniu
{
    class Program
    {
        static AdministrareProduse admin = new AdministrareProduse("produse.txt");

        static void Main()
        {
            string optiune;

            do
            {
                Console.WriteLine("-----Magazin Virtual-----");
                Console.WriteLine("1. Adaugare produs nou");
                Console.WriteLine("2. Afisare catalog produse");
                Console.WriteLine("3. Cautare produs dupa nume");
                Console.WriteLine("4. Efectuare vanzare (scadere stoc)");
                Console.WriteLine("X. Inchidere program");

                Console.Write("Alegeti o optiune: ");
                optiune = Console.ReadLine()?.ToUpper() ?? string.Empty;

                switch (optiune)
                {
                    case "1":
                        Adaugare();
                        break;
                    case "2":
                        List<Produs> produse = admin.GetProduse();
                        Afisare(produse);
                        break;
                    case "3":
                        Cautare();
                        break;
                    case "4":
                        Vanzare();
                        break;
                    case "X":
                        Console.WriteLine("Aplicatia se inchide. La revedere!");
                        break;
                    default:
                        Console.WriteLine("Optiune inexistenta! Incercati din nou.");
                        break;
                }

            } while (optiune != "X");
        }

        public static void Afisare(List<Produs> produse)
        {
            Console.WriteLine("\n--- LISTA PRODUSE DIN FISIER ---");
            if (produse.Count == 0)
            {
                Console.WriteLine("Nu exista produse salvate.");
                return;
            }

            foreach (Produs p in produse)
            {
                Console.WriteLine(p.Info());
            }
        }

        static void Adaugare()
        {
            Console.Write("Introduceti numele: ");
            string n = Console.ReadLine();

            double p;
            Console.Write("Introduceti pretul: ");
            while (!double.TryParse(Console.ReadLine(), out p) || p < 0)
            {
                Console.Write("Eroare! Introduceti un pret valid (numar pozitiv): ");
            }

            int c;
            Console.Write("Introduceti cantitatea: ");
            while (!int.TryParse(Console.ReadLine(), out c) || c < 0)
            {
                Console.Write("Eroare! Introduceti o cantitate valida (numar intreg pozitiv): ");
            }

            Console.WriteLine("Categorii: 1-Aliment, 2-Bautura, 3-Dulciuri, 4-Igiena");
            Console.Write("Alegeti categoria (cifra): ");
            int alegereCategorie;
            while (!int.TryParse(Console.ReadLine(), out alegereCategorie) || !Enum.IsDefined(typeof(CategorieProdus), alegereCategorie))
            {
                Console.Write("Categorie invalida! Alegeti din nou (cifra 1-4): ");
            }
            CategorieProdus cat = (CategorieProdus)alegereCategorie;

            OptiuniProdus opt = OptiuniProdus.Bio | OptiuniProdus.Perisabil;

            admin.AddProdus(new Produs(0, n, cat, p, c, opt));
            Console.WriteLine("Succes: Produsul a fost salvat in fisierul text.");
        }

        static void Cautare()
        {
            Console.Write("Introduceti numele produsului cautat: ");
            string n = Console.ReadLine();

            var p = admin.CautaDupaNume(n); 

            if (p != null)
            {
                Console.WriteLine("Produs gasit: " + p.Info());
            }
            else
            {
                Console.WriteLine("Eroare: Produsul nu a fost gasit.");
            }
        }

        static void Vanzare()
        {
            Console.Write("Nume produs: ");
            string n = Console.ReadLine();

            Console.Write("Cantitate dorita pentru vanzare: ");
            if (!int.TryParse(Console.ReadLine(), out int cant))
            {
                Console.WriteLine("Cantitate invalida!");
                return;
            }

            var p = admin.CautaDupaNume(n);

            if (p != null && p.Cantitate >= cant)
            {
                p.Cantitate = p.Cantitate - cant;
                admin.UpdateProdus(p);

                double valoareTotala = cant * p.Pret;

                Console.WriteLine($"Vandut! Pret per unitate: {p.Pret} RON");
                Console.WriteLine($"Valoare totala a vanzarii: {valoareTotala} RON");
                Console.WriteLine("Stoc actualizat cu succes!");
            }
            else
            {
                Console.WriteLine("Eroare: Stoc insuficient sau produsul nu exista!");
            }
        }
    }
}