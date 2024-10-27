using System.Security.Claims;

namespace CourseProject.Domain.Abstractions.IServices;

public interface IAccessService
{ 
    bool HasEditAccess(string creatorId, ClaimsPrincipal user);
}