using HireRank.Common.Consts;
using HireRank.Core.Entities;
using HireRank.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HireRank.Infrastructure
{
    public static class DbSeed
    {
        public static async Task InitializeRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Admin));
            }
            if (await roleManager.FindByNameAsync(Roles.Student) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Student));
            }
            if (await roleManager.FindByNameAsync(Roles.Employer) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Employer));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }

        }
    }
}
