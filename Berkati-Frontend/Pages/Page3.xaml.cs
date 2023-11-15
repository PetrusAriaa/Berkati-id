using System.Windows;
using System.Windows.Controls;
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

            if (AddAdminBtn.Content.ToString() == "Add")
            {
                Admin.AdminData admin = new()
                {
                    Username = UsernameTextBox.Text,
                    Password = PasswordTextBox.Text,
                };
                Admin.AddAdmin(admin, DataGrid);

                UsernameTextBox.Clear();
                PasswordTextBox.Clear();
            }
            else if (AddAdminBtn.Content.ToString() == "Cancel")
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
                MessageBoxResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus admin ini?", "Konfirmasi Penghapusan", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No) return;
                Admin.AdminData admin = (Admin.AdminData)DataGrid.SelectedItem;

                Admin.DeleteAdmin(admin, DataGrid);

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
                Admin.AdminData selectedAdmin = (Admin.AdminData)DataGrid.SelectedItem;

                UsernameTextBox.Text = selectedAdmin.Username;
                PasswordTextBox.Text = "****";
                UsernameTextBox.IsEnabled = false;
                PasswordTextBox.IsEnabled = false;

                DataGrid.IsReadOnly = true;
                AddAdminBtn.Content = "Cancel";
            }
            else
            {
                DataGrid.IsReadOnly = false;
                AddAdminBtn.Content = "Add";
            }
        }
     }
}
