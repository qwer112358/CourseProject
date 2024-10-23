using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.ITopic;

public interface ITopicsRepository
{
    Task<List<Topic>> Get();
    Task<Guid> Create(Topic topic);
    Task<Guid> Update(Topic topic); // FIX
    Task<Guid> Delete(Guid id);
}