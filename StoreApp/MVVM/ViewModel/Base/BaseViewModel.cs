using System.ComponentModel;
using System.Runtime.CompilerServices;
using StoreApp.Annotations;

namespace StoreApp.MVVM.ViewModel.Base
{
    class BaseViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
