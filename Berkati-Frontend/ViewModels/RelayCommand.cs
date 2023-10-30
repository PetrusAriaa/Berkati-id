using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Berkati_Frontend.ViewModels
{
        // Mendefinisikan kelas RelayCommand yang mengimplementasikan antarmuka ICommand.
        public class RelayCommand : ICommand
        {
            // Variabel untuk menyimpan aksi yang akan dieksekusi.
            private Action<object> _execute;
            // Variabel untuk menyimpan fungsi yang menentukan apakah perintah dapat dieksekusi.
            private Func<object, bool> _canExecute;

            // Konstruktor kelas RelayCommand. Menerima aksi yang akan dieksekusi dan fungsi untuk mengecek apakah perintah dapat dieksekusi.
            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            // Metode CanExecute menentukan apakah perintah dapat dieksekusi dengan parameter tertentu.
            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            // Metode Execute mengeksekusi perintah dengan parameter tertentu.
            public void Execute(object parameter)
            {
                _execute(parameter);
            }

            // Implementasi event CanExecuteChanged dari antarmuka ICommand.
            public event EventHandler CanExecuteChanged
            {
                // Menambah dan menghapus event handler dari event CommandManager.RequerySuggested.
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
}
