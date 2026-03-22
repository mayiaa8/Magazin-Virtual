using System;

namespace Temă_lab_2
{
    class Program
    {
        static void Main()
        {
            Produs p = new Produs();

            p.nume = "Lapte";
            p.categorie = "Aliment";
            p.pret = 7.5;
            p.cantitate = 20;

            p.Afisare();
        }
    }
}