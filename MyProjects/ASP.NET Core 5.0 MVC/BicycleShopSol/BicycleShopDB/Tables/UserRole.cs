using Microsoft.AspNetCore.Identity;

namespace BicycleShopDB.Tables
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
