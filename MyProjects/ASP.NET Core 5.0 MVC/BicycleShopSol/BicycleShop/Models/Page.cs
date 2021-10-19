using System;

namespace BicycleShop.Models
{
    public class Page
    {
        public Page(int pageNumber, int itemsQuantity)
        {
            Number = pageNumber;
            TotalQuantity = (int)Math.Ceiling(itemsQuantity / (double)ContentQuantity);
        }

        public int ContentQuantity { get; } = 6;
        public int Number { get; private set; }
        public int TotalQuantity { get; private set; }

        public bool HasPreviousPage
        {
            get => Number > 1;
        }

        public bool HasNextPage
        {
            get => Number < TotalQuantity;
        }
    }
}
