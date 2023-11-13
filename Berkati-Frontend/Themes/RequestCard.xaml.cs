using Berkati_Frontend.Handler;
using System;
using System.Collections.Generic;
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

namespace Berkati_Frontend.Themes
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RequestCard : UserControl
    {
        public RequestCard(Requests.ResRequests data)
        {
            
        InitializeComponent();
            if(data.Status == "REQUESTED")
            {
                tbStatus.Foreground = new SolidColorBrush(ColorTheme.DARK_BLUE);
            }
            else if (data.Status == "DONE")
            {
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
        }
    }
}
