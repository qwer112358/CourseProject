using CourseProject.Application.Services;
using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;
/*
[ApiController]
[Route("api/[controller]")]
public class SalesforceController : ControllerBase
{
    private readonly SalesforceService _salesforceService;
    private readonly SalesforceAuthService _authService;

    public SalesforceController(SalesforceService salesforceService, SalesforceAuthService authService)
    {
        _salesforceService = salesforceService;
        _authService = authService;
    }

    [HttpGet("accounts")]
    public async Task<IActionResult> GetAccounts()
    {
        var (instanceUrl, accessToken) = await _authService.AuthenticateAsync();
        //_salesforceService = new SalesforceService(instanceUrl, accessToken);
        var accounts = await _salesforceService.GetAccountsAsync();
        return Ok(accounts);
    }
}*/

[Authorize]
public class SalesforceController(
    SalesforceService salesforceService,
    UserManager<ApplicationUser> userManager) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateSalesforceAccount(ApplicationUserViewModel model)
    {
        var accessToken = await salesforceService.AuthenticateAsync();
        await salesforceService.CreateAccountAndContactAsync(accessToken, model.Id, model.Name, model.Email);

        return RedirectToAction("Profile");
    }
    
    [HttpPost]
    public async Task<IActionResult> ExportUserToSalesforce()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();
        
        var accessToken = await salesforceService.AuthenticateAsync();
        
        await salesforceService.CreateAccountAndContactAsync(
            accessToken,
            accountName: user.UserName,
            contactName: $"{user.Name}",
            email: user.Email
        );

        TempData["Message"] = "Профиль пользователя успешно экспортирован в Salesforce!";
        return RedirectToAction("Index", "FormTemplates");
    }
}
