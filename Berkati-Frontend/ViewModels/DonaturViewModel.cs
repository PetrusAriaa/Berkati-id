using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace Berkati_Frontend.ViewModels
{
    public class DonaturViewModel
    {
        public ObservableCollection<Donatur> DonaturList { get; set; }

        private bool _hasChanges;

        public DonaturViewModel()
        {
            DonaturList = new ObservableCollection<Donatur>();
            _hasChanges = false;
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set { _hasChanges = value; }
        }
    }

    public class Donatur
    {
        private string _nama;
        private string _telepon;
        private int _jumlah;
        private string _alamat;
        private DateTime _tanggal;
        private string _jam;

        public string Nama
        {
            get { return _nama; }
            set { _nama = value; }
        }

        public string Telepon
        {
            get { return _telepon; }
            set { _telepon = value; }
        }

        public int Jumlah
        {
            get { return _jumlah; }
            set { _jumlah = value; }
        }

        public string Alamat
        {
            get { return _alamat; }
            set { _alamat = value; }
        }

        public DateTime Tanggal
        {
            get { return _tanggal; }
            set { _tanggal = value; }
        }

        public string Jam
        {
            get { return _jam; }
            set { _jam = value; }
        }

        // Perintah untuk tombol Edit
        public ICommand EditCommand { get; }

        // Perintah untuk tombol Delete
        public ICommand DeleteCommand { get; set; }

        public Donatur()
        {
            EditCommand = new RelayCommand(Edit, CanEdit);
            DeleteCommand = new RelayCommand(Delete);
        }

        private void Edit(object parameter)
        {
            DonaturViewModel viewModel = parameter as DonaturViewModel;
            if (viewModel != null)
            {
                if (!CanEdit(null))
                {
                    MessageBox.Show("Tidak ada perubahan yang disimpan.");
                }
                else
                {
                    // Logika untuk tindakan Edit
                    // Anda dapat menambahkan kode di sini untuk menangani tindakan edit pada Donatur tertentu
                    MessageBox.Show("Data berhasil diedit!");

                    viewModel.HasChanges = false; // Reset HasChanges setelah perubahan disimpan
                    InitializeOriginalValues(); // Panggil InitializeOriginalValues di sini
                }
            }
        }

        private bool CanEdit(object parameter)
        {
            // Tentukan apakah data telah berubah
            InitializeOriginalValues();
            return _nama != Nama || _telepon != Telepon || _jumlah != Jumlah ||
                       _alamat != Alamat || _tanggal != Tanggal || _jam != Jam;
        }

        private void InitializeOriginalValues()
        {
            // Inisialisasi variabel data asli
        }

        private void Delete(object parameter)
        {
            DonaturViewModel viewModel = parameter as DonaturViewModel;
            viewModel?.DonaturList.Remove(this);
            // Logika untuk tindakan Delete
            // Anda dapat menambahkan kode di sini untuk menangani tindakan delete pada Donatur tertentu
            MessageBox.Show("Data berhasil dihapus!");
        }
    }

    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}