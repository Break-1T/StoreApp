using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using StoreApp.Infrastructure.StoreManagement;
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

        public async Task<bool> AddDepartamentAsync(string Name)
        {
            return await Task.Run(() => AddDepartament(Name));
        }

        public bool AddDepartament(string Name, byte[] Image)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Departments.Add(
                        new Department()
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

        public async Task<bool> AddDepartamentAsync(string Name, byte[] Image)
        {
            return await Task.Run(() => AddDepartament(Name, Image));
        }

        public bool RemoveDepartament(string Name)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var departament = db.Departments.Where(x => x.Name.ToLower() == Name.ToLower()).FirstOrDefault();
                    db.Entry(departament).Collection(c => c.Employees);
                    db.Departments.Remove(departament);
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

        public async Task<bool> RemoveDepartamentAsync(string Name)
        {
            return await Task.Run(() => RemoveDepartament(Name));
        }

        public bool RemoveDepartament(int id, string Name)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var departament = db.Departments.Where(x => x.Name.ToLower() == Name.ToLower() && x.Id == id)
                        .FirstOrDefault();
                    db.Entry(departament).Collection(c => c.Employees);
                    db.Departments.Remove(departament);
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

        public async Task<bool> RemoveDepartamentAsync(int id, string Name)
        {
            return await Task.Run(() => RemoveDepartament(id, Name));
        }

        public bool AddProduct(string Name, decimal Price, byte[] Image, Category category)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Products.Add(
                        new Product()
                        {
                            Name = Name,
                            Image = Image,
                            Category = db.Categories.FirstOrDefault(x => x.Id == category.Id),
                            Price = Price
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

        public async Task<bool> AddProductAsync(string Name, decimal Price, byte[] Image, Category category)
        {
            return await Task.Run(() => AddProduct(Name, Price, Image, category));
        }

        public bool AddProduct(string Name, decimal Price, Category category)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Products.Add(
                        new Product()
                        {
                            Name = Name,
                            Category = db.Categories.FirstOrDefault(x => x.Id == category.Id),
                            Price = Price
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

        public async Task<bool> AddProductAsync(string Name, decimal Price, Category category)
        {
            return await Task.Run(() => AddProduct(Name, Price, category));
        }

        public bool RemoveProduct(string Name, int Id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Products.Remove(db.Products.FirstOrDefault(x => x.Name == Name && x.Id == Id));
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

        public async Task<bool> RemoveProductAsync(string Name, int Id)
        {
            return await Task.Run(() => RemoveProduct(Name, Id));
        }

        public bool AddEmployee(string Login, string Password, AccessLevel accessLevel, string Name, string Surname,
            string Email, Department department, string PhoneNumber)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var emp = new Employee();
                    emp.Name = Name;
                    emp.Surname = Surname;
                    emp.Email = Email;
                    emp.AccessLevel = accessLevel!=null? db.AccessLevels.FirstOrDefault(x => x.Id == accessLevel.Id):null;
                    emp.Login = Login;
                    emp.Password = Password;
                    emp.PhoneNumber = PhoneNumber;
                    emp.Department = department != null
                        ? db.Departments.FirstOrDefault(x => x.Id == department.Id)
                        : null;
                    db.Employees.Add(emp);
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

        public async Task<bool> AddEmployeeAsync(string Login, string Password, AccessLevel accessLevel, string Name,
            string Surname, string Email, Department department, string PhoneNumber)
        {
            return await Task.Run(() => AddEmployee(Login, Password, accessLevel, Name, Surname, Email, department,
                PhoneNumber));
        }

        public bool RemoveEmployee(int id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Employees.Remove(db.Employees.FirstOrDefault(x => x.Id==id));
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        public async Task<bool> RemoveEmployeeAsync(int id)
        {
            return await Task.Run(() => RemoveEmployee(id));
        }

        public bool AddUser(string Login, string Password, string Name, string Surname, string PhoneNumber,
            string Email)
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
                            Email = Email
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
        public async Task<bool> AddUserAsync(string Login, string Password, string Name, string Surname,
            string PhoneNumber, string Email)
        {
            return await Task.Run(() => AddUser(Login, Password, Name, Surname, PhoneNumber, Email));
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

        public async Task<bool> RemoveUserAsync(string Login)
        {
            return await Task.Run(() => RemoveUser(Login));
        }

        public bool AddCategory(string Name)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Categories.Add(new Category()
                    {
                        Name = Name
                    });
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddCategory(string Name, byte[] Image)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Categories.Add(new Category()
                    {
                        Name = Name,
                        Image = Image
                    });
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> AddCategoryAsync(string Name)
        {
            return await Task.Run(() => AddCategory(Name));
        }

        public async Task<bool> AddCategoryAsync(string Name, byte[] Image)
        {
            return await Task.Run(() => AddCategory(Name, Image));
        }

        public bool RemoveCategory(int CategoryId, string CategoryName)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var category = db.Categories.Where(x => x.Name == CategoryName && x.Id == CategoryId)
                        .FirstOrDefault();
                    db.Entry(category).Collection(c => c.Products);
                    db.Categories.Remove(category);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> RemoveCategoryAsync(int CategoryId, string CategoryName)
        {
            return await Task.Run(() => RemoveCategory(CategoryId, CategoryName));
        }

        public Employee FindEmployee(string Login, string Password)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var result = db.Employees.FirstOrDefault(x => x.Login == Login && x.Password == Password);
                    return result;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<Employee> FindEmployeeAsync(string Login, string Password)
        {
            return await Task.Run(() => FindEmployee(Login, Password));
        }

        public bool ChangeProduct(Product product)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var prod = db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == product.Id);
                    prod.Image = product.Image;
                    prod.Category = db.Categories.Include(x => x.Products)
                        .FirstOrDefault(i => i.Id == product.Category.Id);
                    prod.Name = product.Name;
                    prod.Price = product.Price;
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> ChangeProductAsync(Product product)
        {
            return await Task.Run(() => ChangeProduct(product));
        }

        public bool ChangeEmployee(Employee employee)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var tmp = db.Employees.Include(x => x.Department).FirstOrDefault(t => t.Id == employee.Id);
                    tmp.AccessLevel = employee.AccessLevel;
                    tmp.Department = employee.Department;
                    tmp.Email = employee.Email;
                    tmp.Image = employee.Image;
                    tmp.Login = employee.Login;
                    tmp.Name = employee.Name;
                    tmp.PhoneNumber = employee.PhoneNumber;
                    tmp.Surname = employee.Surname;
                    tmp.Password = employee.Password;
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public async Task<bool> ChangeEmployeeAsync(Employee employee)
        {
            return await Task.Run(() => ChangeEmployee(employee));
        }
    }
}
