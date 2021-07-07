using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using StoreApp.Infrastructure.Commands;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.Infrastructure.StoreManagement;
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
            AuthorizationPage = new AuthorizationPage() { DataContext = this };
            RegistrationPage = new RegistrationPage() { DataContext = this };
            CurrentPage = AuthorizationPage;

            store = new Store();
            Employee = new Employee();
            
            ToRegistrationPageCommand = new RelayCommand(OnAppToRegistrationPageCommandExecute, CanAppToRegistrationPageCommandExecute);
            ToAuthorizationPageCommand = new RelayCommand(OnAppToAuthorizationPageCommandExecute,
                CanAppToAuthorizationPageCommandExecute);
            RegistrateAdmin = new RelayCommand(OnAppRegistrateAdminCommandExecute, CanAppRegistrateAdminCommandExecute);
        }

        #region Fields

        private Store store;
        
        private AuthorizationPage authorizationPage;
        private RegistrationPage registrationPage;
        private Page currentPage;

        #endregion

        #region Propertys

        public Employee Employee { get; set; }

        public Func<string> Password1Handler { get; set; }
        public Func<string> Password2Handler { get; set; }

        
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
            try
            {
                if (Password1Handler() == Password2Handler())
                {
                    Employee.Password = Password1Handler();
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        if (db.Employees.FirstOrDefault(x => x.Login == Employee.Login) != null)
                            throw new Exception("Данный логин уже используется");
                        if (db.Employees.FirstOrDefault(x => x.PhoneNumber == Employee.PhoneNumber) != null)
                            throw new Exception("Данный номер телефона уже используется");
                        if (db.Employees.FirstOrDefault(x => x.Email == Employee.Email) != null)
                            throw new Exception("Данный email уже используется");

                        store.DataBaseControl.AddEmployee(Employee.Login,Employee.Password, AccessLevel.Administrator.ToString(),Employee.Name,Employee.Surname,Employee.Email,db.Departments.FirstOrDefault(x=>x.Id== 1));
                    }
                    MessageBox.Show("Успех!");
                }
                else
                    MessageBox.Show("Пароли не совпадают");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
