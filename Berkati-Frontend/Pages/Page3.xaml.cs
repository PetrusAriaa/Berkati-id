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
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
            Admin.GetAdminData(DataGrid);
            AddAdminBtn.Content = "Add";
        }
        
        private void AddAdminBtn_Click(object sender, RoutedEventArgs e)
        {

            if (AddAdminBtn.Content == "Add")
            {
                Admin.AdminData admin = new()
                {
                    Username = UsernameTextBox.Text,
                    Password = PasswordTextBox.Text,
                };
                Admin.AddAdmin(admin, DataGrid);

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
        
        
        private void DeleteAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                // Mengambil item yang dipilih dari DataGrid
                Admin.AdminData admin = (Admin.AdminData)DataGrid.SelectedItem;

                // Menghapus item yang dipilih dari daftar
                Admin.DeleteAdmin(admin, DataGrid);

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
                Admin.AdminData selectedAdmin = (Admin.AdminData)DataGrid.SelectedItem;

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
    }
}
