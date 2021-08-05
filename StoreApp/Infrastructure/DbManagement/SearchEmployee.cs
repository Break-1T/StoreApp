using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using StoreApp.Annotations;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.MVVM.Model;

namespace StoreApp.Infrastructure.DbManagement
{
    class SearchEmployee:INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private AccessLevel _accessLevel;
        private string _password;
        private string _login;
        private string _phoneNumber;
        private string _email;
        private string _surname;

        public SearchEmployee()
        {
            Id = "";
            Name = "";
            Surname = "";
            Email = "";
            Login = "";
            PhoneNumber = "";
            Password = "";
            AccessLevel = new AccessLevel();
        }

        public string Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (value == _surname) return;
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (value == _email) return;
                _email = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (value == _phoneNumber) return;
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                if (value == _login) return;
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        public AccessLevel AccessLevel
        {
            get => _accessLevel;
            set
            {
                if (Equals(value, _accessLevel)) return;
                _accessLevel = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
