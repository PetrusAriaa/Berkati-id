using Berkati_Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Berkati_Frontend.Handler;

namespace Berkati_Frontend.Pages
{
    public partial class Page2 : Page
    {
        private DonaturViewModel _donaturViewModel;

        public Page2()
        {
            InitializeComponent();
            _donaturViewModel = ((App)Application.Current).DonaturViewModel ?? throw new ArgumentNullException(nameof(_donaturViewModel));
            DataContext = _donaturViewModel;
        }

        private void AddListBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Requests.RequestsData> requestList = new();
            Requests.RequestsData r = new()
            {
                Est_jumlah = int.TryParse(JumlahTextBox.Text, out int parsedJumlah) ? parsedJumlah : 0,
                Alamat = AlamatTextBox.Text,
                Tanggal = TanggalDatePicker.SelectedDate ?? DateTime.MinValue,
                Waktu = JamTextBox.Text,
            };
            requestList.Add(r);
            Requests.UserData u = new()
            {
                Nama = NamaTextBox.Text,
                Telp = TeleponTextBox.Text,
                Requests = requestList,
            };

            Requests.CreateRequest(u, AddListBtn);

            NamaTextBox.Clear();
            TeleponTextBox.Clear();
            JumlahTextBox.Clear();
            AlamatTextBox.Clear();
            TanggalDatePicker.SelectedDate = null;
            JamTextBox.Clear();
        }

    }
}
