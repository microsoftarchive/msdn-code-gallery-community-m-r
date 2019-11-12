using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.DataAccess
{
    public static class UserSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
                var roleManager = (RoleManager<IdentityRole>) scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

                if (await userManager.FindByEmailAsync(UserData.Users[0].Email) == null)
                {
                    if (await roleManager.FindByNameAsync(RoleData.UserRoleName) == null)
                    {
                        await roleManager.CreateAsync(RoleData.UserRole);
                    }

                    foreach (var user in UserData.Users)
                    {
                        var password = UserData.Passwords.FirstOrDefault(p => p.Contains(user.Email.Split('.')[0]));

                        var result = await userManager.CreateAsync(user, password);

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, RoleData.UserRoleName);
                        }
                    }
                }
            }
        }

    }
}
