using System;

namespace LibrarieModele
{
    public enum CategorieProdus
    {
        Aliment = 1,
        Bautura = 2,
        Curatenie = 3,
        Diverse = 4
    }

    [Flags]
    public enum OptiuniProdus
    {
        Niciuna = 0,
        Bio = 1,          
        FaraGluten = 2,   
        FaraZahar = 4,   
        Promotie = 8      
    }

    public class Produs
    {
        public int IdProdus { get; set; }
        public string Nume { get; set; }
        public double Pret { get; set; }
        public int Cantitate { get; set; }

        public CategorieProdus Categorie { get; set; }
        public OptiuniProdus Optiuni { get; set; }

        public Produs() { }

        public Produs(int id, string nume, double pret, int cantitate)
        {
            IdProdus = id;
            Nume = nume;
            Pret = pret;
            Cantitate = cantitate;
        }

        public string Info()
        {
            return $"ID: {IdProdus} | {Nume} | Categorie: {Categorie} | Pret: {Pret:C} | Stoc: {Cantitate} | Optiuni: {Optiuni}";
        }
    }
}