using System.ComponentModel.DataAnnotations.Schema;

namespace BicycleShopDB.Tables
{
    public class Image
    {
        public int ImageId { get; set; }

        [ForeignKey("Bicycle")]
        public int BicycleId { get; set; }
        public string ImagePath { get; set; }
    }
}
