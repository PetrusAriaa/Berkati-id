using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Berkati_Frontend.Handler;
using Berkati_Frontend.Themes;

namespace Berkati_Frontend.Pages
{
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
            Partner.GetPartnerData(DataGrid);
            EditPartnerBtn.Background = new SolidColorBrush(ColorTheme.BG_GREY);
            EditPartnerBtn.Foreground = new SolidColorBrush(ColorTheme.FG_GREY);
            DeletePartnerBtn.Background = new SolidColorBrush(ColorTheme.BG_GREY);
            DeletePartnerBtn.Foreground = new SolidColorBrush(ColorTheme.FG_GREY);
            EditPartnerBtn.IsEnabled = false;
            DeletePartnerBtn.IsEnabled = false;
            AddPartnerBtn.Content = "Add";
        }
        private void AddPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AddPartnerBtn.Content.ToString() == "Add")
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
                AddPartnerBtn.Content = "Cancel";
            }
            if (AddPartnerBtn.Content.ToString() == "Cancel")
            {
                NamaTextBox.Clear();
                PenanggungJawabTextBox.Clear();
                TeleponTextBox.Clear();
                EmailTextBox.Clear();
                DataGrid.SelectedItem = null;
                AddPartnerBtn.Content = "Add";
            }
        }
        private void EditPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Simpan Perubahan?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No) return;
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
                MessageBoxResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus partner ini?", "Konfirmasi Penghapusan", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No) return;
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
                EditPartnerBtn.Background = new SolidColorBrush(ColorTheme.SALMON);
                EditPartnerBtn.Foreground = new SolidColorBrush(Colors.White);
                DeletePartnerBtn.Background = new SolidColorBrush(ColorTheme.SALMON);
                DeletePartnerBtn.Foreground = new SolidColorBrush(Colors.White);
                EditPartnerBtn.IsEnabled = true;
                DeletePartnerBtn.IsEnabled = true;
                AddPartnerBtn.Content = "Cancel";

                Partner.PartnerData partner = (Partner.PartnerData)DataGrid.SelectedItem;

                NamaTextBox.Text = partner.Nama;
                PenanggungJawabTextBox.Text = partner.PenanggungJawab;
                TeleponTextBox.Text = partner.Telp;
                EmailTextBox.Text = partner.Email;

                // Mengatur DataGrid ke mode baca saja
                DataGrid.IsReadOnly = true;
            }
            else
            {
                AddPartnerBtn.Content = "Add";
                EditPartnerBtn.Background = new SolidColorBrush(ColorTheme.BG_GREY);
                EditPartnerBtn.Foreground = new SolidColorBrush(ColorTheme.FG_GREY);
                DeletePartnerBtn.Background = new SolidColorBrush(ColorTheme.BG_GREY);
                DeletePartnerBtn.Foreground = new SolidColorBrush(ColorTheme.FG_GREY);
                EditPartnerBtn.IsEnabled = false;
                DeletePartnerBtn.IsEnabled = false;
            }
        }
    }
}
