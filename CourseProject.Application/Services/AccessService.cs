using System.Security.Claims;
using CourseProject.Domain;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseProject.Application.Services;

public class AccessService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    : IAccessService
{
    public bool HasEditAccess(string creatorId, ClaimsPrincipal user) =>
        creatorId == userManager.GetUserId(user) || user.IsInRole(UserRoles.Admin);
}
