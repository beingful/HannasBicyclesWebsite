using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BicycleShopDB.Tables
{
    public class User : IdentityUser
    {
        public virtual List<Order> Orders { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
    }
}
