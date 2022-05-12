using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BicycleShopDB;
using Microsoft.AspNetCore.Identity;
using BicycleShop.Models;
using BicycleShopDB.Tables;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using BicycleShop.Services;

namespace BicycleShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly BicycleContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly Email _email;

        public AccountController(BicycleContext context, UserManager<User> userManager, SignInManager<User> signInManager, Email email)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _email = email;
        }

        public IActionResult Login(string returnUrl)
        {
            if (TempData.ContainsKey("success"))
            {
                ViewBag.Success = TempData["success"] as string;
            }

            if (string.IsNullOrEmpty(returnUrl) || !(returnUrl.StartsWith("/Home/") || returnUrl.Contains("?")))
            {
                returnUrl = "/Home/Index";
            }

            return View(
                new LoginViewModel 
                { 
                    ReturnUrl = returnUrl 
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               var result = await _signInManager
                                        .PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    User user = await _userManager.FindByEmailAsync(model.Email);

                    HttpContext.Session.SetString("user", user.Email);
                    HttpContext.Session.SetString("role", (await _userManager.GetRolesAsync(user))?.First());

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid e-mail or password.");
                }
            }
            return View(model);
        }

        public IActionResult Signup()
        {
            if (TempData.ContainsKey("verific-state"))
            {
                ViewBag.Verification = TempData["verific-state"] as string;
                TempData.Remove("verific-state");
            }

            return View(new SignupViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);

                if (user is null)
                {
                    TempData["registrModel"] = JsonConvert.SerializeObject(model);

                    return RedirectToAction("SendEmail", "Verification", new { userEmail = model.Email });
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Email), $"User with name {model.Email} has already exist.");
                }
            }
            else
            {
                ModelState.AddModelError("ConfirmPassword", "Invalid password confirmation.");
            }

            return View(model);
        }

        public async Task<IActionResult> AddUserToDB()
        {
            var model = JsonConvert.DeserializeObject<SignupViewModel>(TempData["registrModel"] as string);

            TempData.Remove("registrModel");

            var user = new User 
            { 
                Email = model.Email, 
                UserName = model.Email 
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "user");

                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("Signup");
        }

        public IActionResult ForgotPassword()
        {
            return View(
                new ForgotPasswordViewModel 
                { 
                    Email = User.Identity.Name 
                });
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    model.SetNewPassword();

                    var passwordValidator = HttpContext.RequestServices
                                                            .GetService(typeof(IPasswordValidator<User>)) 
                                                                                            as IPasswordValidator<User>;

                    var passwordHasher = HttpContext.RequestServices
                                                            .GetService(typeof(IPasswordHasher<User>)) 
                                                                                            as IPasswordHasher<User>;

                    IdentityResult result = await passwordValidator
                                                            .ValidateAsync(_userManager, user, model.NewPassword);

                    if (result.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);

                        _email.SendEmailCode(model.Email, model.NewPassword, "New password.");

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User's not found.");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(model.Email), "Invalid e-mail adress.");
            }

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }
    }
}
