using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.ITopic;

public interface ITopicsService
{
    Task<List<Topic>> GetAllTopics();
    Task<Guid> CreateTopic(Topic topic);
    Task<Guid> UpdateTopic(Topic topic); // FIX
    Task<Guid> DeleteTopic(Guid id);
}