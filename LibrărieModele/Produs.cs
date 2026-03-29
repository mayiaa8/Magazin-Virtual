using System;

namespace LibrarieModele
{
    public enum CategorieProdus { Aliment = 1, Bautura, Dulciuri, Igiena }

    [Flags]
    public enum OptiuniProdus { Niciuna = 0, Reducere = 1, Bio = 2, Perisabil = 4, Promotie = 8 }

    public class Produs
    {
        private const char SEPARATOR = ';';
        public int Id { get; set; }
        public string Nume { get; set; }
        public CategorieProdus Categorie { get; set; }
        public double Pret { get; set; }
        public int Cantitate { get; set; }
        public OptiuniProdus Optiuni { get; set; }

        public Produs() { }

        public Produs(int id, string nume, CategorieProdus cat, double pret, int cant, OptiuniProdus opt)
        {
            Id = id; Nume = nume; Categorie = cat; Pret = pret; Cantitate = cant; Optiuni = opt;
        }

       
        public Produs(string linieFisier)
        {
            var date = linieFisier.Split(SEPARATOR);
            Id = int.Parse(date[0]);
            Nume = date[1];
            Categorie = (CategorieProdus)Enum.Parse(typeof(CategorieProdus), date[2]);
            Pret = double.Parse(date[3]);
            Cantitate = int.Parse(date[4]);
            Optiuni = (OptiuniProdus)Enum.Parse(typeof(OptiuniProdus), date[5]);
        }

        public string ConversieLaSirPentruFisier()
        {
            return $"{Id}{SEPARATOR}{Nume}{SEPARATOR}{Categorie}{SEPARATOR}{Pret}{SEPARATOR}{Cantitate}{SEPARATOR}{Optiuni}";
        }

        public string Info() => $"ID: {Id} | {Nume} ({Categorie}) | Pret: {Pret} | Stoc: {Cantitate} | Optiuni: {Optiuni}";
    }

    
    public class Angajat
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Functie { get; set; }

        public Angajat(int id, string nume, string functie) { Id = id; Nume = nume; Functie = functie; }

        public Angajat(string linie)
        {
            var date = linie.Split(';');
            Id = int.Parse(date[0]); Nume = date[1]; Functie = date[2];
        }

        public string ConversieLaSirPentruFisier() => $"{Id};{Nume};{Functie}";
        public string Info() => $"ID: {Id} | Angajat: {Nume} | Functie: {Functie}";
    }
}