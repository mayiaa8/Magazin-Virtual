using System;
using System.Collections.Generic;
using System.Text;

namespace Temă_lab_2
{
    internal class Produs
    {
        public string nume;
        public string categorie;
        public double pret;
        public int cantitate;

        public void Afisare()
        {
            Console.WriteLine("Nume: " + nume);
            Console.WriteLine("Categorie: " + categorie);
            Console.WriteLine("Pret: " + pret);
            Console.WriteLine("Cantitate: " + cantitate);
        }
    }
}
