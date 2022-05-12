using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BicycleShopDB;
using BicycleShop.Models;
using BicycleShop.Extensions;
using BicycleShopDB.Tables;
using Newtonsoft.Json;
using BicycleShopDB.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BicycleShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly BicycleContext _context;
        private readonly string _key;

        public CartController(BicycleContext context, IHttpContextAccessor httpContextAccessor)
        {

            _context = context;
            _key = httpContextAccessor.HttpContext.Session.GetString("user");
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        Cart GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(_key);

            if (cart is null)
            {
                cart = new Cart();

                HttpContext.Session.SetObjectAsJson(_key, cart);
            }

            var receipt = new Receipt() 
            { 
                Cart = cart 
            };

            TempData["receipt"] = JsonConvert.SerializeObject(receipt);

            return cart;
        }

        public IActionResult AddToCart(int id, string returnUrl, int quantity)
        {
            var bicycleRepository = new BicycleRepository(_context);

            Bicycle bicycle = bicycleRepository.GetById(id);

            if (bicycle != null)
            {
                var cart = GetCart();
                cart.AddItem(bicycle, quantity);

                bicycleRepository.UpdateQuantity(id, quantity);

                HttpContext.Session.SetObjectAsJson(_key, cart);
            }

            return RedirectToAction("Index", new { returnUrl = returnUrl });
        }

        public IActionResult AddToCartAndRedirect(int id, int quantity, string returnUrl)
        {
            var bicycleRepository = new BicycleRepository(_context);

            Bicycle bicycle = bicycleRepository.GetById(id);

            if (bicycle != null)
            {
                var cart = GetCart();
                cart.AddItem(bicycle, quantity);

                bicycleRepository.UpdateQuantity(id, quantity);

                HttpContext.Session.SetObjectAsJson(_key, cart);
            }

            return Redirect(returnUrl);
        }

        public IActionResult DeleteFromCart(int id, int quantity, string returnUrl)
        {
            var bicycleRepository = new BicycleRepository(_context);

            Bicycle bicycle = bicycleRepository.GetById(id);

            if (bicycle != null)
            {
                var cart = GetCart();
                cart.RemoveLine(bicycle);

                bicycleRepository.ReturnQuantity(id, quantity);

                HttpContext.Session.SetObjectAsJson(_key, cart);
            }

            return RedirectToAction("Index", new { returnUrl = returnUrl });
        }

        public IActionResult ClearCart()
        {
            Cart cart = GetCart();
            cart.Clear();

            HttpContext.Session.SetObjectAsJson(_key, cart);

            return RedirectToAction("Index", "Home");
        }
    }
}
