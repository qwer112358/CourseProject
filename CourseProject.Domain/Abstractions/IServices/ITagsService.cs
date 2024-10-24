using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface ITagsService
{
    Task<ICollection<Tag>> GetAllTagsAsync();
    Task<Tag> GetTagByIdAsync(Guid id);
    Task<ICollection<Tag>> GetTagsByIdsAsync(ICollection<Guid> ids);
    Task AddTagAsync(string tagName);
    Task DeleteTagByIdAsync(Guid id);
}