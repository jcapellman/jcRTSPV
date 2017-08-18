using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using jcRTSPV.ViewModels;

namespace jcRTSPV.Views
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel => (SettingsViewModel) DataContext;

        public SettingsPage()
        {
            this.InitializeComponent();

            DataContext = new SettingsViewModel();

            ViewModel.LoadSettings();
        }

        private void btnSave_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.WriteSettings();
        }
    }
}