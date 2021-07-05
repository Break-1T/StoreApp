using System.ComponentModel.DataAnnotations;

namespace StoreApp.MVVM.Model
{
    class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public Department Department { get; set; }
    }
}
