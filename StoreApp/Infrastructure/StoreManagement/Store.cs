using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> LoginAsync(Employee employee)
        {
            return await Task.Run(()=>Login(employee));
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var tmp = db.Employees.FirstOrDefault(x=> x.Id==employee.Id);
                    EmployeeCopy(tmp,employee);
                    db.Entry(tmp).State = EntityState.Modified;
                    db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            return await Task.Run(()=>UpdateEmployee(employee));
        }
        private void EmployeeCopy(Employee empOut,Employee eppIn)
        {
            empOut.AccessLevel = eppIn.AccessLevel;
            empOut.Department = eppIn.Department;
            empOut.Email = eppIn.Email;
            empOut.Id = eppIn.Id;
            empOut.Image = eppIn.Image;
            empOut.Login = eppIn.Login;
            empOut.Name = eppIn.Name;
            empOut.Password = eppIn.Password;
            empOut.PhoneNumber = eppIn.PhoneNumber;
            empOut.Surname = eppIn.Surname;
        }

        /// <summary>
        /// Метод для сравнения 2-х объектов для LINQ выражения. В случае, если 2-й объект будет IsNullOrEmpty, то возвратиться true и цикл продолжит свою работу не обращая на это внимания
        /// </summary>
        /// <param name="x">Параметр для сравнения</param>
        /// <param name="y">Параметр который сравнивается</param>
        /// <returns>bool</returns>
        public bool CanEqualsSearchQueryBeCompleted(string x, string y)
        {
            if (string.IsNullOrEmpty(y))
                return true;

            return x.ToLower() == y.ToLower();
        }

        /// <summary>
        /// Метод для сравнения 2-х объектов для LINQ выражения. В случае, если 2-й объект будет null, то возвратиться true и цикл продолжит свою работу не обращая на это внимания
        /// </summary>
        /// <param name="x">Параметр для сравнения</param>
        /// <param name="y">Параметр который сравнивается</param>
        /// <returns>bool</returns>
        public bool CanEqualsSearchQueryBeCompleted(int? x, int? y)
        {
            if (y == null)
                return true;

            return x == y;
        }

        /// <summary>
        /// Метод для сравнения 2-х объектов для LINQ выражения. В случае, если 2-й объект будет IsNullOrEmpty, то возвратиться true и цикл продолжит свою работу не обращая на это внимания
        /// </summary>
        /// <param name="x">Параметр для сравнения</param>
        /// <param name="y">Параметр который сравнивается</param>
        /// <returns>bool</returns>
        public bool CanEqualsSearchQueryBeCompleted(DateTime x, string y)
        {
            DateTime date = new DateTime();

            if (string.IsNullOrEmpty(y))
                return true;

            if (DateTime.TryParse(y, out date))
            {
                return x.Day == date.Day && x.Month == date.Month && x.Year == date.Year;
            }

            return false;
        }
    }
}
