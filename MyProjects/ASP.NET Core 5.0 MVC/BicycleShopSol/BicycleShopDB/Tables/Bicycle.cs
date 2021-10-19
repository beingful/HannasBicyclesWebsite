using System.Collections.Generic;

namespace BicycleShopDB.Tables
{
    public class Bicycle
    {
        public int BicycleId { get; set; }
        public string BicycleName { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public int ReleaseYear { get; set; }
        public string FrameMaterial { get; set; }
        public double WheelSize { get; set; }
        public string BrakeType { get; set; }
        public int SpeedQuantity { get; set; }
        public int? MaxWeight { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Total { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Image> Images { get; set; }
    }
}
