using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.DataAccess.Seeds;

public static class SeedRole
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider, string role)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string roleLower = role.ToLower();
        string roleUserName = roleLower;
        string roleUserEmail = roleLower + "@mail.com";
        string roleUserPassword = roleLower;
        
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
        
        var user = await userManager.FindByEmailAsync(roleUserEmail);

        if (user is null)
        {
            user = new ApplicationUser
            {
                Name = roleUserName,
                UserName = roleUserEmail,
                Email = roleUserEmail,
                IsAdmin = true,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, roleUserPassword);
        }
        
        if (!await userManager.IsInRoleAsync(user, role))
            await userManager.AddToRoleAsync(user, role);
    }
}
