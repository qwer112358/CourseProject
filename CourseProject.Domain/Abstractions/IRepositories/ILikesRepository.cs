using CourseProject.Domain.Abstractions.IRepositories.Common;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IRepositories;

public interface ILikesRepository : IGenericRepository<Like>
{
	Task<bool> IsLikedByUser(Guid templateId, string userId);
}