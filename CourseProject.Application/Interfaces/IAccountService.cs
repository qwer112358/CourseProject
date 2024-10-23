using CourseProject.Application.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.Application.Interfaces;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(RegisterViewModel model);
    Task<string> LoginAsync(LoginViewModel model);
    Task LogoutAsync();
}