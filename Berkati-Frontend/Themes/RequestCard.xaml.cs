using Berkati_Frontend.Handler;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Berkati_Frontend.Themes
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RequestCard : UserControl
    {
        private Requests.ResRequests _requestData = new();
        public RequestCard(Requests.ResRequests data)
        {
            InitializeComponent();
            _requestData = data;
            if(data.Status == "REQUESTED")
            {
                tbStatus.Foreground = new SolidColorBrush(ColorTheme.DARK_BLUE);
            }
            else if (data.Status == "DONE")
            {
                btnDone.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Collapsed;
                tbStatus.Foreground = new SolidColorBrush(ColorTheme.SUCCESS_2);
            }
            
            tbNama.IsEnabled = false;
            tbTelepon.IsEnabled = false;
            tbAlamat.IsEnabled = false;
            tbJumlah.IsEnabled = false;
            dpTanggal.IsEnabled = false;
            tbWaktu.IsEnabled = false;

            tbNama.Text = data.Nama;
            tbTelepon.Text = data.Telp;
            tbAlamat.Text = data.Alamat;
            tbJumlah.Text = data.Est_jumlah.ToString();
            dpTanggal.SelectedDate = data.Tanggal;
            tbWaktu.Text = data.Waktu;
            tbStatus.Text = data.Status;

            btnEdit.Content = "Edit";
            btnDelete.Content = "Delete";
        }
        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(btnEdit.Content.ToString() == "Edit")
            {
                tbAlamat.IsEnabled = true;
                tbJumlah.IsEnabled = true;
                dpTanggal.IsEnabled = true;
                tbWaktu.IsEnabled = true;

                btnEdit.Content = "Save";
                btnDelete.Content = "Cancel";
                
                btnDone.IsEnabled = false;

                btnEdit.Background = new SolidColorBrush(ColorTheme.PRIMARY);
                btnEdit.Foreground = new SolidColorBrush(Colors.White);
                btnDone.Background = new SolidColorBrush(ColorTheme.BG_GREY);
                btnDone.Foreground = new SolidColorBrush(ColorTheme.FG_GREY);
            }
            else if (btnEdit.Content.ToString() == "Save")
            {
                bool isSuccess = false;
                MessageBoxResult result = MessageBox.Show("Simpan Perubahan?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Requests.RequestsData r = new()
                    {
                        Alamat = tbAlamat.Text,
                        Est_jumlah = int.TryParse(tbJumlah.Text, out int parsedJumlah) ? parsedJumlah : 0,
                        Tanggal = dpTanggal.SelectedDate,
                        Waktu = tbWaktu.Text,
                        Status = tbStatus.Text,
                    };
                    isSuccess = await Requests.UpdateRequest(r, _requestData.RequestId, btnEdit);
                };

                if (!isSuccess)
                {
                    tbAlamat.Text = _requestData.Alamat;
                    tbJumlah.Text = _requestData.Est_jumlah.ToString();
                    dpTanggal.SelectedDate = _requestData.Tanggal;
                    tbWaktu.Text = _requestData.Waktu;
                }

                tbAlamat.IsEnabled = false;
                tbJumlah.IsEnabled = false;
                dpTanggal.IsEnabled = false;
                tbWaktu.IsEnabled = false;

                btnEdit.Content = "Edit";
                btnDelete.Content = "Delete";

                btnEdit.Background = new SolidColorBrush(ColorTheme.WARNING_1);
                btnEdit.Foreground = new SolidColorBrush(ColorTheme.BROWN);
                if (tbStatus.Text != "DONE")
                {
                    btnDone.IsEnabled = true;
                }
                btnDone.Background = new SolidColorBrush(ColorTheme.SUCCESS_1);
                btnDone.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (btnDelete.Content.ToString() == "Delete")
            {
                MessageBoxResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus donatur ini?", "Konfirmasi Penghapusan", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No) return;
                Requests.DeleteRequest(_requestData, btnDelete);
            }
            else if (btnDelete.Content.ToString() == "Cancel")
            {
                tbNama.Text = _requestData.Nama;
                tbTelepon.Text = _requestData.Telp;
                tbAlamat.Text = _requestData.Alamat;
                tbJumlah.Text = _requestData.Est_jumlah.ToString();
                dpTanggal.SelectedDate = _requestData.Tanggal;
                tbWaktu.Text = _requestData.Waktu;
                tbStatus.Text = _requestData.Status;

                tbNama.IsEnabled = false;
                tbTelepon.IsEnabled = false;
                tbAlamat.IsEnabled = false;
                tbJumlah.IsEnabled = false;
                dpTanggal.IsEnabled = false;
                tbWaktu.IsEnabled = false;

                btnEdit.Content = "Edit";
                btnDelete.Content = "Delete";

                btnEdit.Background = new SolidColorBrush(ColorTheme.WARNING_1);
                btnEdit.Foreground = new SolidColorBrush(ColorTheme.BROWN);

                if (tbStatus.Text != "DONE")
                {
                    btnDone.IsEnabled = true;
                }
                btnDone.Background = new SolidColorBrush(ColorTheme.SUCCESS_1);
                btnDone.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Selesaikan layanan ini?", "Konfirmasi", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;
            Requests.FinishRequest(_requestData.RequestId, btnEdit, btnDelete, btnDone, tbStatus);
        }
    }
}
