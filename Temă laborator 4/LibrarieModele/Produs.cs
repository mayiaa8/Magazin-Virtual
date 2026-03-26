using System;

namespace LibrarieModele
{
    
    public enum CategorieProdus { Aliment = 1, Bautura = 2, Dulciuri = 3, Igiena = 4 }

    
    [Flags]
    public enum OptiuniProdus { Niciuna = 0, Reducere = 1, Bio = 2, Perisabil = 4, Promotie = 8 }

    public class Produs
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public CategorieProdus Categorie { get; set; }
        public double Pret { get; set; }
        public int Cantitate { get; set; }
        public OptiuniProdus Optiuni { get; set; }

        public Produs() { }

        public Produs(int id, string nume, CategorieProdus categorie, double pret, int cantitate, OptiuniProdus optiuni)
        {
            Id = id; Nume = nume; Categorie = categorie; Pret = pret; Cantitate = cantitate; Optiuni = optiuni;
        }

        public string Info()
        {
            return $"ID: {Id} | {Nume} ({Categorie}) | Pret: {Pret} RON | Stoc: {Cantitate} | Info: {Optiuni}";
        }
    }
}