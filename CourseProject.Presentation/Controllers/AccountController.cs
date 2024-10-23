using CourseProject.Application.Interfaces;
using CourseProject.Application.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

public class AccountController(IAccountService accountService) : Controller
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
        var result = await accountService.RegisterAsync(model);
        if (result.Succeeded)
        {
            await accountService.LoginAsync(new LoginViewModel {Email = model.Email, Password = model.Password});
            return RedirectToAction("Index", "Home");
        }
        
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
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
        var validationMessage = await accountService.LoginAsync(model);
        if (string.IsNullOrEmpty(validationMessage))
            return RedirectToAction("Index", "Home");
        ModelState.AddModelError(string.Empty, validationMessage);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await accountService.LogoutAsync();
        return RedirectToHome();
    }
    
    private IActionResult RedirectToHome() => RedirectToAction("Index","Home");
}
