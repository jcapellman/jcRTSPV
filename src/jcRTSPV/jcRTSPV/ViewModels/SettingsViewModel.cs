using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using FFmpegInterop;

namespace jcRTSPV.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public string OriginalCameraFeedURL { get; set; }

        private string _editFormCameraFeedURL;

        public string EditFormCameraFeedURL
        {
            get => _editFormCameraFeedURL;
            set
            {
                _editFormCameraFeedURL = value;
                OnPropertyChanged();
                EditFormValid = !string.IsNullOrWhiteSpace(value) && value.Length > 0;
            }
        }

        private bool _editFormValid;

        public bool EditFormValid
        {
            get => _editFormValid;

            set
            {
                _editFormValid = value;
                OnPropertyChanged();
            }
        }

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

        private bool _enableEdit;

        public bool EnableEdit
        {
            get => _enableEdit;

            set
            {
                _enableEdit = value;
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
            EditFormValid = false;
            OriginalCameraFeedURL = string.Empty;
        }

        internal void RemoveSelectedFeeds(List<string> list)
        {
            foreach (var item in list)
            {
                CameraFeeds.Remove(item);
            }
        }

        public bool AddFeed()
        {
            var feed = FFmpegInteropMSS.CreateFFmpegInteropMSSFromUri(FormCameraFeedURL, false, true);

            if (feed == null)
            {
                return false;
            }

            CameraFeeds.Add(FormCameraFeedURL);

            FormCameraFeedURL = string.Empty;

            return true;
        }

        public void WriteSettings()
        {
            SettingManager.WriteSettings(CameraFeeds.ToList());
        }

        public bool CommitEdit()
        {
            for (var x = 0; x < CameraFeeds.Count; x++)
            {
                if (CameraFeeds[x] != OriginalCameraFeedURL)
                {
                    continue;
                }

                CameraFeeds[x] = EditFormCameraFeedURL;

                break;
            }

            EditFormCameraFeedURL = string.Empty;
            OriginalCameraFeedURL = string.Empty;

            return true;
        }
    }
}