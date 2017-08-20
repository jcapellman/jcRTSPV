using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        private bool _enableDelete;

        public bool EnableDelete
        {
            get => _enableDelete;

            set
            {
                _enableDelete = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _cameraFeeds;

        public ObservableCollection<string> CameraFeeds
        {
            get => _cameraFeeds;
            set { _cameraFeeds = value; OnPropertyChanged(); }
        }

        public async void LoadSettings()
        {
            CameraFeeds = new ObservableCollection<string>();

            var settings = await SettingManager.LoadSettingsAsync();

            if (settings != null)
            {
                CameraFeeds = new ObservableCollection<string>(settings.CameraFeeds);
            }
            
            FormValid = false;
        }

        internal void RemoveSelectedFeeds(List<string> list)
        {
            foreach (var item in list)
            {
                CameraFeeds.Remove(item);
            }
        }

        public void AddFeed()
        {
            CameraFeeds.Add(FormCameraFeedURL);

            FormCameraFeedURL = string.Empty;
        }

        public void WriteSettings()
        {
            SettingManager.WriteSettings(CameraFeeds.ToList());
        }
    }
}