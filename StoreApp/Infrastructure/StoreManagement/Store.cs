using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.MVVM.Model;

namespace StoreApp.Infrastructure.StoreManagement
{
    class Store
    {
        public Store()
        {
            DataBaseControl = new DbControl();
            AdminDepartament = new Department() { Name = "Admin" };
        }

        public DbControl DataBaseControl { get; set; }
        public Department AdminDepartament { get; set; }
    }
}
