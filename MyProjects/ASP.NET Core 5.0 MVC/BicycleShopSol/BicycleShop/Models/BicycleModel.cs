using System.ComponentModel.DataAnnotations;
using BicycleShopDB.Tables;

namespace BicycleShop.Models
{
    public class BicycleModel
    {
        public BicycleModel() { }

        public BicycleModel(Bicycle bicycle)
        {
            BicycleId = bicycle.BicycleId;
            BicycleName = bicycle.BicycleName;
            Brand = bicycle.Brand;
            Type = bicycle.Type;
            ReleaseYear = bicycle.ReleaseYear;
            FrameMaterial = bicycle.FrameMaterial;
            WheelSize = bicycle.WheelSize;
            BrakeType = bicycle.BrakeType;
            SpeedQuantity = bicycle.SpeedQuantity;
            MaxWeight = bicycle.MaxWeight;
            Price = bicycle.Price;
            Discount = bicycle.Discount;
            Total = bicycle.Total;
            Quantity = bicycle.Quantity;
            ImagePath = bicycle.ImagePath;
        }

        public int BicycleId { get; set; }
        [Required]
        public string BicycleName { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [RegularExpression("^20(([0-1]\\d){1}|((2[0-1]){1}))$", ErrorMessage = "Release year's not valid.")]
        public int ReleaseYear { get; set; }
        public string FrameMaterial { get; set; }
        [Required]
        [RegularExpression("^[1-2]\\d{1}(,5)?$", ErrorMessage = "Wheel size's not valid.")]
        public double WheelSize { get; set; }
        public string BrakeType { get; set; }
        [Required]
        [RegularExpression("^[1-9]{1}\\d?$", ErrorMessage = "Speed quantity's not valid.")]
        public int SpeedQuantity { get; set; }
        [RegularExpression("^1\\d{2}$", ErrorMessage = "Maximum weight's not valid.")]
        public int? MaxWeight { get; set; }
        [Required]
        [RegularExpression("^[1-9]\\d+$", ErrorMessage = "Price's not valid.")]
        public int Price { get; set; }
        [Required]
        [RegularExpression("(^0$)|(^[1-9]\\d?$)", ErrorMessage = "Discount's not valid.")]
        public int Discount { get; set; }
        public int Total { get; set; }
        [Required]
        [RegularExpression("(^0$)|(^[1-9]\\d*$)", ErrorMessage = "Quantity's not valid.")]
        public int Quantity { get; set; }
        [Required]
        [RegularExpression("(http(s?):)([/|.|\\w|\\s|-])*\\.(?:jpg|gif|png)", ErrorMessage = "Image path's not valid.")]
        public string ImagePath { get; set; }
    }
}
