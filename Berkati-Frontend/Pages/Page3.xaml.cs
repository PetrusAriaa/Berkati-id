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
using System.Net.Http;
using Newtonsoft.Json;

namespace Berkati_Frontend.Pages
{
    public partial class Page3 : Page
    {
        private readonly HttpClient _httpClient = new();
        public Page3()
        {
            InitializeComponent();
            GetAdminData();
        }
        private async void GetAdminData()
        {
            var apiUri = "https://localhost:7036/admin";
            try
            {
                HttpResponseMessage res = await _httpClient.GetAsync(apiUri);
                if (res.IsSuccessStatusCode)
                {
                    var content = await res.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<UserData>(content);
                    DataGrid.ItemsSource = json.Data;
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void AddAdmin(Admin admin)
        {
            var apiUri = "https://localhost:7036/admin";
            string body = "{\"username\":\"" + admin.Username + "\",\"password\":\"" + admin.Password+ "\"}";
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage res = await _httpClient.PostAsync(apiUri, content);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Berhasil menambahkan admin");
                    GetAdminData();
                }
                else
                {
                    MessageBox.Show("Gagal menambahkan admin");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        private void AddAdminBtn_Click(object sender, RoutedEventArgs e)
        {

            if (AddAdminBtn.Content == "Add")
            {
                Admin admin = new()
                {
                    Username = UsernameTextBox.Text,
                    Password = PasswordTextBox.Text,
                };
                AddAdmin(admin);

                // Reset TextBoxes setelah menambahkan admin
                UsernameTextBox.Clear();
                PasswordTextBox.Clear();
            }
            else if (AddAdminBtn.Content == "Cancel")
            {
                UsernameTextBox.IsEnabled = true;
                PasswordTextBox.IsEnabled = true;
                UsernameTextBox.Clear();
                PasswordTextBox.Clear();
                AddAdminBtn.Content = "Add";
            }
        }
        
        private async void DeleteAdmin(Admin admin)
        {
            var apiUri = "https://localhost:7036/admin/"+admin.Id;
            try
            {
                HttpResponseMessage res = await _httpClient.DeleteAsync(apiUri);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Berhasil menghapus admin");
                    GetAdminData();
                }
                else
                {
                    MessageBox.Show("Gagal menghapus admin");
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
        }
        
        private void DeleteAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                // Mengambil item yang dipilih dari DataGrid
                Admin admin = (Admin)DataGrid.SelectedItem;

                // Menghapus item yang dipilih dari daftar
                DeleteAdmin(admin);

                // Reset TextBoxes setelah menghapus admin
                UsernameTextBox.Clear();
                PasswordTextBox.Clear();
                UsernameTextBox.IsEnabled = true;
                PasswordTextBox.IsEnabled = true;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                // Mengambil item yang dipilih dari DataGrid
                Admin selectedAdmin = (Admin)DataGrid.SelectedItem;

                // Menampilkan nilai item yang dipilih di dalam inputan
                UsernameTextBox.Text = selectedAdmin.Username;
                PasswordTextBox.Text = "****";
                UsernameTextBox.IsEnabled = false;
                PasswordTextBox.IsEnabled = false;
                // Mengatur DataGrid ke mode baca saja
                DataGrid.IsReadOnly = true;
                AddAdminBtn.Content = "Cancel";
            }
            else
            {
                // Mengatur DataGrid ke mode aktif
                DataGrid.IsReadOnly = false;
                AddAdminBtn.Content = "Add";
            }
        }

        // Kelas untuk data pengguna
        public class Admin
        {
            public Guid? Id { get; set; }
            public string? Username { get; set; }
            public string? Password { get; set; }
            public string? IsSuperUser { get; set; }
            public DateTime? LastLogin { get; set; }
        }
        public class UserData
        {
            public List<Admin>? Data { get; set; }
        }
    }
}
