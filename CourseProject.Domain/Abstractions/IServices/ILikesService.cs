using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface ILikesService
{
    Task ToggleLike(Guid templateId, string userId);
    Task<ICollection<Like>> GetLikesByTemplateId(Guid templateId);
}