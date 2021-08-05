using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using StoreApp.MVVM.Model;

namespace StoreApp.Infrastructure.StoreManagement
{
    class AccessLevel
    {
        public AccessLevel()
        {
            Employees = new ObservableCollection<Employee>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ObservableCollection<Employee> Employees { get; set; }
    }
    //enum AccessLevel
    //{
    //    User,
    //    Employee,
    //    Administrator
    //}
}
