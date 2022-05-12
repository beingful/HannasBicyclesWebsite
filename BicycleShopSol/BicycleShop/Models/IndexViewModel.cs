using BicycleShopDB.Tables;
using System.Collections.Generic;

namespace BicycleShop.Models
{
    public class IndexViewModel
    {
        public List<Bicycle> Bicycles { get; set; }
        public ProductFilter Filter { get; set; }
        public Sorting Sorting { get; set; }
        public Page Page { get; set; }
        public int TotalBicycleQuantity { get; set; }
    }
}
