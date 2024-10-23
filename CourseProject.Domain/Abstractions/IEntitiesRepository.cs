namespace CourseProject.Domain.Abstractions;

public interface IEntitiesRepository<TEntity>
{
    Task<List<TEntity>> Get();
    Task<Guid> Create(TEntity formTemplate);
    Task<Guid> Update(TEntity formTemplate);
    Task<Guid> Delete(Guid id);
}