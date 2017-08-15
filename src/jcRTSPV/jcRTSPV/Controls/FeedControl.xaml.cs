using System;
using Windows.Media.Core;
using Windows.Media.Streaming.Adaptive;

using Windows.UI.Xaml.Controls;
using YunFan.Gallery.FFmpegInterop;

namespace jcRTSPV.Controls
{
    public sealed partial class FeedControl : UserControl
    {
        public FeedControl()
        {
            InitializeComponent();

            LoadData("rtsp://192.168.2.82:7447/5987abd6aa8cffef92ee3d7c_0");
        }
        
        private void LoadData(string url)
        {
            var feed = FFmpegInteropMSS.CreateFFmpegInteropMSSFromUri(url, false, true);

            MediaStreamSource mss = feed.GetMediaStreamSource();

            meFeed.SetMediaStreamSource(mss);
        }
    }
}