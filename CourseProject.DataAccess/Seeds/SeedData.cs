using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProject.DataAccess.Seeds;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        const string adminName = "admin";
        const string adminEmail = "admin@mail.com";
        const string adminPassword = "admin";

        // Создание роли Admin, если её нет
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        
        var user = await userManager.FindByEmailAsync(adminEmail);

        if (user == null)
        {
            user = new ApplicationUser
            {
                Name = adminName,
                UserName = adminEmail,
                Email = adminEmail,
                IsAdmin = true,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, adminPassword); // Пример пароля
        }

        // Проверяем, есть ли у пользователя роль Admin
        if (!await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
