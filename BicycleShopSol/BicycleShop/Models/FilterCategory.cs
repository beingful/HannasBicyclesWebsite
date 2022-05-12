using System.Collections.Generic;

namespace BicycleShop.Models
{
    public class FilterCategory
    {
        public string Name { get; set; }
        public List<FilterSelection> Selections { get; set; }
    }
}
