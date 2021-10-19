using System;
using System.Collections.Generic;
using System.Linq;
using BicycleShopDB.Tables;

namespace BicycleShop.Models
{
    public class Sorting 
    {
        private Dictionary<int, Func<List<Bicycle>, List<Bicycle>>> _sortMethods;

        public Sorting()
        {
            SortTypesQuantity = Enum.GetValues<SortType>().Length;

            _sortMethods = new Dictionary<int, Func<List<Bicycle>, List<Bicycle>>>
            {
                { (int)SortType.NameAsc, 
                    (bicycles) => 
                    {
                        return bicycles.OrderBy(bicycle => bicycle.BicycleName).ToList();
                    } 
                },
                { (int)SortType.NameDesc,
                    (bicycles) =>
                    {
                        return bicycles.OrderByDescending(bicycle => bicycle.BicycleName).ToList();
                    }
                },
                { (int)SortType.PriceAsc,
                    (bicycles) =>
                    {
                        return bicycles.OrderBy(bicycle => bicycle.Price).ToList();
                    }
                },
                { (int)SortType.PriceDesc,
                    (bicycles) =>
                    {
                        return bicycles.OrderByDescending(bicycle => bicycle.Price).ToList();
                    }
                },
                { (int)SortType.SpeedAsc,
                    (bicycles) =>
                    {
                        return bicycles.OrderBy(bicycle => bicycle.SpeedQuantity).ToList();
                    }
                },
                { (int)SortType.SpeedDesc,
                    (bicycles) =>
                    {
                        return bicycles.OrderByDescending(bicycle => bicycle.SpeedQuantity).ToList();
                    }
                },
                { (int)SortType.YearAsc,
                    (bicycles) =>
                    {
                        return bicycles.OrderBy(bicycle => bicycle.ReleaseYear).ToList();
                    }
                },
                { (int)SortType.YearDesc,
                    (bicycles) =>
                    {
                        return bicycles.OrderByDescending(bicycle => bicycle.ReleaseYear).ToList();
                    }
                }
            };
        }

        private enum SortType
        {
            NameAsc,
            NameDesc,
            PriceAsc,
            PriceDesc,
            YearAsc,
            YearDesc,
            SpeedAsc,
            SpeedDesc
        }

        public int SortTypeIndex { get; private set; }
        public int SortTypesQuantity { get; private set; }

        public void SetSortTypeIndex(string sortCategory, string sortDirection)
        {
            if (string.IsNullOrEmpty(sortCategory) && string.IsNullOrEmpty(sortDirection))
            {
                SortTypeIndex = (int)SortType.NameAsc;
            }

            else
            {
                if (string.IsNullOrEmpty(sortCategory))
                {
                    sortCategory = "Name";
                }

                else if (string.IsNullOrEmpty(sortDirection))
                {
                    sortDirection = "Asc";
                }

                SortType sort;

                string sortType = sortCategory.Replace(
                sortCategory[0].ToString(),
                sortCategory[0].ToString().ToUpper()
                ) +
                sortDirection.Replace(
                    sortDirection[0].ToString(),
                    sortDirection[0].ToString().ToUpper()
                    );

                Enum.TryParse(sortType, out sort);

                SortTypeIndex = (int)sort;
            }
        }

        public string GetSortCategoryName(int index)
        {
            var sort = (SortType)Enum.GetValues<SortType>().GetValue(index);

            string sortTypeName = Enum.GetName<SortType>(sort);

            if (sortTypeName.EndsWith("Asc"))
            {
                return sortTypeName.Replace("Asc", "");
            }

            return sortTypeName.Replace("Desc", "");
        }

        public List<Bicycle> Sort(List<Bicycle> bicycles)
        {
            return _sortMethods.GetValueOrDefault(SortTypeIndex)(bicycles);
        }
    }
}
