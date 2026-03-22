using System.Collections.Generic;
using System.Linq; 
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareProduseMemorie
    {
        private List<Produs> produse;

        public AdministrareProduseMemorie()
        {
            produse = new List<Produs>();
        }

        public void AddProdus(Produs p)
        {
            p.IdProdus = produse.Count + 1;
            produse.Add(p);
        }

        public List<Produs> GetProduse()
        {
            return produse;
        }

        public Produs CautaProdusDupaNume(string nume)
        {
            
            return produse.FirstOrDefault(p => p.Nume.Equals(nume, System.StringComparison.OrdinalIgnoreCase));
        }

        
        public List<Produs> GetProduseDinCategorie(CategorieProdus cat)
        {
            return produse.Where(p => p.Categorie == cat).ToList();
        }
    }
}