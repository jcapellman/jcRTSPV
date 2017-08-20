using System.Collections.Generic;
using System.Linq;

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

        private void btnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            pNewForm.IsOpen = false;
        }

        private void btnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.AddFeed();
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
            ViewModel.RemoveSelectedFeeds(lvCameraFeeds.SelectedItems.Cast<string>().ToList());
        }

        private void lvCameraFeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.EnableDelete = lvCameraFeeds.SelectedItems.Any();
        }
    }
}