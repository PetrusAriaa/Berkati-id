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

namespace Berkati_Frontend.Pages
{
    public partial class Page3 : Page
    {
        // Daftar objek untuk menampung data
        private ObservableCollection<UserData> userDataList;
        // Mendeklarasikan variabel untuk menyimpan item yang dipilih
        private UserData selectedUser; 

        public Page3()
        {
            InitializeComponent();
            // Inisialisasi daftar data
            userDataList = new ObservableCollection<UserData>();
            DataGrid.ItemsSource = userDataList;
        }

        private void AddAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            // Mendapatkan nilai dari input
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            // Menambahkan data ke daftar
            userDataList.Add(new UserData { Username = username, Password = password });


            // Reset TextBoxes setelah menambahkan admin
            UsernameTextBox.Clear();
            PasswordTextBox.Clear();
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
                PasswordTextBox.Clear();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                // Mengambil item yang dipilih dari DataGrid
                UserData selectedUser = (UserData)DataGrid.SelectedItem;

                // Menampilkan nilai item yang dipilih di dalam inputan
                UsernameTextBox.Text = selectedUser.Username;
                PasswordTextBox.Text = selectedUser.Password;

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
        public class UserData
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
