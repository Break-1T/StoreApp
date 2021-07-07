using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Controls;

namespace StoreApp.MVVM.Model
{
    class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //public Image? Image { get; set; }
    }
}
