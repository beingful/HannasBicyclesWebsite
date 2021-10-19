using System.ComponentModel.DataAnnotations;

namespace BicycleShop.Models
{
    public class UserModel
    { 
        public string Id { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,3}$", ErrorMessage = "E-mail's not valid")]
        public string Email { get; set; }

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\\S{8,16}$", ErrorMessage = "Pasword's not valid")]
        public string NewPassword { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
