using Berkati_Frontend.ViewModels;
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
    public partial class Page1 : Page
    {
        private DonaturViewModel _donaturViewModel;
        public Page1()
        {
            InitializeComponent();
            _donaturViewModel = ((App)Application.Current).DonaturViewModel;
            // Memastikan _donaturViewModel tidak null sebelum mengakses DonaturList
            DonaturItemsControl.ItemsSource = _donaturViewModel?.DonaturList ?? new ObservableCollection<Donatur>();
            DataContext = new DonaturViewModel();

        }
        private void EditListButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            if (editButton != null)
            {
                Donatur selectedDonatur = editButton.DataContext as Donatur;
                if (selectedDonatur != null && selectedDonatur.EditCommand != null)
                {
                    // Set variabel HasChanges saat ada nilai yang diedit
                    _donaturViewModel.HasChanges = true;

                    // Memanggil perintah Edit di dalam Donatur
                    selectedDonatur.EditCommand.Execute(_donaturViewModel);
                }
            }
        }

        private void DeleteListButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            if (deleteButton != null)
            {
                Donatur selectedDonatur = deleteButton.DataContext as Donatur;
                if (selectedDonatur != null)
                {
                    MessageBoxResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus donatur ini?", "Konfirmasi Penghapusan", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        //Menghapus objek Donatur dari koleksi
                        _donaturViewModel.DonaturList.Remove(selectedDonatur);
                    }
                }
            }
        }
    }
}
