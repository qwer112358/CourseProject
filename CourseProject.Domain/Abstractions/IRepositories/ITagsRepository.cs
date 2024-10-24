using CourseProject.Domain.Abstractions.IRepositories.Common;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IRepositories;

public interface ITagsRepository : IGenericRepository<Tag>
{
    Task<ICollection<Tag>> GetByIdsAsync(ICollection<Guid> ids);
}