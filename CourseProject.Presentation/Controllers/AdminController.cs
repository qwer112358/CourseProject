using CourseProject.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CourseProject.Domain.Abstractions.IServices;

namespace CourseProject.Presentation.Controllers;

[Route("admin")]
[Authorize(Roles = UserRoles.Admin)]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _adminService.GetAllUsersAsync();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> BlockUsers(string[] userIds)
    {
        await _adminService.BlockUsersAsync(userIds, User);
        return RedirectToHome();
    }

    [HttpPost]
    public async Task<IActionResult> UnblockUsers(string[] userIds)
    {
        await _adminService.UnblockUsersAsync(userIds);
        return RedirectToHome();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUsers(string[] userIds)
    {
        await _adminService.DeleteUsersAsync(userIds, User);
        return RedirectToHome();
    }
    
    [HttpPost]
    public async Task<IActionResult> MakeAdmins(string[] userIds)
    {
        await _adminService.MakeAdminsAsync(userIds);
        return RedirectToHome();
    }
    
    [HttpPost]
    public async Task<IActionResult> RemoveAdmins(string[] userIds)
    {
        await _adminService.RemoveAdminsAsync(userIds);
        return RedirectToHome();
    }
    
    private IActionResult RedirectToHome() => RedirectToAction("Index");
}
