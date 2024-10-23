namespace CourseProject.Domain.Abstractions;

public interface IEntitiesService<TEntity>
{
    Task<List<TEntity>> GetAllTemplates();
    Task<Guid> CreateTemplate(TEntity formTemplate);
    Task<Guid> UpdateTemplate(TEntity formTemplate);
    Task<Guid> DeleteTemplate(Guid id);
}