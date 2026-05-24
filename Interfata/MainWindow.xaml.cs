using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LibrarieModele;
using NivelStocareDate;

namespace Interfata
{
    public partial class MainWindow : Window
    {
        private AdministrareProduse admin = new AdministrareProduse("produse.txt");
        private Produs produsDeEditat = null;

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
            dgvVanzare.ItemsSource = null;
            dgvVanzare.ItemsSource = produse;
            dgvStoc.ItemsSource = null;
            dgvStoc.ItemsSource = produse;
        }

        private void ArataMesaj(TextBlock blocText, string mesaj, bool eSucces)
        {
            blocText.Text = mesaj;
            blocText.Foreground = eSucces ? new SolidColorBrush(Color.FromRgb(34, 197, 94)) : new SolidColorBrush(Color.FromRgb(239, 68, 68));
        }

        private void CurataMesaje()
        {
            txtStatusAdaugare.Text = string.Empty;
            txtStatusVanzare.Text = string.Empty;
            txtStatusStoc.Text = string.Empty;
            txtStatusEditare.Text = string.Empty;

            // Prevenim erori de initializare prin verificarea obiectelor
            if (txtCautaVanzare != null) txtCautaVanzare.Text = string.Empty;
            if (txtCautaStoc != null) txtCautaStoc.Text = string.Empty;
        }

        private void AscundeTot()
        {
            MeniuPrincipal.Visibility = Visibility.Collapsed;
            PaginaAdaugare.Visibility = Visibility.Collapsed;
            PaginaVanzare.Visibility = Visibility.Collapsed;
            PaginaStoc.Visibility = Visibility.Collapsed;
            panouEditare.Visibility = Visibility.Collapsed;
            CurataMesaje();
        }

        private void Nav_Adaugare_Click(object sender, RoutedEventArgs e)
        {
            AscundeTot();
            PaginaAdaugare.Visibility = Visibility.Visible;
        }

        private void Nav_Vanzare_Click(object sender, RoutedEventArgs e)
        {
            AscundeTot();
            IncarcaStocProduse();
            PaginaVanzare.Visibility = Visibility.Visible;
        }

        private void Nav_Stoc_Click(object sender, RoutedEventArgs e)
        {
            AscundeTot();
            IncarcaStocProduse();
            PaginaStoc.Visibility = Visibility.Visible;
        }

        private void Nav_Inapoi_Click(object sender, RoutedEventArgs e)
        {
            AscundeTot();
            MeniuPrincipal.Visibility = Visibility.Visible;
        }

        // --- FILTRARE / CAUTARE IN TIMP REAL LA VANZARE ---
        private void txtCautaVanzare_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (admin == null || dgvVanzare == null) return;

            string textCautat = txtCautaVanzare.Text.Trim();
            List<Produs> produse = admin.GetProduse();

            if (!string.IsNullOrWhiteSpace(textCautat))
            {
                produse = produse.FindAll(p => p.Nume.Contains(textCautat, StringComparison.OrdinalIgnoreCase));
            }

            dgvVanzare.ItemsSource = null;
            dgvVanzare.ItemsSource = produse;
        }

        // --- FILTRARE / CAUTARE IN TIMP REAL LA STOC ---
        private void txtCautaStoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (admin == null || dgvStoc == null) return;

            string textCautat = txtCautaStoc.Text.Trim();
            List<Produs> produse = admin.GetProduse();

            if (!string.IsNullOrWhiteSpace(textCautat))
            {
                produse = produse.FindAll(p => p.Nume.Contains(textCautat, StringComparison.OrdinalIgnoreCase));
            }

            dgvStoc.ItemsSource = null;
            dgvStoc.ItemsSource = produse;
        }

        // --- ADAUGARE ---
        private void btnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNume.Text)) { ArataMesaj(txtStatusAdaugare, "Introduceți un nume valid!", false); return; }
            if (!double.TryParse(txtPret.Text, out double pret) || pret < 0) { ArataMesaj(txtStatusAdaugare, "Prețul este invalid!", false); return; }
            if (!int.TryParse(txtCantitate.Text, out int cantitate) || cantitate < 0) { ArataMesaj(txtStatusAdaugare, "Cantitatea este invalidă!", false); return; }

            CategorieProdus cat = CategorieProdus.Aliment;
            if (rbBautura.IsChecked == true) cat = CategorieProdus.Bautura;
            else if (rbDulciuri.IsChecked == true) cat = CategorieProdus.Dulciuri;
            else if (rbIgiena.IsChecked == true) cat = CategorieProdus.Igiena;

            admin.AddProdus(new Produs(0, txtNume.Text, cat, pret, cantitate, OptiuniProdus.Niciuna));

            ArataMesaj(txtStatusAdaugare, $"Produsul '{txtNume.Text}' a fost adăugat cu succes!", true);

            txtNume.Clear(); txtPret.Clear(); txtCantitate.Clear();
            rbAliment.IsChecked = true;
        }

        // --- VANZARE ---
        private void btnVinde_Click(object sender, RoutedEventArgs e)
        {
            if (dgvVanzare.SelectedItem is Produs p)
            {
                if (!int.TryParse(txtCantitateVanzare.Text, out int cant) || cant <= 0)
                {
                    ArataMesaj(txtStatusVanzare, "Cantitate invalidă!", false);
                    return;
                }

                if (p.Cantitate >= cant)
                {
                    p.Cantitate -= cant;
                    admin.UpdateProdus(p);

                    ArataMesaj(txtStatusVanzare, $"Ai vândut {cant}x {p.Nume} la {p.Pret} RON bucata.", true);

                    txtCantitateVanzare.Clear();
                    IncarcaStocProduse();
                }
                else ArataMesaj(txtStatusVanzare, "Stoc insuficient!", false);
            }
            else ArataMesaj(txtStatusVanzare, "Selectează un produs din tabel!", false);
        }

        // --- EDITARE ---
        private void btnIncepeEditare_Click(object sender, RoutedEventArgs e)
        {
            if (dgvStoc.SelectedItem is Produs p)
            {
                produsDeEditat = p;
                txtEditNume.Text = p.Nume;
                txtEditPret.Text = p.Pret.ToString();
                txtEditCantitate.Text = p.Cantitate.ToString();
                panouEditare.Visibility = Visibility.Visible;
                txtStatusStoc.Text = string.Empty;
            }
            else
            {
                ArataMesaj(txtStatusStoc, "Alege un produs din tabel pentru a-l edita.", false);
            }
        }

        private void btnSalveazaEdit_Click(object sender, RoutedEventArgs e)
        {
            if (produsDeEditat != null)
            {
                if (!double.TryParse(txtEditPret.Text, out double pNou) || !int.TryParse(txtEditCantitate.Text, out int cNou))
                {
                    ArataMesaj(txtStatusEditare, "Valori numerice invalide!", false);
                    return;
                }

                produsDeEditat.Nume = txtEditNume.Text;
                produsDeEditat.Pret = pNou;
                produsDeEditat.Cantitate = cNou;

                admin.UpdateProdus(produsDeEditat);

                ArataMesaj(txtStatusStoc, "Modificările au fost salvate cu succes!", true);
                panouEditare.Visibility = Visibility.Collapsed;
                IncarcaStocProduse();
            }
        }
    }
}