using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp;

using jcRTSPV.Objects;

using Newtonsoft.Json;
using Windows.Storage;

namespace jcRTSPV.Managers
{
    public class SettingsManager : BaseManager
    {
        public async Task<SettingsFileItem> LoadSettingsAsync()
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (!await storageFolder.FileExistsAsync(Common.Constants.FILENAME_SETTINGS))
            {
                return null;
            }

            var settingsFile = await storageFolder.GetFileAsync(Common.Constants.FILENAME_SETTINGS);

            var str = await Windows.Storage.FileIO.ReadTextAsync(settingsFile);

            return JsonConvert.DeserializeObject<SettingsFileItem>(str);            
        }

        public async void WriteSettings(List<string> cameraFeeds)
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            var settingsFile = await storageFolder.CreateFileAsync(Common.Constants.FILENAME_SETTINGS, CreationCollisionOption.ReplaceExisting);

            var settingsItem = new SettingsFileItem
            {
                CameraFeeds = cameraFeeds
            };

            await FileIO.WriteTextAsync(settingsFile, JsonConvert.SerializeObject(settingsItem));
        }
    }
}