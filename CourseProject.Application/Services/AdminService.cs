using System.Security.Claims;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Application.Services;

public class AdminService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    : IAdminService
{
    public async Task<ICollection<ApplicationUser>> GetAllUsersAsync()
    {
        return await userManager.Users.ToListAsync();
    }

    public async Task BlockUsersAsync(string[] userIds, ClaimsPrincipal currentUser)
    {
        foreach (var id in userIds)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null) continue;
            user.IsBlocked = true;
            await userManager.UpdateAsync(user);
            if (IsCurrentUser(user, currentUser))
            {
                await signInManager.SignOutAsync();
            }
        }
    }

    public async Task UnblockUsersAsync(string[] userIds)
    {
        foreach (var id in userIds)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null) continue;
            user.IsBlocked = false;
            await userManager.UpdateAsync(user);
        }
    }

    public async Task DeleteUsersAsync(string[] userIds, ClaimsPrincipal currentUser)
    {
        foreach (var id in userIds)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null) continue;
            await userManager.DeleteAsync(user);
            if (IsCurrentUser(user, currentUser))
            {
                await signInManager.SignOutAsync();
            }
        }
    }

    public async Task MakeAdminsAsync(string[] userIds)
    {
        foreach (var id in userIds)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null) continue;
            user.IsAdmin = true;
            if (!await userManager.IsInRoleAsync(user, "Admin"))
                await userManager.AddToRoleAsync(user, "Admin");
            await userManager.UpdateAsync(user);
        }
    }

    public async Task RemoveAdminsAsync(string[] userIds)
    {
        foreach (var id in userIds)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null) continue;
            user.IsAdmin = false;
            if (await userManager.IsInRoleAsync(user, "Admin"))
                await userManager.RemoveFromRoleAsync(user, "Admin");
            await userManager.UpdateAsync(user);
        }
    }
    
    private bool IsCurrentUser(ApplicationUser user, ClaimsPrincipal currentUser)
    {
        return currentUser.FindFirstValue(ClaimTypes.NameIdentifier) == user.Id;
    }
}
