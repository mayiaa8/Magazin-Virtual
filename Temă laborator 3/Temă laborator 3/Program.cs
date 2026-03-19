using System;

class Produs
{
    public string nume;
    public string categorie;
    public double pret;
    public int cantitate;

    public void Citire()
    {
        Console.Write("Nume produs: ");
        nume = Console.ReadLine();

        Console.Write("Categorie: ");
        categorie = Console.ReadLine();

        Console.Write("Pret: ");
        pret = Convert.ToDouble(Console.ReadLine());

        Console.Write("Cantitate: ");
        cantitate = Convert.ToInt32(Console.ReadLine());
    }

    public void Afisare()
    {
        Console.WriteLine("Nume: " + nume +
                          " | Categorie: " + categorie +
                          " | Pret: " + pret +
                          " | Cantitate: " + cantitate);
    }
}

class Program
{
    static void Main()
    {
        Produs[] produse = new Produs[100];
        int n = 0;
        int opt;

        do
        {
            Console.WriteLine("\n--- MINIMARKET ---");
            Console.WriteLine("1. Adaugare produs");
            Console.WriteLine("2. Afisare produse");
            Console.WriteLine("3. Cautare produs");
            Console.WriteLine("4. Vanzare produs");
            Console.WriteLine("0. Iesire");

            Console.Write("Alege optiunea: ");
            opt = Convert.ToInt32(Console.ReadLine());

            switch (opt)
            {
                case 1:
                    produse[n] = new Produs();
                    produse[n].Citire();
                    n++;
                    break;

                case 2:
                    for (int i = 0; i < n; i++)
                        produse[i].Afisare();
                    break;

                case 3:
                    Console.Write("Introdu numele produsului: ");
                    string cauta = Console.ReadLine();
                    bool gasit = false;

                    for (int i = 0; i < n; i++)
                    {
                        if (produse[i].nume == cauta)
                        {
                            produse[i].Afisare();
                            gasit = true;
                        }
                    }

                    if (!gasit)
                        Console.WriteLine("Produsul nu exista!");
                    break;

                case 4:
                    Console.Write("Produs dorit: ");
                    string numeProdus = Console.ReadLine();

                    Console.Write("Cantitate dorita: ");
                    int cant = Convert.ToInt32(Console.ReadLine());

                    bool ok = false;

                    for (int i = 0; i < n; i++)
                    {
                        if (produse[i].nume == numeProdus)
                        {
                            ok = true;

                            if (produse[i].cantitate >= cant)
                            {
                                double total = cant * produse[i].pret;
                                produse[i].cantitate -= cant;

                                Console.WriteLine("Vanzare realizata!");
                                Console.WriteLine("Total de plata: " + total);
                            }
                            else
                            {
                                Console.WriteLine("Stoc insuficient!");
                            }
                        }
                    }

                    if (!ok)
                        Console.WriteLine("Produsul nu exista!");
                    break;
            }

        } while (opt != 0);
    }
}