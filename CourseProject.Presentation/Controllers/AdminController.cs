using CourseProject.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CourseProject.Domain.Abstractions.IServices;

namespace CourseProject.Presentation.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class AdminController(IAdminService adminService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var users = await adminService.GetAllUsersAsync();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> BlockUsers(string[] userIds)
    {
        await adminService.BlockUsersAsync(userIds, User);
        return RedirectToHome();
    }

    [HttpPost]
    public async Task<IActionResult> UnblockUsers(string[] userIds)
    {
        await adminService.UnblockUsersAsync(userIds);
        return RedirectToHome();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUsers(string[] userIds)
    {
        await adminService.DeleteUsersAsync(userIds, User);
        return RedirectToHome();
    }
    
    [HttpPost]
    public async Task<IActionResult> MakeAdmins(string[] userIds)
    {
        await adminService.MakeAdminsAsync(userIds);
        return RedirectToHome();
    }
    
    [HttpPost]
    public async Task<IActionResult> RemoveAdmins(string[] userIds)
    {
        await adminService.RemoveAdminsAsync(userIds);
        return RedirectToHome();
    }
    
    private IActionResult RedirectToHome() => RedirectToAction("Index");
}
