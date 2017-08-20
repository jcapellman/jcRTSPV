using System;
using System.Linq;

using Windows.UI.Popups;
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

        private async void btnSave_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.WriteSettings();

            await new MessageDialog(Common.Constants.MSG_SETTINGS_SAVED).ShowAsync();
        }

        private void btnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            pNewForm.IsOpen = false;
        }

        private async void btnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var result = ViewModel.AddFeed();

            if (!result)
            {
                await new MessageDialog(Common.Constants.MSG_SETTINGS_INVALID_URL).ShowAsync();

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

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var count = lvCameraFeeds.SelectedItems.Cast<string>().Count();

            ViewModel.RemoveSelectedFeeds(lvCameraFeeds.SelectedItems.Cast<string>().ToList());

            var md = new MessageDialog($"{count} {Common.Constants.MSG_SETTINGS_INVALID_URL}");

            await md.ShowAsync();
        }

        private void lvCameraFeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.EnableDelete = lvCameraFeeds.SelectedItems.Any();
        }
    }
}