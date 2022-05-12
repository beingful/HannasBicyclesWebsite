using BicycleShopDB.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using BicycleShopDB;
using BicycleShopDB.Repository;
using BicycleShop.Models;
using BicycleShop.Services;

namespace BicycleShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly BicycleContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly Email _email;

        public AdminController(BicycleContext context, UserManager<User> userManager, SignInManager<User> signInManager, Email email)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _email = email;
        }

        public IActionResult BicyclePanel()
        {
            return View(_context.Bicycles.ToList());
        }

        public IActionResult CreateBicycle(int? id)
        {
            if (id is null)
            {
                return View(new BicycleModel());
            }
            
            Bicycle bicycle = _context.Bicycles.Find(id);

            var model = new BicycleModel(bicycle);

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateBicycle(BicycleModel editBicycle)
        {
            if (ModelState.IsValid)
            {
                BicycleRepository repository = new BicycleRepository(_context);

                if (editBicycle.Total is 0)
                {
                    editBicycle.Total = Convert.ToInt32(editBicycle.Price * (1 - editBicycle.Discount / 100.0));
                }

                var bicycle = new Bicycle
                {
                    BicycleId = editBicycle.BicycleId,
                    BicycleName = editBicycle.BicycleName,
                    Brand = editBicycle.Brand,
                    Type = editBicycle.Type,
                    ReleaseYear = editBicycle.ReleaseYear,
                    FrameMaterial = editBicycle.FrameMaterial,
                    WheelSize = editBicycle.WheelSize,
                    BrakeType = editBicycle.BrakeType,
                    SpeedQuantity = editBicycle.SpeedQuantity,
                    MaxWeight = editBicycle.MaxWeight,
                    Price = editBicycle.Price,
                    Discount = editBicycle.Discount,
                    Total = editBicycle.Total,
                    Quantity = editBicycle.Quantity,
                    ImagePath = editBicycle.ImagePath
                };

                if (editBicycle.BicycleId is 0)
                {
                    repository.Create(bicycle);
                }
                else
                {
                    repository.Update(bicycle);
                }

                return RedirectToAction("BicyclePanel");
            }

            return View(editBicycle);
        }

        public IActionResult DeleteBicycle(int id)
        {
            var bicycleRepository = new BicycleRepository(_context);

            bicycleRepository.DeleteById(id);

            return RedirectToAction("BicyclePanel");
        }

        public IActionResult UserPanel()
        {
            return View(_context.Users.ToList());
        }

        public async Task<IActionResult> CreateUser(string id)
        {
            if (id is null)
            {
                return View(new UserModel());
            }

            User user = await _userManager.FindByIdAsync(id);

            string userRole = user.UserRoles.First().Role.Name;

            var userModel = new UserModel 
            { 
                Id = user.Id, 
                Email = user.Email, 
                Role = userRole 
            };

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = HttpContext.RequestServices
                                                    .GetService(typeof(RoleManager<Role>)) 
                                                                                as RoleManager<Role>;

                Role role = await roleManager.FindByNameAsync(model.Role);

                if (role is null)
                {
                    await roleManager.CreateAsync(new Role 
                    { 
                        Name = model.Role 
                    });
                }

                if (string.IsNullOrEmpty(model.Id))
                {
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        var user = new User 
                        { 
                            Email = model.Email, 
                            UserName = model.Email 
                        };

                        IdentityResult creationResult = await _userManager.CreateAsync(user, model.NewPassword);

                        if (creationResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, false);
                            await _userManager.AddToRoleAsync(user, model.Role);

                            return RedirectToAction("UserPanel");
                        }
                        else
                        {
                            foreach (var error in creationResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }

                            return View(model);
                        }
                    }

                    ModelState.AddModelError("NewPassword", "This field is required");

                    return View(model);
                }

                else
                {
                    User existedUser = await _userManager.FindByEmailAsync(model.Email);

                    existedUser.UserName = model.Email;
                    existedUser.Email = model.Email;

                    string oldRole = (await _userManager.GetRolesAsync(existedUser)).First();

                    await _userManager.RemoveFromRoleAsync(existedUser, oldRole);
                    await _userManager.AddToRoleAsync(existedUser, model.Role);

                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        var passwordValidator = HttpContext.RequestServices
                                                                .GetService(typeof(IPasswordValidator<User>)) 
                                                                                            as IPasswordValidator<User>;

                        var passwordHasher = HttpContext.RequestServices
                                                                .GetService(typeof(IPasswordHasher<User>)) 
                                                                                            as IPasswordHasher<User>;

                        IdentityResult validationResult = await passwordValidator
                                                                .ValidateAsync(_userManager, existedUser, model.NewPassword);

                        if (validationResult.Succeeded)
                        {
                            existedUser.PasswordHash = passwordHasher.HashPassword(existedUser, model.NewPassword);

                            await _userManager.UpdateAsync(existedUser);

                            _email.SendEmailCode(model.Email, model.NewPassword, "New password.");

                            return RedirectToAction("UserPanel");
                        }
                        else
                        {
                            foreach (var error in validationResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserPanel");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong. User wasn't deleted.");

                    return RedirectToAction("UserPanel");
                }
            }
            else
            {
                ModelState.AddModelError("", "User wasn't found at the system.");

                return View("UserPanel");
            }
        }
    }
}
