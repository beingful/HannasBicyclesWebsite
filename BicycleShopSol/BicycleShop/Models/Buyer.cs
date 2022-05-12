using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BicycleShopDB.Tables;

namespace BicycleShop.Models
{
    public class Buyer
    {
        [Required(ErrorMessage = "The name field is required.")]
        [RegularExpression("(^[A-Z]{1}[a-z]* {1})*[A-Z]{1}[a-z]*$", ErrorMessage = "Name's not valid.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The last name field is required.")]
        [RegularExpression("^[A-Z]{1}[a-z]*$", ErrorMessage = "Last name's not valid.")]
        public string Lastname { get; set; }
        public string Email { get; set; }
    }
}
