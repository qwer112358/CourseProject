using CourseProject.Application.Interfaces;
using CourseProject.Application.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

[Route("[controller]")]
public class AccountController(IAccountService accountService) : Controller
{
    [HttpGet(nameof(Register))]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var result = await accountService.RegisterAsync(model);
        if (result.Succeeded)
        {
            await accountService.LoginAsync(new LoginViewModel { Email = model.Email, Password = model.Password });
            return RedirectToHome();
        }
        
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
        
        return View(model);
    }

    [HttpGet(nameof(Login))]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var validationMessage = await accountService.LoginAsync(model);
        if (string.IsNullOrEmpty(validationMessage))
            return RedirectToHome();
        
        ModelState.AddModelError(string.Empty, validationMessage);
        return View(model);
    }

    [HttpGet(nameof(Logout))]
    public async Task<IActionResult> Logout()
    {
        await accountService.LogoutAsync();
        return RedirectToHome();
    }

    private IActionResult RedirectToHome() => Redirect("/");
}

