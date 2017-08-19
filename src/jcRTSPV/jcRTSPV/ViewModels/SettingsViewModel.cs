using System.Collections.Generic;

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

            var settings = await SettingManager.LoadSettingsAsync();

            if (settings != null)
            {
                CameraFeeds = settings.CameraFeeds;
            }
            
            FormValid = false;
        }

        public void AddFeed()
        {
            CameraFeeds.Add(FormCameraFeedURL);

            FormCameraFeedURL = string.Empty;
        }

        public void WriteSettings()
        {
            SettingManager.WriteSettings(CameraFeeds);
        }
    }
}