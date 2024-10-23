using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
        var result = await userManager.CreateAsync(user, model.Password!);
            
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = await userManager.FindByEmailAsync(model.Email!);
        var validationMessage = ValidateUser(user!);
        if (!string.IsNullOrEmpty(validationMessage))
        {
            ModelState.AddModelError(string.Empty, validationMessage);
            return View(model);
        }
        
        var result = await signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);
        
        if (result.Succeeded)
        {
            await UpdateLastLoginDateAsync(user!);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
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
