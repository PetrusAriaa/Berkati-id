using Berkati_Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Berkati_Frontend
{
    public partial class App : Application
    {
        public DonaturViewModel? DonaturViewModel { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DonaturViewModel = new DonaturViewModel();
        }
    }
}
