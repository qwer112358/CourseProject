using CourseProject.Domain.Abstractions.IRepositories.Common;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IRepositories;

public interface IFormsRepository : IGenericRepository<Form>
{
    Task<ICollection<Form>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task<ICollection<Form>> GetByTemplateIdAsync(Guid formTemplateId);
}