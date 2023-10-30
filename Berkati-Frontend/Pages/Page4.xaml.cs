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
        private async void AddPartner(PartnerData partner)
        {
            var apiUrl = "https://localhost:7036/partner";
            string body = "{\"nama\":\"" + partner.Nama + "\",\"penanggungJawab\":\"" + partner.PenanggungJawab + "\",\"telp\":\"" + partner.Telp + "\",\"email\":\"" + partner.Email + "\"}";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PostAsync(apiUrl, content);
                if (!res.IsSuccessStatusCode )
                {
                    var _res = await res.Content.ReadAsStringAsync();
                    MessageBox.Show("Gagal menambahkan admin", _res);
                    return;
                }
                MessageBox.Show("Berhasil menambahkan partner");
                GetPartnerData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            PartnerData partner = new()
            {
                Nama = NamaTextBox.Text,
                PenanggungJawab = PenanggungJawabTextBox.Text,
                Telp = TeleponTextBox.Text,
                Email = EmailTextBox.Text,
            };
            AddPartner(partner);

            // Reset TextBoxes setelah menambahkan data
            NamaTextBox.Clear();
            PenanggungJawabTextBox.Clear();
            TeleponTextBox.Clear();
            EmailTextBox.Clear();
        }

        private async void DeletePartner(PartnerData partner)
        {
            var apiUri = "https://localhost:7036/partner/" + partner.Id;
            try
            {
                HttpResponseMessage res = await _httpClient.DeleteAsync(apiUri);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Berhasil menghapus partner");
                    GetPartnerData();
                }
                else
                {
                    MessageBox.Show("Gagal menghapus partner");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void EditPartner (PartnerData partner)
        {
            var apiUrl = "https://localhost:7036/partner/" + partner.Id;
            string body = "{\"nama\":\"" + partner.Nama + "\",\"penanggungJawab\":\"" + partner.PenanggungJawab + "\",\"telp\":\"" + partner.Telp + "\",\"email\":\"" + partner.Email + "\"}";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PutAsync(apiUrl, content);
                if (!res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Gagal mengedit partner");
                    return;
                }
                MessageBox.Show("Berhasil mengedit partner");
                GetPartnerData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EditPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                PartnerData partner = (PartnerData)DataGrid.SelectedItem;

                partner.Nama = NamaTextBox.Text;
                partner.PenanggungJawab = PenanggungJawabTextBox.Text;
                partner.Telp = TeleponTextBox.Text;
                partner.Email = EmailTextBox.Text;

                EditPartner(partner);

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
                PartnerData partner = (PartnerData)DataGrid.SelectedItem;

                DeletePartner(partner);

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
                PartnerData partner = (PartnerData)DataGrid.SelectedItem;

                NamaTextBox.Text = partner.Nama;
                PenanggungJawabTextBox.Text = partner.PenanggungJawab;
                TeleponTextBox.Text = partner.Telp; // Ubah nilai integer ke string saat menampilkan di TextBox
                EmailTextBox.Text = partner.Email;

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
