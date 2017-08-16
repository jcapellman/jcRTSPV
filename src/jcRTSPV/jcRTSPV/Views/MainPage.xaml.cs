using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;

using FFmpegInterop;

using jcRTSPV.ViewModels;

namespace jcRTSPV.Views
{
    public sealed partial class MainPage : Page
    {
        private FFmpegInteropMSS _fFmpegMss;
        private MediaStreamSource _feedSource;

        private MainViewModel ViewModel => (MainViewModel) DataContext;

        public MainPage()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

            ViewModel.LoadFeeds();

            foreach (var feed in ViewModel.Feeds)
            {
                LoadData(feed);
            }
        }
        
        private void LoadData(string url)
        {
            var options = new PropertySet();
           
            _fFmpegMss = FFmpegInteropMSS.CreateFFmpegInteropMSSFromUri(url, false, true, options);

            _feedSource = _fFmpegMss?.GetMediaStreamSource();

            if (_feedSource != null)
            {
                meFeed.SetMediaStreamSource(_feedSource);
            }
        }
    }
}