using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using BicycleShop.Services;

namespace BicycleShop.Controllers
{
    public class VerificationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Email _email;

        public VerificationController(IConfiguration configuration, Email email)
        {
            _configuration = configuration;
            _email = email;
        }

        public IActionResult SendEmail(string userEmail)
        {
            var rand = new Random();

            string code = Math.Round(rand.NextDouble() * rand.Next(1, 10), 4)
                                                                        .ToString()
                                                                                .Replace(",", "");

            _email.SendEmailCode(userEmail, code, "E-mail verification");

            return RedirectToAction("VerificateEmail", new { code = code });
        }

        public IActionResult VerificateEmail(string code)
        {
            ViewBag.Code = code;

            return View();
        }

        [HttpPost]
        public IActionResult VerificateEmail(string code, string confirmation)
        {
            if (confirmation == code)
            {
                return RedirectToAction("AddUserToDB", "Account");
            }

            TempData["verific-state"] = "Verification failed!";

            return RedirectToAction("Signup", "Account");
        }
    }
}
