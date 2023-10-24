using Berkati_Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Berkati_Frontend.Pages
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private DonaturViewModel _donaturViewModel;

        public Page2()
        {
            InitializeComponent();
            _donaturViewModel = ((App)Application.Current).DonaturViewModel;
            DataContext = _donaturViewModel;
        }

        private void AddListBtn_Click(object sender, RoutedEventArgs e)
        {
            // Membaca data dari TextBoxes
            string nama = NamaTextBox.Text;
            string jenis = JenisTextBox.Text;
            int jumlah = int.TryParse(JumlahTextBox.Text, out int parsedJumlah) ? parsedJumlah : 0;
            string satuan = SatuanTextBox.Text; // Mendapatkan satuan dari TextBox
            string alamat = AlamatTextBox.Text;
            DateTime tanggal = TanggalDatePicker.SelectedDate ?? DateTime.MinValue; // Mendapatkan tanggal dari DatePicker
            string jam = JamTextBox.Text; // Mendapatkan jam dari TextBox

            // Menambahkan donatur baru ke koleksi donatur
            _donaturViewModel.DonaturList.Add(new Donatur
            {
                Nama = nama,
                Jenis = jenis,
                Jumlah = jumlah,
                Satuan = satuan, // Menyimpan satuan
                Alamat = alamat,
                Tanggal = tanggal, // Menyimpan tanggal
                Jam = jam // Menyimpan jam
            });

            // Reset TextBoxes setelah menambahkan donatur
            NamaTextBox.Clear();
            JenisTextBox.Clear();
            JumlahTextBox.Clear();
            SatuanTextBox.Clear();
            AlamatTextBox.Clear();
            TanggalDatePicker.SelectedDate = null;
            JamTextBox.Clear();
        }

    }
}
