using System;

using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace jcRTSPV.Views
{
    public class BasePage : Page
    {
        public async void ShowMessageBox(string content)
        {
            var md = new MessageDialog(content);

            await md.ShowAsync();
        }
    }
}