using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IRepositories;

public interface ILikesRepository : IGenericRepository<Like>
{
	Task<bool> IsLikedByUser(int itemId, string userId);
}