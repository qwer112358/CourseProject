using System.Linq.Expressions;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IRepositories.Common;

public interface IGenericRepository<T> where T : class
{
    Task<ICollection<T>> GetAll(); 
    Task<T> GetById(Guid id); 
    Task Create(T entity);
    Task Update(T entity); 
    Task Delete(Guid id); 
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
}