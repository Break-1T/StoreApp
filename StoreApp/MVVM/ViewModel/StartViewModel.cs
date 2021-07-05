using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using StoreApp.Infrastructure.Commands;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.MVVM.Model;
using StoreApp.MVVM.View.Pages;
using StoreApp.MVVM.View.Windows;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class StartViewModel:BaseViewModel
    {
        public StartViewModel()
        {
            dbControl = new DbControl();
            Administrator = new Administrator();
            
            ToRegistrationPageCommand = new RelayCommand(OnAppToRegistrationPageCommandExecute, CanAppToRegistrationPageCommandExecute);
            ToAuthorizationPageCommand = new RelayCommand(OnAppToAuthorizationPageCommandExecute,
                CanAppToAuthorizationPageCommandExecute);
            RegistrateAdmin = new RelayCommand(OnAppRegistrateAdminCommandExecute, CanAppRegistrateAdminCommandExecute);

            AuthorizationPage = new AuthorizationPage(){DataContext = this};
            RegistrationPage = new RegistrationPage(){DataContext = this};
            CurrentPage = AuthorizationPage;
        }

        #region Fields

        private DbControl dbControl;
        
        private AuthorizationPage authorizationPage;
        private RegistrationPage registrationPage;
        private Page currentPage;

        #endregion

        #region Propertys

        public Administrator Administrator { get; set; }
        
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
        public RelayCommand RegistrateAdmin { get; }

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

        private bool CanAppRegistrateAdminCommandExecute(object arg) => true;
        private void OnAppRegistrateAdminCommandExecute(object obj)
        {
            dbControl.AddAdministrator(Administrator.Login,Administrator.Password,Administrator.Name,
                Administrator.Surname,Administrator.PhoneNumber,Administrator.Email);
        }
    }
}
