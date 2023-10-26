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
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        // Daftar objek untuk menampung data
        private ObservableCollection<UserData> userDataList;
        private UserData selectedUser; // Deklarasikan variabel untuk menyimpan item yang dipilih
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

        private void AddAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            //// Mendapatkan nilai dari input
            //string username = UsernameTextBox.Text;
            //string password = PasswordBox.Password;

            //// Menambahkan data ke daftar
            //userDataList.Add(new UserData { Username = username, Password = password });


            //// Reset TextBoxes setelah menambahkan admin
            //UsernameTextBox.Clear();
            //PasswordBox.Clear();
        }

        private void DeleteAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                // Mengambil item yang dipilih dari DataGrid
                UserData selectedUser = (UserData)DataGrid.SelectedItem;

                // Menghapus item yang dipilih dari daftar
                userDataList.Remove(selectedUser);

                // Reset TextBoxes setelah menghapus admin
                UsernameTextBox.Clear();
                PasswordBox.Clear();
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
                PasswordBox.Password = "****";
                UsernameTextBox.IsEnabled = false;
                PasswordBox.IsEnabled = false;

                // Mengatur DataGrid ke mode baca saja
                DataGrid.IsReadOnly = true;
            }
            else
            {
                // Mengatur DataGrid ke mode aktif
                DataGrid.IsReadOnly = false;
            }
        }

        // Kelas untuk data pengguna
        public class Admin
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string IsSuperUser { get; set; }
            public DateTime LastLogin { get; set; }
        }
        public class UserData
        {
            public List<Admin> Data { get; set; }
        }
    }
}
