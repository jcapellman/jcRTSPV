using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

using FFmpegInterop;

namespace jcRTSPV.Controls
{
    public sealed partial class FeedControl : UserControl
    {
        private FFmpegInteropMSS _fFmpegMss;
        private MediaStreamSource _feedSource;
        
        public FeedControl()
        {
            InitializeComponent();
        }

        public void LoadData(string url)
        {
            var options = new PropertySet();

            _fFmpegMss = FFmpegInteropMSS.CreateFFmpegInteropMSSFromUri(url, false, true, options);

            _feedSource = _fFmpegMss?.GetMediaStreamSource();

            if (_feedSource != null)
            {
                meFeed.SetMediaStreamSource(_feedSource);
            }
        }

        private void symbolMute_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            meFeed.IsMuted = !meFeed.IsMuted;

            tbIcon.Text = meFeed.IsMuted ? "\uE198" : "\uE15D";
        }
    }
}