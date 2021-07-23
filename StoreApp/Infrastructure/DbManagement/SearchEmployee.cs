using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using StoreApp.Annotations;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.MVVM.Model;

namespace StoreApp.Infrastructure.DbManagement
{
    class SearchEmployee
    {
        public SearchEmployee()
        {
            Id = "";
            Name = "";
            Surname = "";
            Email = "";
            Login = "";
            PhoneNumber = "";
            Password = "";
            AccessLevel = new AccessLevel();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
