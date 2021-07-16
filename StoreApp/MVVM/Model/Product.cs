using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Controls;
using StoreApp.Resources;

namespace StoreApp.MVVM.Model
{
    class Product
    {
        public Product()
        {
            Image = Images.Empty_goods_icon;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}
