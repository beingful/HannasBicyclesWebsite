using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BicycleShopDB.Tables
{
    public class Order
    {
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Bicycle")]
        public int BicycleId { get; set; }
        public virtual Bicycle Bicycle { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int ItemsQuantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Price { get; set; }
    }
}
