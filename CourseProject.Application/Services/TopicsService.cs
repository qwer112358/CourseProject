using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Services;

public class TopicsService(ITopicsRepository topicsRepository) : ITopicsService
{
    public async Task<ICollection<Topic>> GetAllTopicsAsync()
    {
        return await topicsRepository.GetAll();
    }

    public async Task<Topic> GetTopicByIdAsync(Guid id)
    {
        return await topicsRepository.GetById(id);
    }

    public async Task AddTopicAsync(string topicName)
    {
        await topicsRepository.Create(new Topic { Name = topicName });
    }

    public async Task DeleteTopicByIdAsync(Guid id)
    {
        await topicsRepository.Delete(id);
    }
}