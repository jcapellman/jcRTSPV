using System;
using System.Collections.Generic;

using jcRTSPV.Objects;

using Microsoft.Toolkit.Uwp;

using Newtonsoft.Json;

namespace jcRTSPV.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private List<string> _feeds;

        public List<string> Feeds
        {
            get => _feeds;

            set { _feeds = value; OnPropertyChanged(); }
        }

        public async void LoadFeeds()
        {
            var feeds = new List<string>();

            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (!await storageFolder.FileExistsAsync(Common.Constants.FILENAME_SETTINGS))
            {
                return;
            }

            var settingsFile = await storageFolder.GetFileAsync(Common.Constants.FILENAME_SETTINGS);

            var str = await Windows.Storage.FileIO.ReadTextAsync(settingsFile);
            
            var settingsItem = JsonConvert.DeserializeObject<SettingsFileItem>(str);

            Feeds = settingsItem.CameraFeeds;
        }
    }
}