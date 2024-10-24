using CourseProject.Application.Interfaces;
using CourseProject.Application.Services;
using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.Presentation.Configuration.LayerConfigurations;

public static class ServiceLayerConfigurations
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;
            }) 
            .AddRoles<IdentityRole>() 
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>();;

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IFormTemplatesService, FormTemplatesService>();
    }
}