using Berkati_Frontend.ViewModels;
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
