﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StoreApp.MVVM.Model
{
    class Department
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }

        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; }
        
    }
}
