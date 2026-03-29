using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareProduse
    {
        private string numeFisier;

        public AdministrareProduse(string numeFisier)
        {
            this.numeFisier = numeFisier;
           
            File.Open(numeFisier, FileMode.OpenOrCreate).Close();
        }

        public void AddProdus(Produs p)
        {
            p.Id = GetNextId();
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(p.ConversieLaSirPentruFisier());
            }
        }

        public List<Produs> GetProduse()
        {
            List<Produs> lista = new List<Produs>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(linie))
                        continue;

                    lista.Add(new Produs(linie));
                }
            }
            return lista;
        }

        public Produs CautaDupaNume(string nume)
        {
            return GetProduse().FirstOrDefault(p => p.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase));
        }

        public bool UpdateProdus(Produs pActualizat)
        {
            var lista = GetProduse();
            bool gasit = false;
            using (StreamWriter sw = new StreamWriter(numeFisier, false)) 
            {
                foreach (var p in lista)
                {
                    if (p.Id == pActualizat.Id)
                    {
                        sw.WriteLine(pActualizat.ConversieLaSirPentruFisier());
                        gasit = true;
                    }
                    else sw.WriteLine(p.ConversieLaSirPentruFisier());
                }
            }
            return gasit;
        }

        private int GetNextId() => GetProduse().Count > 0 ? GetProduse().Max(p => p.Id) + 1 : 1;
    }
}