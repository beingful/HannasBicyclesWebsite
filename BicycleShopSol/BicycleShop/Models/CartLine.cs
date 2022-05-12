using BicycleShopDB.Tables;

namespace BicycleShop.Models
{
    public class CartLine
    {
        public Bicycle Bicycle { get; set; }
        public int Quantity { get; set; }
    }
}
