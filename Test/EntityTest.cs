using System;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.MVVM.Model;

namespace Test
{
    [RankColumn,MemoryDiagnoser]
    public class EntityTest
    {
        [Benchmark]
        public void ShowWindow()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var list = db.Users.Include(x => x.AccessLevel).Include(x => x.Orders).ToList();
            }
        }

        [Benchmark]
        public void FillUsers()
        {
            try
            {
                using (ApplicationContext db = new())
                {
                    var  users = new ObservableCollection<User>(db.Users.Include(e => e.AccessLevel).Select(x => new User()
                    {
                        AccessLevel = x.AccessLevel != null ? new AccessLevel { Id = x.AccessLevel.Id, Name = x.AccessLevel.Name } : null,
                        Orders = null,
                        Image = new byte[1],
                        Email = x.Email,
                        Id = x.Id,
                        Login = x.Login,
                        Name = x.Name,
                        Password = x.Password,
                        PhoneNumber = x.PhoneNumber,
                        Surname = x.Surname
                    }));
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
