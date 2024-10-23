using CourseProject.Application.Interfaces;
using CourseProject.Application.ViewModels.ApplicationUser;
using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.Application.Services;

public class AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    : IAccountService
{
    public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
        return await userManager.CreateAsync(user, model.Password!);
    }

    public async Task<string> LoginAsync(LoginViewModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email!);
        string validationMessage = ValidateUser(user);
        
        if (!string.IsNullOrEmpty(validationMessage))
        {
            return validationMessage;
        }

        var result = await signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);
        if (result.Succeeded)
        {
            await UpdateLastLoginDateAsync(user);
            return string.Empty;
        }

        return string.Empty;
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    private string ValidateUser(ApplicationUser user)
    {
        return user switch
        {
            null => "Invalid login attempt.",
            { IsBlocked: true } => "Your account is blocked.",
            _ => string.Empty,
        };
    }

    private async Task UpdateLastLoginDateAsync(ApplicationUser user)
    {
        user.LastLoginDate = DateTime.UtcNow;
        await userManager.UpdateAsync(user);
    }
}

