using CourseProject.Domain.Abstractions.IRepositories.Common;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IRepositories;

public interface ICommentsRepository : IGenericRepository<Comment>
{
	Task<ICollection<Comment>> GetCommentsByUserId(string userId);
}