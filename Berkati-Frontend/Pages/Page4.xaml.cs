using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Newtonsoft.Json;
using System.Net.Http;


namespace Berkati_Frontend.Pages
{
    public partial class Page4 : Page
    {
        // Daftar objek untuk menampung data
        private ObservableCollection<PartnerData> partnerDataList;
        private PartnerData selectedPartner; // Deklarasikan variabel untuk menyimpan item yang dipilih
        private readonly HttpClient _httpClient = new();
        public Page4()
        {
            InitializeComponent();
            GetPartnerData();
        }
        private async void GetPartnerData()
        {
            var apiUrl = "https://localhost:7036/partner";
            try
            {
                HttpResponseMessage res = await _httpClient.GetAsync(apiUrl);
                if (res.IsSuccessStatusCode)
                {
                    var _res = await res.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<PartnerList>(_res);
                    DataGrid.ItemsSource = json?.Data;
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }
        private void AddPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            string nama = NamaTextBox.Text;
            string penanggungJawab = PenanggungJawabTextBox.Text;
            string telepon = TeleponTextBox.Text;
            string email = EmailTextBox.Text;
            
            partnerDataList.Add(new PartnerData { Nama = nama, PenanggungJawab = penanggungJawab, Telp = telepon, Email = email });

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
                selectedPartner = (PartnerData)DataGrid.SelectedItem;

                selectedPartner.Nama = NamaTextBox.Text;
                selectedPartner.PenanggungJawab = PenanggungJawabTextBox.Text;
                selectedPartner.Telp = TeleponTextBox.Text;
                selectedPartner.Email = EmailTextBox.Text;

                // Memperbarui DataGrid
                DataGrid.Items.Refresh();

                // Mengatur nilai TextBoxes ke kosong jika tidak ada item yang dipilih
                NamaTextBox.Clear();
                PenanggungJawabTextBox.Clear();
                TeleponTextBox.Clear();
                EmailTextBox.Clear();

                // Setelah pembaruan, atur fokus ke baris yang baru saja diperbarui
                DataGrid.SelectedItem = null;
                //DataGrid.SelectedItem = selectedPartner;
            }
        }

        private void DeletePartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                PartnerData selectedPartner = (PartnerData)DataGrid.SelectedItem;

                partnerDataList.Remove(selectedPartner);

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
                PartnerData selectedPartner = (PartnerData)DataGrid.SelectedItem;

                NamaTextBox.Text = selectedPartner.Nama;
                PenanggungJawabTextBox.Text = selectedPartner.PenanggungJawab;
                TeleponTextBox.Text = selectedPartner.Telp; // Ubah nilai integer ke string saat menampilkan di TextBox
                EmailTextBox.Text = selectedPartner.Email;

                // Mengatur DataGrid ke mode baca saja
                DataGrid.IsReadOnly = true;
            }
        }

        // Kelas untuk data pengguna
        public class PartnerData
        {
            public Guid? Id { get; set; }
            public string? Nama { get; set; }
            public string? PenanggungJawab { get; set; }
            public string? Telp { get; set; }
            public string? Email { get; set; }
        }
        public class PartnerList
        {
            public List<PartnerData>? Data { get; set; }
        }
    }
}
