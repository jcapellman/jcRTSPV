using System;
using System.Collections.Generic;

using Windows.Storage;

using jcRTSPV.Objects;

using Microsoft.Toolkit.Uwp;

using Newtonsoft.Json;

namespace jcRTSPV.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private List<string> _cameraFeeds;

        public List<string> CameraFeeds
        {
            get => _cameraFeeds;
            set { _cameraFeeds = value; OnPropertyChanged(); }
        }

        public async void LoadSettings()
        {
            CameraFeeds = new List<string>();

            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (!await storageFolder.FileExistsAsync(Common.Constants.FILENAME_SETTINGS))
            {
                return;
            }

            var settingsFile = await storageFolder.GetFileAsync(Common.Constants.FILENAME_SETTINGS);

            var str = await Windows.Storage.FileIO.ReadTextAsync(settingsFile);

            var settingsItem = (SettingsFileItem)JsonConvert.DeserializeObject(str);

            CameraFeeds = settingsItem.CameraFeeds;
        }

        public async void WriteSettings()
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            
            var settingsFile = await storageFolder.CreateFileAsync(Common.Constants.FILENAME_SETTINGS, CreationCollisionOption.ReplaceExisting);

            var settingsItem = new SettingsFileItem
            {
                CameraFeeds = CameraFeeds
            };

            await FileIO.WriteTextAsync(settingsFile, JsonConvert.SerializeObject(settingsItem));
        }
    }
}