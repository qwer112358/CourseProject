using System.Security.Claims;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface IAdminService
{
    Task<ICollection<ApplicationUser>> GetAllUsersAsync();
    Task BlockUsersAsync(string[] userIds, ClaimsPrincipal currentUser);
    Task UnblockUsersAsync(string[] userIds);
    Task DeleteUsersAsync(string[] userIds, ClaimsPrincipal currentUser);
    Task MakeAdminsAsync(string[] userIds);
    Task RemoveAdminsAsync(string[] userIds);
}