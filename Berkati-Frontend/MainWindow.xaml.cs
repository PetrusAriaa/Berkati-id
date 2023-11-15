using System;
using System.Windows;
using System.Windows.Controls;

namespace Berkati_Frontend
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            navframe.Navigate(new Uri("Pages/Page1.xaml", UriKind.Relative));
        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = sidebar.SelectedItem as NavButton;

            navframe.Navigate(selected.Navlink);

        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.SetEnvironmentVariable("TOKEN", null);
            LoginWindow loginWindow = new();
            loginWindow.Show();
            Close();
        }
    }
}
