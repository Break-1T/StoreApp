using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using StoreApp.Annotations;
using StoreApp.Infrastructure.Interfaces;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.Resources;

namespace StoreApp.MVVM.Model
{
    class Employee: ClassWithImage
    {
        public Employee()
        {
            Image = Images.Empty_employee_icon;
        }

        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        [CanBeNull]
        public AccessLevel AccessLevel { get; set; }

        [CanBeNull]
        public Department Department { get; set; }
    }
}
