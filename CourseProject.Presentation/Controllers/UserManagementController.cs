using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CourseProject.Domain.Models;

namespace CourseProject.Presentation.Controllers;

[Authorize(Roles = "Admin")]
public class UserManagementController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    : Controller
{
    public async Task<IActionResult> Index() => View(await userManager.Users.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> BlockUsers(string[] userIds)
    {
        foreach (var id in userIds)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is not null)
            {
                user.IsBlocked = true;
                await userManager.UpdateAsync(user);
                if (IsCurrentUser(user))
                {
                    await signInManager.SignOutAsync();
                }
            }
        }

        return RedirectToHome();
    }

    [HttpPost]
    public async Task<IActionResult> UnblockUsers(string[] userIds)
    {
        foreach (var id in userIds)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null) continue;
            user.IsBlocked = false;
            await userManager.UpdateAsync(user);
        }

        return RedirectToHome();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUsers(string[] userIds)
    {
        foreach (var id in userIds)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is not null)
                await userManager.DeleteAsync(user);
        }

        return RedirectToHome();
    }

    private IActionResult RedirectToHome() => RedirectToAction("Index");

    private bool IsCurrentUser(ApplicationUser user) => User.FindFirstValue(ClaimTypes.NameIdentifier) == user.Id;
}