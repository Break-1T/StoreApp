using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using StoreApp.Annotations;
using StoreApp.Infrastructure.Interfaces;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.Resources;

namespace StoreApp.MVVM.Model
{
    class User : ClassWithImage
    {
        public User()
        {
            Orders = new ObservableCollection<Order>();
            Image = Images.Empty_user_icon;
        }
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [CanBeNull]
        public AccessLevel AccessLevel { get; set; }
        [CanBeNull]
        public ObservableCollection<Order> Orders { get; set; }

        public string GetFullName => Name + " " + Surname;
    }
}
