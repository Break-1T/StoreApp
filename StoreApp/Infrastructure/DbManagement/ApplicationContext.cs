using System.Configuration;
using Microsoft.EntityFrameworkCore;
using StoreApp.MVVM.Model;

namespace StoreApp.Infrastructure.DbManagement
{
    class ApplicationContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString);
        }
    }
}
