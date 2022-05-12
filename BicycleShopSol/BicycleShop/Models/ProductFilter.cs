using System.Collections.Generic;
using System.Linq;
using BicycleShopDB.Tables;

namespace BicycleShop.Models
{
    public class ProductFilter
    {
        public bool Discount { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public List<FilterCategory> FilterCategories { get; set; }

        public void CreateFilterCategory(string categoryName, List<string> selections)
        {
            var category = new FilterCategory 
            { 
                Name = categoryName, 
                Selections = new List<FilterSelection>() 
            };

            foreach (var selection in selections.OrderBy(el => el))
            {
                category.Selections.Add(new FilterSelection 
                { 
                    Name = selection, 
                    IsChecked = false 
                });
            }

            FilterCategories.Add(category);
        }

        public void SetFilterCategory(string categoryName, string checkedSelections, List<string> allSelections)
        {
            if (string.IsNullOrEmpty(checkedSelections))
            {
                CreateFilterCategory(categoryName, allSelections);
            }
            else
            {
                var category = new FilterCategory 
                { 
                    Name = categoryName, 
                    Selections = new List<FilterSelection>() 
                };

                List<string> selectionNames = checkedSelections.Split(',').ToList();

                bool isChecked = false;

                foreach (var selection in allSelections)
                {
                    if (selectionNames.Contains(selection.ToLower().Replace(' ', '-').Replace(",", "")))
                    {
                        isChecked = true;
                    }
                    else
                    {
                        isChecked = false;
                    }

                    category.Selections.Add(new FilterSelection 
                    { 
                        Name = selection, 
                        IsChecked = isChecked 
                    });
                }

                FilterCategories.Add(category);
            }
        }

        public void Filter(Dictionary<string, string> filters, ref List<Bicycle> bicycles)
        {
            List<string> brandSelections = filters
                                                .Where(el => el.Value is "Brand")
                                                                            .Select(el => el.Key)
                                                                                                .ToList();

            List<string> typeSelections = filters
                                                .Where(el => el.Value is "Type")
                                                                            .Select(el => el.Key)
                                                                                                .ToList();

            List<string> brakeSelections = filters
                                                .Where(el => el.Value is "Brake")
                                                                            .Select(el => el.Key)
                                                                                                .ToList();

            if (brandSelections.Count != 0)
            {
                bicycles = bicycles
                                .Where(bicycle => brandSelections.Contains(bicycle.Brand))
                                                                                        .ToList();
            }

            if (typeSelections.Count != 0)
            {
                bicycles = bicycles
                                .Where(bicycle => typeSelections.Contains(bicycle.Type))
                                                                                        .ToList();
            }

            if (brakeSelections.Count != 0)
            {
                bicycles = bicycles
                                .Where(bicycle => brakeSelections.Contains(bicycle.BrakeType))
                                                                                            .ToList();
            }

            if (Discount is true)
            {
                bicycles = bicycles
                                .Where(bicycle => bicycle.Discount > 0)
                                                                    .ToList();
            }

            bicycles = bicycles
                            .Where(bicycle => bicycle.Total >= PriceFrom && bicycle.Total <= PriceTo)
                                                                                                    .ToList();
        }
    }
}
