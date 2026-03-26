using System.Collections.Generic;
using System.Linq; 
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareProduse
    {
        private List<Produs> produse = new List<Produs>();

        public void AddProdus(Produs p)
        {
            p.Id = produse.Count > 0 ? produse.Max(pr => pr.Id) + 1 : 1;
            produse.Add(p);
        }

        public List<Produs> GetProduse() => produse;

        public Produs CautaProdus(string nume)
        {
            return produse.FirstOrDefault(p => p.Nume.Equals(nume, System.StringComparison.OrdinalIgnoreCase));
        }

        public string Vinde(string nume, int cantitateVanduta)
        {
            var p = CautaDupaNume(nume);

            if (p == null) return "Produs inexistent!";
            if (p.Cantitate < cantitateVanduta) return "Stoc insuficient!";

            p.Cantitate -= cantitateVanduta;
           
            UpdateProdus(p);

            return $"Vanzare reusita! Pret produs: {p.Pret} RON.";
        }
    }
}