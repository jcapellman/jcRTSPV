using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<bool> LoadFeeds()
        {
            var feeds = new List<string>();

            var settings = await SettingManager.LoadSettingsAsync();

            if (settings == null)
            {
                Feeds = feeds;

                return false;
            }

            Feeds = settings.CameraFeeds;

            return true;
        }
    }
}