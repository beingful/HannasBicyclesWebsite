using System.Collections.Generic;
using System.Linq;
using BicycleShopDB.Tables;

namespace BicycleShop.Models
{
    public class Cart
    {
        public Cart()
        {
            lineCollection = new List<CartLine>();
        }

        private List<CartLine> lineCollection;

        public void AddItem(Bicycle bicycle, int quantity)
        {
            CartLine line = lineCollection
                                    .FirstOrDefault(x => x.Bicycle.BicycleId == bicycle.BicycleId);
            if (line is null)
            {
                lineCollection.Add(new CartLine
                {
                    Bicycle = bicycle,
                    Quantity = quantity
                });

            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(x => x.Bicycle.Total * x.Quantity);
        }

        public void RemoveLine(Bicycle bicycle)
        {
            lineCollection.RemoveAll(x => x.Bicycle.BicycleId == bicycle.BicycleId);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines => lineCollection;
    }
}
