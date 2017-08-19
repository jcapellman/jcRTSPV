using System.ComponentModel;
using System.Runtime.CompilerServices;

using jcRTSPV.Annotations;
using jcRTSPV.Managers;

namespace jcRTSPV.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public SettingsManager SettingManager;

        public BaseViewModel()
        {
            SettingManager = new SettingsManager();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}