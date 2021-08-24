using StoreApp.MVVM.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;
using StoreApp.Infrastructure.Commands;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.MVVM.Model;
using StoreApp.Resources;

namespace StoreApp.MVVM.ViewModel
{
    class HomePageViewModel:BaseViewModel
    {
        public HomePageViewModel()
        {
            EditCommand = new RelayCommand(OnAppEditCommandExecute);
            SaveChangesCommand = new RelayCommand(OnAppSaveChangesCommandExecute);
            DeclineChangesCommand = new RelayCommand(OnAppDeclineChangesCommandExecute);
            UploadImageCommand = new RelayCommand(OnAppUploadImageCommandExecute);
            ButtonsVisibility = Visibility.Hidden.ToString();

            Employee = new Employee();
            StoreManagement = new Store();
            ToReadOnlyFields();
        }

        #region Fields

        private string _buttonsVisibility;
        
        private bool _isReadOnlyField;
        private bool _isReadOnlyCaretVisible;
        
        private Employee tempEmployee;
        private Employee _employee;

        #endregion

        #region Properties

        public Employee Employee
        {
            get => _employee;
            set
            {
                if (Equals(value, _employee)) return;
                _employee = value;
                OnPropertyChanged();
            }
        }

        public Store StoreManagement { get; set; }
        public string ButtonsVisibility
        {
            get => _buttonsVisibility;
            set
            {
                if (value == _buttonsVisibility) return;
                _buttonsVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsReadOnlyField
        {
            get => _isReadOnlyField;
            set
            {
                if (value == _isReadOnlyField) return;
                _isReadOnlyField = value;
                OnPropertyChanged();
            }
        }
        public bool IsReadOnlyCaretVisible
        {
            get => _isReadOnlyCaretVisible;
            set
            {
                if (value == _isReadOnlyCaretVisible) return;
                _isReadOnlyCaretVisible = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand EditCommand { get; private set; }
        public RelayCommand SaveChangesCommand { get; private set; }
        public RelayCommand DeclineChangesCommand { get; private set; }
        public RelayCommand UploadImageCommand { get; private set; }
        
        #endregion

        private void OnAppEditCommandExecute(object obj)
        {
            tempEmployee = GetEmployeeCopy(Employee);
            ButtonsVisibility = Visibility.Visible.ToString();
            ToReadWriteFields();
        }

        private void OnAppDeclineChangesCommandExecute(object obj)
        {
            Employee = tempEmployee;
            ButtonsVisibility = Visibility.Hidden.ToString();
            ToReadOnlyFields();
        }

        private void OnAppSaveChangesCommandExecute(object obj)
        {
            StoreManagement.UpdateEmployee(Employee);
            tempEmployee = null;
            ButtonsVisibility = Visibility.Hidden.ToString();
            ToReadOnlyFields();
        }

        private void OnAppUploadImageCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            Employee.Image = File.ReadAllBytes(dialog.FileName);
        }

        public void ToReadOnlyFields()
        {
            IsReadOnlyField = true;
            IsReadOnlyCaretVisible = false;
        }
        public void ToReadWriteFields()
        {
            IsReadOnlyField = false;
            IsReadOnlyCaretVisible = true;
        }

        public Employee GetEmployeeCopy(Employee employee)
        {
            return new Employee()
            {
                AccessLevel = employee.AccessLevel,
                Department = employee.Department,
                Email = employee.Email,
                Id = employee.Id,
                Image = employee.Image,
                Login = employee.Login,
                Name = employee.Name,
                Password = employee.Password,
                PhoneNumber = employee.PhoneNumber,
                Surname = employee.Surname
            };
        }

        public override void Dispose()
        {
            StoreManagement = null;
            Employee = null;
        }
    }
}
