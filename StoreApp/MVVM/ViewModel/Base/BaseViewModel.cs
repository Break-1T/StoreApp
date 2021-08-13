using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using StoreApp.Annotations;

namespace StoreApp.MVVM.ViewModel.Base
{
    class BaseViewModel:INotifyPropertyChanged,IDisposable
    {
        public bool IsActiveViewModel { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public virtual void Dispose()
        {
            IsActiveViewModel = false;
            //MessageBox.Show(this.GetType().Name + " - Disposed!");
        }

        /// <summary>
        /// Заполнение ViewModel данными
        /// </summary>
        public virtual void FillViewModel()
        {
            IsActiveViewModel = true;
        }
        public virtual void FillViewModel(object x)
        {
            IsActiveViewModel = true;
        }
    }
}
