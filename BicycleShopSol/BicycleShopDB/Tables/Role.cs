using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BicycleShopDB.Tables
{
    public class Role : IdentityRole
    {
        public virtual List<UserRole> UserRoles { get; set; }
    }
}
