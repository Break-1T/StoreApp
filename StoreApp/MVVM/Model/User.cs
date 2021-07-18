using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using StoreApp.Resources;

namespace StoreApp.MVVM.Model
{
    class User
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
        public byte[] Image { get; set; }

        public ObservableCollection<Order> Orders { get; set; }
    }
}
