using System.Linq;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using jcRTSPV.ViewModels;

namespace jcRTSPV.Views
{
    public sealed partial class SettingsPage : BasePage
    {
        private SettingsViewModel ViewModel => (SettingsViewModel) DataContext;

        public SettingsPage()
        {
            InitializeComponent();

            DataContext = new SettingsViewModel();

            ViewModel.LoadSettings();
        }

        private void btnSave_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.WriteSettings();

            ShowMessageBox(Common.Constants.MSG_SETTINGS_SAVED);
        }

        private void btnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            pNewForm.IsOpen = false;
        }

        private void btnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var result = ViewModel.AddFeed();

            if (!result)
            {
                ShowMessageBox(Common.Constants.MSG_SETTINGS_INVALID_URL);

                return;
            }

            pNewForm.IsOpen = false;
        }

        private void btnOpenPopup_OnClick(object sender, RoutedEventArgs e)
        {
            if (pNewForm.IsOpen)
            {
                return;
            }

            pNewForm.IsOpen = true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var count = lvCameraFeeds.SelectedItems.Cast<string>().Count();

            ViewModel.RemoveSelectedFeeds(lvCameraFeeds.SelectedItems.Cast<string>().ToList());

            ShowMessageBox($"{count} {Common.Constants.MSG_SETTINGS_INVALID_URL}");            
        }

        private void lvCameraFeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.EnableDelete = lvCameraFeeds.SelectedItems.Any();
        }
    }
}