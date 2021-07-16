using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.MVVM.Model
{
    class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Image { get; set; }

        public List<Order> Orders { get; set; }
    }
}
