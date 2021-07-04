using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using StoreApp.MVVM.Model;

namespace StoreApp.Infrastructure.DbManagement
{
    class DbControl
    {
        public bool AddDepartament(string Name)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Departments.Add(
                        new Department()
                        {
                            Name = Name
                        });
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        public bool RemoveDepartament(string Name)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Departments.Remove(db.Departments.FirstOrDefault(x => x.Name == Name));
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        
        public bool AddProduct(string Name,Image Image)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Products.Add(
                        new Product()
                        {
                            Name = Name,
                            Image = Image
                        });
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        public bool RemoveProduct(string Name)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Products.Remove(db.Products.FirstOrDefault(x => x.Name == Name));
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        
        public bool AddEmployee(string Name, string Surname, Department department)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Employees.Add(
                        new Employee()
                        {
                            Name = Name,
                            Surname = Surname,
                            Department = department
                        });
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
            
        }
        public bool RemoveEmployee(string Name, string Surname)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Employees.Remove(db.Employees.FirstOrDefault(x => (x.Name == Name && x.Surname == Surname)));
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddUser(string Login,string Password, string Name, string Surname, string PhoneNumber)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Users.Add(
                        new User()
                        {
                            Name = Name,
                            Surname = Surname,
                            Login = Login,
                            Password = Password,
                            PhoneNumber = PhoneNumber
                        });
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

        }
        public bool RemoveUser(string Login)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Users.Remove(db.Users.FirstOrDefault(x => x.Login == Login));
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}
