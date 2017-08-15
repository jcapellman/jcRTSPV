using System.Collections.Generic;

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

        public void LoadFeeds()
        {
            var feeds = new List<string>
            {
                "rtsp://192.168.2.82:7447/5987abd6aa8cffef92ee3d7c_0"
            };

            Feeds = feeds;
        }
    }
}