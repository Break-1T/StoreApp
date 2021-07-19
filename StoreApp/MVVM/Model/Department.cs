using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using StoreApp.Annotations;
using StoreApp.Resources;

namespace StoreApp.MVVM.Model
{
    class Department
    {
        public Department()
        {
            Image = Images.Empty_departament_icon;
            Employees = new ObservableCollection<Employee>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [CanBeNull]
        public byte[] Image { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }

    }
}
