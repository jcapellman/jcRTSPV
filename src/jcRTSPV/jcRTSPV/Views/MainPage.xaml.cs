using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using jcRTSPV.ViewModels;

namespace jcRTSPV.Views
{
    public sealed partial class MainPage : Page
    {
        
        private MainViewModel ViewModel => (MainViewModel) DataContext;

        public MainPage()
        {
            InitializeComponent();

            DataContext = new MainViewModel();            
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadFeeds();
        }
    }
}