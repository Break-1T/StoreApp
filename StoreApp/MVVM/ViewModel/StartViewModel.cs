using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Infrastructure.Commands;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class StartViewModel:BaseViewModel
    {
        public StartViewModel()
        {
            RegistrationCommand = new RelayCommand(OnAppRegistrationCommandExecute, CanAppRegistrationCommandExecute);
        }

        private bool CanAppRegistrationCommandExecute(object arg) => true;

        private void OnAppRegistrationCommandExecute(object obj)
        {
            
        }

        public RelayCommand RegistrationCommand { get; }
    }
}
