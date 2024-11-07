using CourseProject.Application.Services;
using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

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

        return RedirectToAction("Index", "FormTemplates");
    }
    
    [HttpPost]
    public async Task<IActionResult> ExportUserToSalesforce()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return NotFound();
        var accessToken = await salesforceService.AuthenticateAsync();
        await salesforceService.CreateAccountAndContactAsync(
            accessToken,
            accountName: user.UserName,
            contactName: user.Name,
            email: user.Email
        );

        TempData["Message"] = "Профиль пользователя успешно экспортирован в Salesforce!";
        return RedirectToAction("Index", "FormTemplates");
    }
    
    [HttpPost]
    public async Task<IActionResult> ExportUsersToSalesforce(string[] userIds)
    {
        foreach (var userId in userIds)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null) continue;
            var accessToken = await salesforceService.AuthenticateAsync();
            await salesforceService.CreateAccountAndContactAsync(
                accessToken,
                accountName: user.UserName,
                contactName: user.Name,
                email: user.Email
            );
        }
        
        TempData["Message"] = "Профиль пользователя успешно экспортирован в Salesforce!";
        return RedirectToAction("Index", "FormTemplates");
    }
}
