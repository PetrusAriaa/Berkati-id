using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Diagnostics;

namespace Berkati_Frontend.ViewModels
{
    // Kelas DonaturViewModel untuk mengelola daftar donatur.
    public class DonaturViewModel
    {
        // Daftar observabel donatur untuk ditampilkan di antarmuka pengguna.
        public ObservableCollection<Donatur> DonaturList { get; set; }

        private bool _hasChanges;

        public DonaturViewModel()
        {
            DonaturList = new ObservableCollection<Donatur>();
            _hasChanges = false;
        }

        // Properti untuk menandai apakah ada perubahan pada data donatur.
        public bool HasChanges
        {
            get { return _hasChanges; }
            set { _hasChanges = value; }
        }
    }

    // Kelas Donatur merepresentasikan entitas donatur dengan properti-properti tertentu.
    public class Donatur : INotifyPropertyChanged
    {
        private string? _nama;
        private string? _telepon;
        private int _jumlah;
        private string? _alamat;
        private DateTime _tanggal;
        private string? _jam;

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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Properti-propertri asli untuk menyimpan nilai awal sebelum diedit.
        private string _originalNama;
        private string _originalTelepon;
        private int _originalJumlah;
        private string _originalAlamat;
        private DateTime _originalTanggal;
        private string _originalJam;

        // Command untuk tombol Edit dengan implementasi ICommand.
        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get { return _editCommand; }
            set
            {
                if (_editCommand != value)
                {
                    _editCommand = value;
                    OnPropertyChanged(nameof(EditCommand));
                }
            }
        }

        // Command untuk tombol Delete dengan implementasi ICommand.
        public ICommand DeleteCommand { get; private set; }

        // Donatur yang dipilih oleh pengguna.
        private Donatur _selectedDonatur;
        public Donatur SelectedDonatur
        {
            get { return _selectedDonatur; }
            set
            {
                _selectedDonatur = value;
                OnPropertyChanged(nameof(SelectedDonatur));
            }
        }

        // Konstruktor untuk inisialisasi nilai awal.
        public Donatur()
        {
            // Inisialisasi nilai awal.
            _originalNama = Nama;
            _originalTelepon = Telepon;
            _originalJumlah = Jumlah;
            _originalAlamat = Alamat;
            _originalTanggal = Tanggal;
            _originalJam = Jam;

            // Mengaitkan tombol Edit dengan metode Edit dan CanEdit.
            _editCommand = new RelayCommand(Edit, CanEdit);
            // Mengaitkan tombol Delete dengan metode Delete.
            DeleteCommand = new RelayCommand(Delete);
        }

        // Metode untuk menginisialisasi nilai awal setelah pengeditan.
        private void InitializeOriginalValues()
        {
            _originalNama = Nama;
            _originalTelepon = Telepon;
            _originalJumlah = Jumlah;
            _originalAlamat = Alamat;
            _originalTanggal = Tanggal;
            _originalJam = Jam;
        }

        // Metode untuk menentukan apakah data donatur dapat diedit.
        public bool CanEdit(object parameter)
        {
            return _originalNama != Nama || _originalTelepon != Telepon || _originalJumlah != Jumlah ||
                   _originalAlamat != Alamat || _originalTanggal != Tanggal || _originalJam != Jam;
        }

        // Metode untuk menjalankan logika saat tombol Edit ditekan.
        public void Edit(object parameter)
        {
            DonaturViewModel viewModel = parameter as DonaturViewModel;
            if (viewModel != null)
            {
                // Logika untuk tindakan Edit
                MessageBox.Show("Data berhasil diedit!");

                // Reset nilai original setelah edit
                InitializeOriginalValues();

                // Atur HasChanges ke false
                viewModel.HasChanges = false;
            }
        }

        // Metode untuk menjalankan logika saat tombol Delete ditekan.
        private void Delete(object parameter)
        {
            DonaturViewModel viewModel = parameter as DonaturViewModel;
            viewModel?.DonaturList.Remove(this);
            MessageBox.Show("Data berhasil dihapus!");
        }
    }
}