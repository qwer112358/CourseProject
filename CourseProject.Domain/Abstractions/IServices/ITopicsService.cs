using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface ITopicsService
{
    Task<ICollection<Topic>> GetAllTopicsAsync();
    Task<Topic> GetTopicByIdAsync(Guid id);
    Task AddTopicAsync(string topicName);
    Task DeleteTopicByIdAsync(Guid id);
}