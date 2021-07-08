using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        }

        public DbControl DataBaseControl { get; set; }

        public bool Login(Employee employee)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {

                    if (db.Employees.FirstOrDefault(x => (x.Login == employee.Login) && (x.Password == employee.Password)) != null)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Логин или пароль не найдены");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
