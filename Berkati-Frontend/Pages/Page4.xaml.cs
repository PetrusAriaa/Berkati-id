using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Berkati_Frontend.Handler;

namespace Berkati_Frontend.Pages
{
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
            Partner.GetPartnerData(DataGrid);
        }
        private void AddPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            Partner.PartnerData partner = new()
            {
                Nama = NamaTextBox.Text,
                PenanggungJawab = PenanggungJawabTextBox.Text,
                Telp = TeleponTextBox.Text,
                Email = EmailTextBox.Text,
            };
            Partner.AddPartner(partner, DataGrid);

            // Reset TextBoxes setelah menambahkan data
            NamaTextBox.Clear();
            PenanggungJawabTextBox.Clear();
            TeleponTextBox.Clear();
            EmailTextBox.Clear();
        }
        private void EditPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Partner.PartnerData partner = (Partner.PartnerData)DataGrid.SelectedItem;

                partner.Nama = NamaTextBox.Text;
                partner.PenanggungJawab = PenanggungJawabTextBox.Text;
                partner.Telp = TeleponTextBox.Text;
                partner.Email = EmailTextBox.Text;

                Partner.EditPartner(partner, DataGrid);

                // Mengatur nilai TextBoxes ke kosong jika tidak ada item yang dipilih
                NamaTextBox.Clear();
                PenanggungJawabTextBox.Clear();
                TeleponTextBox.Clear();
                EmailTextBox.Clear();

                // Setelah pembaruan, atur fokus ke baris yang baru saja diperbarui
                DataGrid.SelectedItem = null;
            }
        }

        private void DeletePartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Partner.PartnerData partner = (Partner.PartnerData)DataGrid.SelectedItem;

                Partner.DeletePartner(partner, DataGrid);

                // Reset TextBoxes setelah menghapus data
                NamaTextBox.Clear();
                PenanggungJawabTextBox.Clear();
                TeleponTextBox.Clear();
                EmailTextBox.Clear();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                Partner.PartnerData partner = (Partner.PartnerData)DataGrid.SelectedItem;

                NamaTextBox.Text = partner.Nama;
                PenanggungJawabTextBox.Text = partner.PenanggungJawab;
                TeleponTextBox.Text = partner.Telp;
                EmailTextBox.Text = partner.Email;

                // Mengatur DataGrid ke mode baca saja
                DataGrid.IsReadOnly = true;
            }
        }
    }
}
