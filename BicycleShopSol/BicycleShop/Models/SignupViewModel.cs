using System.ComponentModel.DataAnnotations;

namespace BicycleShop.Models
{
    public class SignupViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail's not valid")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\\S{8,16}$", ErrorMessage = "Password's not valid")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password confirmation does not match to the password.")]
        public string ConfirmPassword { get; set; }
    }
}
