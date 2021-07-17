using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using StoreApp.Resources;

namespace StoreApp.MVVM.Model
{
    class Category
    {
        public Category()
        {
            Image = Images.Empty_category_icon;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public List<Product> Products { get; set; }
    }
}
