using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using BicycleShopDB;
using BicycleShopDB.Tables;
using BicycleShop.Services;
using Newtonsoft.Json;
using BicycleShop.Models;
using BicycleShopDB.Repository;

namespace BicycleShop.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly BicycleContext _context;
        private readonly Email _email;

        public ShoppingController(BicycleContext context, Email email)
        {
            _context = context;
            _email = email;
        }

        public IActionResult Buy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buy(Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                var receipt = JsonConvert.DeserializeObject<Receipt>(TempData["receipt"].ToString());
                receipt.Buyer = buyer;

                TempData.Remove("receipt");

                AddOrderToDB(receipt);

                var bicycleRepository = new BicycleRepository(_context);

                foreach (var line in receipt.Cart.Lines)
                {
                    bicycleRepository.UpdateQuantity(line.Bicycle.BicycleId, line.Quantity);
                }

                _email.SendEmailGoodIsBought(receipt);

                TempData["success"] = "Your purchase was successful!";

                return RedirectToAction("ClearCart", "Cart");
            }

            return View();
        }

        private void AddOrderToDB(Receipt receipt)
        {
            var orderRepos = new OrderRepository(_context);

            foreach (var line in receipt.Cart.Lines)
            {
                orderRepos.Insert(new Order
                {
                    UserId = _context
                                    .Users
                                        .First(user => user.Email == receipt.Buyer.Email)
                                                                                        .Id,
                    BicycleId = line.Bicycle.BicycleId,
                    Name = receipt.Buyer.Name,
                    Lastname = receipt.Buyer.Lastname,
                    Email = receipt.Buyer.Email,
                    ItemsQuantity = line.Quantity,
                    PurchaseDate = DateTime.ParseExact(DateTime.Now.ToString("d"), "dd/MM/yyyy", null),
                    Price = line.Quantity * line.Bicycle.Total
                });
            }
        }
    }
}
