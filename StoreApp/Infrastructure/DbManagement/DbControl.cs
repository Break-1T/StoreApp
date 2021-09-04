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

        /// <summary>
        /// Добавляет в базу данных новый департамент
        /// </summary>
        /// <param name="Name">Имя департамента</param>
        /// <returns>bool</returns>
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
        
        /// <summary>
        /// Добавляет в базу данных новый департамент
        /// </summary>
        /// <param name="Name">Имя департамента</param>
        /// <returns>bool</returns>
        public async Task<bool> AddDepartamentAsync(string Name)
        {
            return await Task.Run(() => AddDepartament(Name));
        }

        /// <summary>
        /// Добавляет в базу данных новый департамент
        /// </summary>
        /// <param name="Name">Имя департамента</param>
        /// <param name="Image">Фото департамента</param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Добавляет в базу данных новый департамент
        /// </summary>
        /// <param name="Name">Имя департамента</param>
        /// <param name="Image">Фото департамента</param>
        /// <returns>bool</returns>
        public async Task<bool> AddDepartamentAsync(string Name, byte[] Image)
        {
            return await Task.Run(() => AddDepartament(Name, Image));
        }

        /// <summary>
        /// Удаляет департамент по заданному имени
        /// </summary>
        /// <param name="Name">Имя департамента</param>
        /// <returns>bool</returns>
        public bool RemoveDepartment(string Name)
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
        
        /// <summary>
        /// Удаляет департамент по заданному имени
        /// </summary>
        /// <param name="Name">Имя департамента</param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveDepartmentAsync(string Name)
        {
            return await Task.Run(() => RemoveDepartment(Name));
        }

        /// <summary>
        /// Удаляет департамент по его Id и Name
        /// </summary>
        /// <param name="id">Id департамента</param>
        /// <param name="Name">Имя департамента</param>
        /// <returns>bool</returns>
        public bool RemoveDepartment(int id, string Name)
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
        
        /// <summary>
        /// Удаляет департамент по его Id и Name
        /// </summary>
        /// <param name="id">Id департамента</param>
        /// <param name="Name">Имя департамента</param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveDepartmentAsync(int id, string Name)
        {
            return await Task.Run(() => RemoveDepartment(id, Name));
        }

        /// <summary>
        /// Добавляет новый продукт в базу данных
        /// </summary>
        /// <param name="Name">Имя продукта</param>
        /// <param name="Price">Цена продукта</param>
        /// <param name="Image">Фото продукта</param>
        /// <param name="category">Категория продукта</param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Добавляет новый продукт в базу данных
        /// </summary>
        /// <param name="Name">Имя продукта</param>
        /// <param name="Price">Цена продукта</param>
        /// <param name="Image">Фото продукта</param>
        /// <param name="category">Категория продукта</param>
        /// <returns>bool</returns>
        public async Task<bool> AddProductAsync(string Name, decimal Price, byte[] Image, Category category)
        {
            return await Task.Run(() => AddProduct(Name, Price, Image, category));
        }

        /// <summary>
        /// Добавляет новый продукт в базу данных
        /// </summary>
        /// <param name="Name">Имя продукта</param>
        /// <param name="Price">Цена продукта</param>
        /// <param name="category">Категория продукта</param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Добавляет новый продукт в базу данных
        /// </summary>
        /// <param name="Name">Имя продукта</param>
        /// <param name="Price">Цена продукта</param>
        /// <param name="category">Категория продукта</param>
        /// <returns>bool</returns>
        public async Task<bool> AddProductAsync(string Name, decimal Price, Category category)
        {
            return await Task.Run(() => AddProduct(Name, Price, category));
        }

        /// <summary>
        /// Удаляет продукт из базы данных
        /// </summary>
        /// <param name="Name">Имя продукта</param>
        /// <param name="Id">Id продукта</param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Удаляет продукт из базы данных
        /// </summary>
        /// <param name="Name">Имя продукта</param>
        /// <param name="Id">Id продукта</param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveProductAsync(string Name, int Id)
        {
            return await Task.Run(() => RemoveProduct(Name, Id));
        }

        /// <summary>
        /// Добавляет работника в базу данных
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <param name="accessLevel">Уровень доступа</param>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="Email">Почта</param>
        /// <param name="department">Департамент</param>
        /// <param name="PhoneNumber">Номер телефона</param>
        /// <returns>bool</returns>
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
        
        /// <summary>
        /// Добавляет работника в базу данных
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <param name="accessLevel">Уровень доступа</param>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="Email">Почта</param>
        /// <param name="department">Департамент</param>
        /// <param name="PhoneNumber">Номер телефона</param>
        /// <returns>bool</returns>
        public async Task<bool> AddEmployeeAsync(string Login, string Password, AccessLevel accessLevel, string Name,
            string Surname, string Email, Department department, string PhoneNumber)
        {
            return await Task.Run(() => AddEmployee(Login, Password, accessLevel, Name, Surname, Email, department,
                PhoneNumber));
        }

        /// <summary>
        /// Удаляет работника из базы данных
        /// </summary>
        /// <param name="id">Id работника</param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Удаляет работника из базы данных
        /// </summary>
        /// <param name="id">Id работника</param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveEmployeeAsync(int id)
        {
            return await Task.Run(() => RemoveEmployee(id));
        }

        /// <summary>
        /// Добавляет нового пользователя в базу данных
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="PhoneNumber">Номер телефона</param>
        /// <param name="Email">Почта</param>
        /// <param name="Image">Фото</param>
        /// <param name="AccessLevel">Уровень доступа</param>
        /// <returns>bool</returns>
        public bool AddUser(string Login, string Password, string Name, string Surname, string PhoneNumber,
            string Email,byte[] Image, AccessLevel AccessLevel)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User user = new User();
                    user.Name = Name;
                    user.Surname = Surname;
                    user.Login = Login;
                    user.Password = Password;
                    user.PhoneNumber = PhoneNumber;
                    user.Email = Email;
                    user.Image = Image;
                    if (AccessLevel!=null)
                    {
                        user.AccessLevel = db.AccessLevels.Include(x => x.Employees).Include(t => t.Users)
                                .FirstOrDefault(e => e.Id == AccessLevel.Id);
                    }

                    db.Users.Add(user);
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

        /// <summary>
        /// Добавляет нового пользователя в базу данных
        /// </summary>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="PhoneNumber">Номер телефона</param>
        /// <param name="Email">Почта</param>
        /// <param name="Image">Фото</param>
        /// <param name="AccessLevel">Уровень доступа</param>
        /// <returns>bool</returns>
        public async Task<bool> AddUserAsync(string Login, string Password, string Name, string Surname,
            string PhoneNumber, string Email, byte[] Image, AccessLevel AccessLevel)
        {
            return await Task.Run(() => AddUser(Login, Password, Name, Surname, PhoneNumber, Email,Image, AccessLevel));
        }

        /// <summary>
        /// Удаляет пользователя из базы данных
        /// </summary>
        /// <param name="Id">Имя пользователя</param>
        /// <returns>bool</returns>
        public bool RemoveUser(int Id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User user = db.Users.Include(t=>t.Orders).Include(e=>e.AccessLevel).FirstOrDefault(x => x.Id == Id);
                    if (user == null) return false;
                    db.Users.Remove(user);
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
        
        /// <summary>
        /// Удаляет пользователя из базы данных
        /// </summary>
        /// <param name="Id">Имя пользователя</param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveUserAsync(int Id)
        {
            return await Task.Run(() => RemoveUser(Id));
        }

        /// <summary>
        /// Добавляет новую категорию
        /// </summary>
        /// <param name="Name">Имя категории</param>
        /// <returns>bool</returns>
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
        
        /// <summary>
        /// Добавляет новую категорию
        /// </summary>
        /// <param name="Name">Имя категории</param>
        /// <param name="Image">Изоображение категории</param>
        /// <returns>bool</returns>
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
        
        /// <summary>
        /// Добавляет новую категорию
        /// </summary>
        /// <param name="Name">Имя категории</param>
        /// <returns>bool</returns>
        public async Task<bool> AddCategoryAsync(string Name)
        {
            return await Task.Run(() => AddCategory(Name));
        }
        
        /// <summary>
        /// Добавляет новую категорию
        /// </summary>
        /// <param name="Name">Имя категории</param>
        /// <param name="Image">Изоображение категории</param>
        /// <returns>bool</returns>
        public async Task<bool> AddCategoryAsync(string Name, byte[] Image)
        {
            return await Task.Run(() => AddCategory(Name, Image));
        }

        /// <summary>
        /// Удаляет категорию из базы данных
        /// </summary>
        /// <param name="CategoryId">Id категории</param>
        /// <param name="CategoryName">Имя категории</param>
        /// <returns>bool</returns>
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
        
        /// <summary>
        /// Удаляет категорию из базы данных
        /// </summary>
        /// <param name="CategoryId">Id категории</param>
        /// <param name="CategoryName">Имя категории</param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveCategoryAsync(int CategoryId, string CategoryName)
        {
            return await Task.Run(() => RemoveCategory(CategoryId, CategoryName));
        }

        /// <summary>
        /// Производит поиск работника по базе данных
        /// </summary>
        /// <param name="Login">Логин работника</param>
        /// <param name="Password">Пароль работника</param>
        /// <returns>new Employee()</returns>
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
        
        /// <summary>
        /// Производит поиск работника по базе данных
        /// </summary>
        /// <param name="Login">Логин работника</param>
        /// <param name="Password">Пароль работника</param>
        /// <returns>new Employee()</returns>
        public async Task<Employee> FindEmployeeAsync(string Login, string Password)
        {
            return await Task.Run(() => FindEmployee(Login, Password));
        }

        /// <summary>
        /// Изменяет свойства продукта в базе данных
        /// </summary>
        /// <param name="product">Продукт</param>
        /// <returns>bool</returns>
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
        
        /// <summary>
        /// Изменяет свойства продукта в базе данных
        /// </summary>
        /// <param name="product">Продукт</param>
        /// <returns>bool</returns>
        public async Task<bool> ChangeProductAsync(Product product)
        {
            return await Task.Run(() => ChangeProduct(product));
        }

        /// <summary>
        /// Изменяет свойства работника в базе данных
        /// </summary>
        /// <param name="employee">Работник</param>
        /// <returns>bool</returns>
        public bool ChangeEmployee(Employee employee)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var tmp = db.Employees.Include(x => x.Department).Include(e=>e.AccessLevel).FirstOrDefault(t => t.Id == employee.Id);
                    
                    if (employee.AccessLevel!=null)
                        tmp.AccessLevel = db.AccessLevels.Include(x => x.Employees).FirstOrDefault(t => t.Id == employee.AccessLevel.Id);
                    
                    if (employee.Department != null)
                        tmp.Department = db.Departments.Include(x => x.Employees)
                            .FirstOrDefault(t => t.Id == employee.Department.Id);

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
                MessageBox.Show(e.Message);
                return false;
            }
        }
        
        /// <summary>
        /// Изменяет свойства работника в базе данных
        /// </summary>
        /// <param name="employee">Работник</param>
        /// <returns>bool</returns>
        public async Task<bool> ChangeEmployeeAsync(Employee employee)
        {
            return await Task.Run(() => ChangeEmployee(employee));
        }

        /// <summary>
        /// Добавляет новый уровень доступа в базу данных
        /// </summary>
        /// <param name="Name">Имя уровня доступа</param>
        /// <returns>bool</returns>
        public bool AddAccessLevel(string Name)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (!string.IsNullOrEmpty(Name))
                    {
                        db.AccessLevels.Add(new AccessLevel() { Name = Name });
                        db.SaveChanges();
                        return true;
                    }
                    throw new Exception("String is null or empty!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Добавляет новый уровень доступа в базу данных
        /// </summary>
        /// <param name="Name">Имя уровня доступа</param>
        /// <returns>bool</returns>
        public async Task<bool> AddAccessLevelAsync(string Name)
        {
            return await Task.Run(() => AddAccessLevel(Name));
        }

        /// <summary>
        /// Удаляет уровень доступа из базы данных
        /// </summary>
        /// <param name="Id">Id уровня доступа</param>
        /// <returns>bool</returns>
        public bool RemoveAccessLevel(int Id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    AccessLevel accessLevel = db.AccessLevels.Include(x => x.Employees).Include(t => t.Users)
                        .FirstOrDefault(a => a.Id == Id);
                    if (accessLevel!=null)
                    {
                        db.AccessLevels.Remove(accessLevel);
                        db.SaveChanges();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Удаляет уровень доступа из базы данных
        /// </summary>
        /// <param name="Id">Id уровня доступа</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteAccessLevelAsync(int Id)
        {
            return await Task.Run(() => RemoveAccessLevel(Id));
        }

        /// <summary>
        /// Изменяет свойства уровня доступа в базе данных
        /// </summary>
        /// <param name="id">Id уровня доступа</param>
        /// <param name="Name">Имя уровня доступа</param>
        /// <returns>bool</returns>
        public bool ChangeAccessLevel(int id,string Name)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    AccessLevel accessLevel = db.AccessLevels.Include(x => x.Users).Include(t => t.Employees)
                        .FirstOrDefault(i => i.Id == id);
                    if (accessLevel!=null)
                    {
                        accessLevel.Name = Name;
                        db.SaveChanges();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Изменяет свойства уровня доступа в базе данных
        /// </summary>
        /// <param name="id">Id уровня доступа</param>
        /// <param name="Name">Имя уровня доступа</param>
        /// <returns>bool</returns>
        public async Task<bool> ChangeAccessLevelAsync(int id, string Name)
        {
            return await Task.Run(() => ChangeAccessLevel(id, Name));
        }

        /// <summary>
        /// Изменяет значения пользователя в базе данных
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>bool</returns>
        public bool ChangeUser(User user)
        {
            try
            {
                using (var db = new ApplicationContext())
                {
                    var tmp = db.Users.Include(x =>x.Orders).Include(e => e.AccessLevel).FirstOrDefault(t => t.Id == user.Id);

                    if (user.AccessLevel != null)
                        tmp.AccessLevel = db.AccessLevels.Include(x => x.Employees).Include(e=>e.Users).FirstOrDefault(t => t.Id == user.AccessLevel.Id);

                    tmp.Email = !string.IsNullOrEmpty(user.Email)? user.Email: tmp.Email;
                    tmp.Image = user.Image;
                    tmp.Login = user.Login;
                    tmp.Name = user.Name;
                    tmp.PhoneNumber = user.PhoneNumber;
                    tmp.Surname = user.Surname;
                    tmp.Password = user.Password;
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Изменяет значения пользователя в базе данных
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>bool</returns>
        public async Task<bool> ChangeUserAsync(User user) => await Task.Run(() => ChangeUser(user));

        /// <summary>
        /// Добавляет новый заказ в базу данных
        /// </summary>
        /// <param name="order">Объект типа Order</param>
        /// <returns>bool</returns>
        public bool AddOrder(Order order)
        {
            try
            {
                using (ApplicationContext db = new())
                {
                    order.Product = db.Products.Include(x => x.Category).FirstOrDefault(p=>p.Id==order.Product.Id);
                    order.User = db.Users.Include(x => x.AccessLevel).Include(e => e.Orders)
                        .FirstOrDefault(u => u.Id == order.User.Id);
                    db.Orders.Add(new Order()
                    {
                        Date = order.Date,
                        Product = order.Product,
                        User = order.User
                    });
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Добавляет новый заказ в базу данных
        /// </summary>
        /// <param name="order">Объект типа Order</param>
        /// <returns>bool</returns>
        public async Task<bool> AddOrderAsync(Order order)
        {
            return await Task.Run(() => AddOrder(order));
        }
    }
}
