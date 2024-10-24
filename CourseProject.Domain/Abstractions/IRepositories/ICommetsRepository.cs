using CourseProject.Domain.Abstractions.IRepositories.Common;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IRepositories;

public interface ICommentRepository : IGenericRepository<Comment>
{
	Task<IEnumerable<Comment>> GetCommentsByItemId(int itemId);
}