﻿using StoreApp.MVVM.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;
using StoreApp.Infrastructure.Commands;
using StoreApp.MVVM.Model;
using StoreApp.Resources;

namespace StoreApp.MVVM.ViewModel
{
    class HomePageViewModel:BaseViewModel
    {
        public HomePageViewModel()
        {
            EditCommand = new RelayCommand(OnAppEditCommandExecute, CanAppEditCommandExecute);
            SaveChangesCommand = new RelayCommand(OnAppSaveChangesCommandExecute, CanAppSaveChangesCommandExecute);
            DeclineChangesCommand = new RelayCommand(OnAppDeclineChangesCommandExecute, CanAppDeclineChangesCommandExecute);
            UploadImageCommand = new RelayCommand(OnAppUploadImageCommandExecute, CanAppUploadImageCommandExecute);

            ButtonsVisibility = Visibility.Hidden.ToString();
            ToReadOnlyFields();
        }

        #region Fields

        private string _buttonsVisibility;
        
        private bool _isReadOnlyField;
        private bool _isReadOnlyCaretVisible;
        
        private Employee tempEmployee;

        #endregion

        #region Properties

        public MainWindowViewModel MainWM { get; set; }

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

        public RelayCommand EditCommand { get; }
        public RelayCommand SaveChangesCommand { get; }
        public RelayCommand DeclineChangesCommand { get; }
        public RelayCommand UploadImageCommand { get; }

        #endregion

        private bool CanAppEditCommandExecute(object arg) => true;
        private void OnAppEditCommandExecute(object obj)
        {
            tempEmployee = GetEmployeeCopy(MainWM.Employee);
            ButtonsVisibility = Visibility.Visible.ToString();
            ToReadWriteFields();
        }

        private bool CanAppDeclineChangesCommandExecute(object arg) => true;
        private void OnAppDeclineChangesCommandExecute(object obj)
        {
            MainWM.Employee = tempEmployee;
            ButtonsVisibility = Visibility.Hidden.ToString();
            ToReadOnlyFields();
        }

        private bool CanAppSaveChangesCommandExecute(object arg) => true;
        private void OnAppSaveChangesCommandExecute(object obj)
        {
            MainWM.StoreManagement.UpdateEmployee(MainWM.Employee);
            tempEmployee = null;
            ButtonsVisibility = Visibility.Hidden.ToString();
            ToReadOnlyFields();
        }


        private bool CanAppUploadImageCommandExecute(object arg) => true;
        private void OnAppUploadImageCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            MainWM.Employee.Image = File.ReadAllBytes(dialog.FileName);
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

    }

    public enum Visibility
    {
        Visible,
        Hidden
    }
}
