using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkati_Frontend.ViewModels
{
    public class DonaturViewModel
    {
        public ObservableCollection<Donatur> DonaturList { get; set; }

        public DonaturViewModel()
        {
            DonaturList = new ObservableCollection<Donatur>();
        }
    }

    public class Donatur
    {
        public string? Nama { get; set; }
        public string? Jenis { get; set; }
        public int Jumlah { get; set; }
        public string? Satuan { get; set; }
        public string? Alamat { get; set; }
        public DateTime Tanggal { get; set; }
        public string? Jam { get; set; }
    }
}
