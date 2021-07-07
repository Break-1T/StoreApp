using System;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.MVVM.Model
{
    class Order
    {
        [Key]
        public int Id { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime Date { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
