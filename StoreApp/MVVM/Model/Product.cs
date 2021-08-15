using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using StoreApp.Annotations;
using StoreApp.Infrastructure.Interfaces;
using StoreApp.Resources;

namespace StoreApp.MVVM.Model
{
    class Product : ClassWithImage, INotifyPropertyChanged
    {
        [NotNull] private Category _category;

        public Product()
        {
            Image = Images.Empty_goods_icon;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required, NotNull]
        public Category Category
        {
            get => _category;
            set
            {
                if (Equals(value, _category)) return;
                _category = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
