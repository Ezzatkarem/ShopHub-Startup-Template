using Microsoft.AspNetCore.Identity;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Data
{
    public  static class RoleSeed
    {
        public static async Task SeedAsync(
     RoleManager<IdentityRole> roleManager,
     UserManager<ApplicationUser> userManager)
        {
            string[] roles =
            {
        "Admin",
        "Customer"
    };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var admin = await userManager.FindByEmailAsync("admin@gmail.com");

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "System Admin",
                    EmailConfirmed=true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
