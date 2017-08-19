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
        private string _formCameraFeedURL;

        public string FormCameraFeedURL
        {
            get => _formCameraFeedURL;
            set
            {
                _formCameraFeedURL = value;
                OnPropertyChanged();
                FormValid = !string.IsNullOrWhiteSpace(value) && value.Length > 0;
            }
        }
        
        private bool _formValid;

        public bool FormValid
        {
            get => _formValid;

            set
            {
                _formValid = value;
                OnPropertyChanged();
            }
        }

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

            var settingsItem = JsonConvert.DeserializeObject<SettingsFileItem>(str);

            CameraFeeds = settingsItem.CameraFeeds;

            FormValid = false;
        }

        public void AddFeed()
        {
            CameraFeeds.Add(FormCameraFeedURL);

            FormCameraFeedURL = string.Empty;
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