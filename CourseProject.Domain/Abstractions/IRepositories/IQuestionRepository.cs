using CourseProject.Domain.Abstractions.IRepositories.Common;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IRepositories;

public interface IQuestionRepository : IGenericRepository<Question>
{
    Task<ICollection<Question>> GetByIdsAsync(ICollection<Guid> ids);
}