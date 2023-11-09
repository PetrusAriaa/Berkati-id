using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Berkati_Frontend.Handler;

namespace Berkati_Frontend.Pages
{
    public partial class Page4 : Page
    {
        private static Color FG_GREY = (Color)ColorConverter.ConvertFromString("#A2A2A2");
        private static Color BG_GREY = (Color)ColorConverter.ConvertFromString("#D9D9D9");
        private static Color SALMON = (Color)ColorConverter.ConvertFromString("#F78838");
        public Page4()
        {
            InitializeComponent();
            Partner.GetPartnerData(DataGrid);
            EditPartnerBtn.Background = new SolidColorBrush(BG_GREY);
            EditPartnerBtn.Foreground = new SolidColorBrush(FG_GREY);
            DeletePartnerBtn.Background = new SolidColorBrush(BG_GREY);
            DeletePartnerBtn.Foreground = new SolidColorBrush(FG_GREY);
            EditPartnerBtn.IsEnabled = false;
            DeletePartnerBtn.IsEnabled = false;
            AddPartnerBtn.Content = "Add";
        }
        private void AddPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AddPartnerBtn.Content == "Add")
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
            if (AddPartnerBtn.Content == "Cancel")
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
                EditPartnerBtn.Background = new SolidColorBrush(SALMON);
                EditPartnerBtn.Foreground = new SolidColorBrush(Colors.White);
                DeletePartnerBtn.Background = new SolidColorBrush(SALMON);
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
                EditPartnerBtn.Background = new SolidColorBrush(BG_GREY);
                EditPartnerBtn.Foreground = new SolidColorBrush(FG_GREY);
                DeletePartnerBtn.Background = new SolidColorBrush(BG_GREY);
                DeletePartnerBtn.Foreground = new SolidColorBrush(FG_GREY);
                EditPartnerBtn.IsEnabled = false;
                DeletePartnerBtn.IsEnabled = false;
            }
        }
    }
}
