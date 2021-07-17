﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
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
    }
}
