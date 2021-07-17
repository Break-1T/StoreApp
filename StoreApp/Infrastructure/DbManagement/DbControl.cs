using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.MVVM.Model;

namespace StoreApp.Infrastructure.DbManagement
{
    class DbControl
    {
        public DbControl(){}

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
                    db.SaveChangesAsync();
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
                    db.SaveChangesAsync();
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
                            //Image = Image
                        });
                    db.SaveChangesAsync();
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
                    db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
        
        public bool AddEmployee(string Login, string Password, string accessLevel, string Name, string Surname, string Email,
            Department department, string PhoneNumber)
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
                            Department = department,
                            Email = Email,
                            AccessLevel=accessLevel,
                            Login=Login,
                            Password=Password,
                            PhoneNumber=PhoneNumber
                        });
                    db.SaveChangesAsync();
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
                    db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddUser(string Login,string Password, string Name, string Surname, string PhoneNumber,string Email)
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
                            PhoneNumber = PhoneNumber,
                            Email=Email
                        });
                    db.SaveChangesAsync();
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
                    db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public void AddCategory(string Name)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Categories.Add(new Category()
                    {
                        Name=Name
                    });
                    db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        public void AddCategory(string Name,byte[] Image)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Categories.Add(new Category()
                    {
                        Name = Name,
                        Image=Image
                    });
                    db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async void AddCategoryAsync(string Name)
        {
            await Task.Run(() => AddCategory(Name));
        }
        public async void AddCategoryAsync(string Name, byte[] Image)
        {
            await Task.Run(() => AddCategory(Name,Image));
        }

        public Employee FindEmployee(string Login, string Password)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var result = db.Employees.FirstOrDefault(x=>x.Login==Login && x.Password==Password);
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
