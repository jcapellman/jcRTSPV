using System.Collections.ObjectModel;
using System.Threading.Tasks;

using jcRTSPV.Controls;

namespace jcRTSPV.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<FeedControl> _feeds;

        public ObservableCollection<FeedControl> Feeds
        {
            get => _feeds;

            set { _feeds = value; OnPropertyChanged(); }
        }

        public async Task<bool> LoadFeeds()
        {
            var settings = await SettingManager.LoadSettingsAsync();

            if (settings == null)
            {
                Feeds = new ObservableCollection<FeedControl>();

                return false;
            }

            Feeds = new ObservableCollection<FeedControl>();

            foreach (var url in settings.CameraFeeds)
            {
                var feedControl = new FeedControl();
                feedControl.LoadData(url);

                Feeds.Add(feedControl);
            }
            
            return true;
        }
    }
}