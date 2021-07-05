using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using StoreApp.Infrastructure.Commands;
using StoreApp.MVVM.View.Pages;
using StoreApp.MVVM.View.Windows;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class StartViewModel:BaseViewModel
    {
        public StartViewModel()
        {
            ToRegistrationPageCommand = new RelayCommand(OnAppToRegistrationPageCommandExecute, CanAppToRegistrationPageCommandExecute);
            ToAuthorizationPageCommand = new RelayCommand(OnAppToAuthorizationPageCommandExecute,
                CanAppToAuthorizationPageCommandExecute);
            RegistrateUser = new RelayCommand(OnAppRegistrateUserCommandExecute,CanAppRegistrateUserCommandExecute);

            AuthorizationPage = new AuthorizationPage(){DataContext = this};
            RegistrationPage = new RegistrationPage(){DataContext = this};
            CurrentPage = AuthorizationPage;
        }

        #region Fields

        private AuthorizationPage authorizationPage;
        private RegistrationPage registrationPage;
        private Page currentPage;

        #endregion

        #region Propertys
        public StartupWindow StartupWindow { get; set; }
        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        public AuthorizationPage AuthorizationPage
        {
            get
            {
                return authorizationPage;
            }
            set
            {
                authorizationPage = value;
                OnPropertyChanged("AuthorizationPage");
            }
        }
        public RegistrationPage RegistrationPage
        {
            get
            {
                return registrationPage;
            }
            set
            {
                registrationPage = value;
                OnPropertyChanged("RegistrationPage");
            }
        }

        #endregion

        #region Commands

        public RelayCommand ToRegistrationPageCommand { get; }
        public RelayCommand ToAuthorizationPageCommand { get; }
        public RelayCommand RegistrateUser { get; }

        #endregion

        private bool CanAppToRegistrationPageCommandExecute(object arg) => true;
        private void OnAppToRegistrationPageCommandExecute(object obj)
        {
            CurrentPage = RegistrationPage;
        }

        private bool CanAppToAuthorizationPageCommandExecute(object arg) => true;
        private void OnAppToAuthorizationPageCommandExecute(object obj)
        {
            CurrentPage = AuthorizationPage;
        }

        private bool CanAppRegistrateUserCommandExecute(object arg) => true;
        private void OnAppRegistrateUserCommandExecute(object obj)
        {
        }
    }
}
