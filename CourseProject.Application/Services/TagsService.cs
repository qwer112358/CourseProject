using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Services;

public class TagsService(ITagsRepository tagsRepository) : ITagsService
{
    public async Task<ICollection<Tag>> GetAllTagsAsync()
    {
        return await tagsRepository.GetAll();
    }

    public async Task<Tag> GetTagByIdAsync(Guid id)
    {
        return await tagsRepository.GetById(id);
    }

    public async Task<ICollection<Tag>> GetTagsByIdsAsync(ICollection<Guid> ids)
    {
        return await tagsRepository.GetByIdsAsync(ids);
    }

    public async Task AddTagAsync(string topicName)
    {
        await tagsRepository.Create(new Tag { Name = topicName });
    }

    public async Task DeleteTagByIdAsync(Guid id)
    {
        await tagsRepository.Delete(id);
    }
}