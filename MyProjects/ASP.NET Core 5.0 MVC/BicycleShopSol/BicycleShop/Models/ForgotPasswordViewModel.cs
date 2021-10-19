using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BicycleShop.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [RegularExpression("[a-zA-Z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,3}$", ErrorMessage = "E-mail's not valid")]
        public string Email { get; set; }
        public string NewPassword { get; private set; }

        public void SetNewPassword()
        {
            Regex passwordPattern = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\\S{8,16}$");

            NewPassword = GenerateRandomPassword();

            while (!passwordPattern.IsMatch(NewPassword))
            {
                NewPassword = GenerateRandomPassword();
            }
        }

        private string GenerateRandomPassword()
        {
            string password = "";

            Random rand = new Random();

            int passLength = rand.Next(8, 17);

            for (int i = 0; i < passLength; i++)
            {
                int choise = rand.Next(0, 4);

                switch (choise)
                {
                    case 0:
                        password += rand.Next(0, 9);
                        break;
                    case 1:
                        password += (char)rand.Next(58, 64);
                        break;
                    case 2:
                        password += (char)rand.Next(65, 90);
                        break;
                    case 3:
                        password += (char)rand.Next(97, 122);
                        break;
                }
            }

            return password;
        }
    }
}
