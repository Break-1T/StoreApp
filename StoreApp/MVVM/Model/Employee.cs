using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using StoreApp.Annotations;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.Resources;

namespace StoreApp.MVVM.Model
{
    class Employee:INotifyPropertyChanged
    {
        public Employee()
        {
            Image = Images.Empty_employee_icon;
        }
        private string _name;
        private string _surname;
        private string _email;
        private Department _department;
        private string _phoneNumber;
        private string _login;
        private string _password;
        private byte[] _image;

        [Key]
        public int Id { get; set; }
        
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
        
        [CanBeNull]
        public AccessLevel AccessLevel { get; set; }

        [Required, CanBeNull]
        public byte[] Image
        {
            get => _image;
            set
            {
                if (Equals(value, _image)) return;
                _image = value;
                OnPropertyChanged();
            }
        }

        [CanBeNull]
        public Department Department
        {
            get => _department;
            set
            {
                if (Equals(value, _department)) return;
                _department = value;
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
