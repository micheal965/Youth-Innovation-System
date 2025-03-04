using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Youth_Innovation_System.Repository.Identity
{
    public static class RolesSeeding
    {
        public async static Task SeedRoles(this IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] rolesData = { "Admin", "User", "Investor" };
            foreach (var item in rolesData)
            {
                if (!await roleManager.RoleExistsAsync(item))
                {
                    await roleManager.CreateAsync(new IdentityRole(item));
                }
            }

        }
    }
}
