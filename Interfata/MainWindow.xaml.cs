using System;
using System.Collections.Generic;
using System.Windows;
using LibrarieModele;
using NivelStocareDate;

namespace Interfata
{
    public partial class MainWindow : Window
    {
        private AdministrareProduse admin = new AdministrareProduse("produse.txt");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IncarcaStocProduse();
        }

        private void IncarcaStocProduse()
        {
            List<Produs> produse = admin.GetProduse();
            dgvProduse.ItemsSource = null;
            dgvProduse.ItemsSource = produse;
        }

        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNume.Text))
            {
                MessageBox.Show("Te rog să completezi numele produsului!", "Câmp obligatoriu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(txtPret.Text, out double pret) || pret < 0)
            {
                MessageBox.Show("Te rog introdu un preț valid și pozitiv!", "Eroare date", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtCantitate.Text, out int cantitate) || cantitate < 0)
            {
                MessageBox.Show("Te rog introdu o cantitate validă (număr întreg)!", "Eroare date", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CategorieProdus categorieSelectata = CategorieProdus.Aliment;

            if (rbBautura.IsChecked == true) categorieSelectata = CategorieProdus.Bautura;
            else if (rbDulciuri.IsChecked == true) categorieSelectata = CategorieProdus.Dulciuri;
            else if (rbIgiena.IsChecked == true) categorieSelectata = CategorieProdus.Igiena;

            // Transmitem silențios opțiunea 'Niciuna' către clasa Produs
            OptiuniProdus optiuni = OptiuniProdus.Niciuna;

            Produs produsNou = new Produs(0, txtNume.Text, categorieSelectata, pret, cantitate, optiuni);
            admin.AddProdus(produsNou);

            MessageBox.Show($"Produsul '{txtNume.Text}' ({categorieSelectata}) a fost salvat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

            txtNume.Clear();
            txtPret.Clear();
            txtCantitate.Clear();
            rbAliment.IsChecked = true;

            IncarcaStocProduse();
        }

        private void btnCauta_Click(object sender, RoutedEventArgs e)
        {
            string deCautat = txtCautaNume.Text.Trim();

            if (string.IsNullOrWhiteSpace(deCautat))
            {
                MessageBox.Show("Te rog scrie un nume pentru a porni căutarea.", "Atenție", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Produs gasit = admin.CautaDupaNume(deCautat);

            if (gasit != null)
            {
                MessageBox.Show($"Produs identificat în stoc!\n\nID: {gasit.Id} | Nume: {gasit.Nume} | Categorie: {gasit.Categorie} | Preț: {gasit.Pret} RON | Stoc: {gasit.Cantitate} buc.", "Rezultat Căutare", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Eroare: Produsul '{deCautat}' nu există în magazin.", "Rezultat Căutare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnVinde_Click(object sender, RoutedEventArgs e)
        {
            if (dgvProduse.SelectedItem is Produs produsSelectat)
            {
                if (!int.TryParse(txtCantitateVanzare.Text, out int cantitateDorita) || cantitateDorita <= 0)
                {
                    MessageBox.Show("Introdu o cantitate validă pentru vânzare!", "Validare", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (produsSelectat.Cantitate >= cantitateDorita)
                {
                    produsSelectat.Cantitate -= cantitateDorita;
                    admin.UpdateProdus(produsSelectat);

                    double valoareTotala = cantitateDorita * produsSelectat.Pret;

                    MessageBox.Show($"🛒 Vânzare încheiată cu succes!\n\n" +
                                    $"Produs: {produsSelectat.Nume}\n" +
                                    $"Bucăți vândute: {cantitateDorita}\n" +
                                    $"Total de încasat: {valoareTotala:F2} RON",
                                    "Bon Fiscal", MessageBoxButton.OK, MessageBoxImage.Information);

                    txtCantitateVanzare.Clear();
                    IncarcaStocProduse();
                }
                else
                {
                    MessageBox.Show($"Stoc insuficient! Mai sunt doar {produsSelectat.Cantitate} bucăți disponibile pentru '{produsSelectat.Nume}'.", "Stoc Indisponibil", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Selectează produsul printr-un click în tabel înainte de a finaliza vânzarea.", "Instrucțiuni", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}