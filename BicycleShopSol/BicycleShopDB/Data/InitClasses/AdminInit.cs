using BicycleShopDB.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleShopDB.Data.InitClasses
{
    public class AdminInit
    {
        public static async Task<Role> CreateRole(RoleManager<Role> roleManager, string roleName)
        {
            Role role = await roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                role = new Role { Name = roleName };
                await roleManager.CreateAsync(role);
            }

            return role;
        }
        public static async Task Initialize(IConfiguration configuration, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            Role admin = await CreateRole(roleManager, nameof(admin));

            Role user = await CreateRole(roleManager, nameof(user));

            User administrator = userManager.Users
                                            .FirstOrDefault(user => user.Email == configuration["AdminEmail"]);

            if (administrator is null)
            {
                administrator = new User { Email = configuration["AdminEmail"], UserName = configuration["AdminEmail"] };

                await userManager.CreateAsync(administrator, configuration["AdminPassword"]);

                await userManager.AddToRoleAsync(administrator, admin.Name);
            }

            User justUser = userManager.Users
                                   .FirstOrDefault(user => user.Email == configuration["UserEmail"]);

            if (justUser is null)
            {
                justUser = new User { Email = configuration["UserEmail"], UserName = configuration["UserEmail"] };

                await userManager.CreateAsync(justUser, configuration["UserPassword"]);

                await userManager.AddToRoleAsync(justUser, user.Name);
            }
        }
    }
}
