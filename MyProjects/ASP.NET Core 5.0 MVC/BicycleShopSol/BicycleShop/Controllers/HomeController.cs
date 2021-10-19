using BicycleShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BicycleShopDB.Repository;
using BicycleShopDB;
using BicycleShopDB.Tables;
using BicycleShop.Services;
using Microsoft.AspNetCore.Http;

namespace BicycleShop.Controllers
{
   [ResponseCache(NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        private readonly BicycleContext _context;
        private readonly Email _email;

        public HomeController(BicycleContext context, Email email)
        {
            _context = context;
            _email = email;
        }

        public IActionResult Index(int? id, string brand, string type, string brake, string sortCategory, 
                                    string sortDirection, string discount, string price, string search)
        {
            var bicycleRepository = new BicycleRepository(_context);

            List<Bicycle> bicycles = bicycleRepository.GetAll().ToList();

            ViewBag.HighestPrice = bicycles.Max(bicycle => bicycle.Total);
            ViewBag.Search = search;

            var filter = new ProductFilter { 
                PriceFrom = string.IsNullOrEmpty(price) ? 
                                                            0 : 
                                                                Convert.ToInt32(price.Split('-')[0]),
                PriceTo = string.IsNullOrEmpty(price) ?
                                                bicycles.Max(bicycle => bicycle.Total) :
                                                                                Convert.ToInt32(price.Split('-')[1]),
                Discount = string.IsNullOrEmpty(discount) ? false : true,
                FilterCategories = new List<FilterCategory>() 
            };

            List<string> allBrands = bicycles
                                        .OrderBy(bicycle => bicycle.Brand)
                                                                    .Select(x => x.Brand)
                                                                                    .Distinct()
                                                                                            .ToList();
            filter.SetFilterCategory("Brand", brand, allBrands);

            List<string> allTypes = bicycles
                                        .OrderBy(bicycle => bicycle.Type)
                                                                    .Select(x => x.Type)
                                                                                    .Distinct()
                                                                                            .ToList();
            filter.SetFilterCategory("Type", type, allTypes);

            List<string> allBrakes = bicycles
                                        .OrderBy(bicycle => bicycle.BrakeType)
                                                    .Select(x => x.BrakeType)
                                                                .Where(name => !string.IsNullOrEmpty(name))
                                                                                                        .Distinct()
                                                                                                                .ToList();
            filter.SetFilterCategory("Brake", brake, allBrakes);

            var categoryFilters = new Dictionary<string, string>();

            foreach (var category in filter.FilterCategories)
            {
                foreach (var selection in category.Selections)
                {
                    if (selection.IsChecked)
                    {
                        categoryFilters.Add(selection.Name, category.Name);
                    }
                }
            }

            filter.Filter(categoryFilters, ref bicycles);

            Sorting sorting = new Sorting();

            sorting.SetSortTypeIndex(sortCategory, sortDirection);
            bicycles = sorting.Sort(bicycles);

            if (!string.IsNullOrEmpty(search))
            {
                bicycles = bicycles
                                .Where(bicycle => bicycle.BicycleName.ToLower().Contains(search.ToLower()))
                                                                                                        .ToList();
            }

            var page = new Page(id ?? 1, bicycles.Count);

            var pageContent = new IndexViewModel
            {
                Page = page,
                Filter = filter,
                Sorting = sorting,
                TotalBicycleQuantity = bicycles.Count,
                Bicycles = bicycles
                                .Skip((page.Number - 1) * page.ContentQuantity)
                                                                        .Take(page.ContentQuantity)
                                                                                                .ToList()
            };

            if (TempData.ContainsKey("success"))
            {
                ViewBag.Success = TempData["success"] as string;
                TempData.Remove("success");
            } 

            return View(pageContent);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return RedirectToPage("Index");
            }

            var bicycleRepos = new BicycleRepository(_context);

            Bicycle bicycle = bicycleRepos.GetById(id);

            return View(bicycle);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
