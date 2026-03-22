using System;
using LibrarieModele;
using NivelStocareDate;

namespace MinimarketApp
{
    class Program
    {
        static void Main()
        {
            AdministrareProduseMemorie admin = new AdministrareProduseMemorie();
            string optiune;

            do
            {
                Console.WriteLine("\n1. Adaugare Produs");
                Console.WriteLine("2. Afisare Stoc");
                Console.WriteLine("3. Cautare Produs");
                Console.WriteLine("4. Vanzare Produs");
                Console.WriteLine("X. Iesire");
                optiune = Console.ReadLine()?.ToUpper();

                switch (optiune)
                {
                    case "1":
                        AdaugaProdus(admin);
                        break;
                    case "2":
                        foreach (var p in admin.GetProduse()) Console.WriteLine(p.Info());
                        break;
                    case "3":
                        Console.Write("Nume produs: ");
                        var gasit = admin.CautaProdusDupaNume(Console.ReadLine());
                        Console.WriteLine(gasit != null ? gasit.Info() : "Produsul nu exista!");
                        break;
                    case "4":
                        RealizeazaVanzare(admin);
                        break;
                }
            } while (optiune != "X");
        }

        static void AdaugaProdus(AdministrareProduseMemorie admin)
        {
            Console.Write("Nume: "); string nume = Console.ReadLine();
            Console.Write("Pret: "); double.TryParse(Console.ReadLine(), out double pret);
            Console.Write("Cantitate: "); int.TryParse(Console.ReadLine(), out int cant);

            Produs p = new Produs(0, nume, pret, cant);

            Console.WriteLine("Categorie: 1-Aliment, 2-Bautura, 3-Curatenie");
            int.TryParse(Console.ReadLine(), out int cat);
            p.Categorie = (CategorieProdus)cat;

            Console.WriteLine("Este Bio? (da/nu)");
            if (Console.ReadLine().ToLower() == "da") p.Optiuni |= OptiuniProdus.Bio;

            Console.WriteLine("Este la Promotie? (da/nu)");
            if (Console.ReadLine().ToLower() == "da") p.Optiuni |= OptiuniProdus.Promotie;

            admin.AddProdus(p);
        }

        static void RealizeazaVanzare(AdministrareProduseMemorie admin)
        {
            Console.Write("Numele produsului de vandut: ");
            string nume = Console.ReadLine();
            var p = admin.CautaProdusDupaNume(nume);

            if (p == null) { Console.WriteLine("Produs negasit!"); return; }

            Console.Write($"Cantitate dorita (stoc: {p.Cantitate}): ");
            int.TryParse(Console.ReadLine(), out int cant);

            if (cant <= p.Cantitate)
            {
                p.Cantitate -= cant;
                double total = cant * p.Pret;
                Console.WriteLine($"Vanzare reusita! Total: {total:C}");
            }
            else
            {
                Console.WriteLine("Eroare: Stoc insuficient!");
            }
        }
    }
}