﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using StoreApp.Annotations;
using Test.Resources;

namespace StoreApp.MVVM.Model
{
    class Category
    {
        public Category()
        {
            Image = Images.Empty_category_icon;
            Products = new ObservableCollection<Product>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [CanBeNull]
        public byte[] Image { get; set; }
        
        public ObservableCollection<Product> Products { get; set; }
    }
}
