﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using StoreApp.Annotations;

namespace StoreApp.MVVM.ViewModel.Base
{
    class BaseViewModel:INotifyPropertyChanged,IDisposable
    {
        public bool HasChanges { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public virtual void Update(){}

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public virtual void Dispose()
        {
            //MessageBox.Show(this.GetType().Name + " - Disposed!");
        }
    }
}
