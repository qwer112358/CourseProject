using System.Linq.Expressions;
using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataAccess.Repository;

public class TagsRepository(ApplicationDbContext dbContext) : ITagsRepository
{
    public async Task<ICollection<Tag>> GetAll()
    {
        return await dbContext.Tags.ToListAsync();
    }

    public async Task<Tag> GetById(Guid id)
    {
        return await dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task Create(Tag queston)
    {
        await dbContext.Tags.AddAsync(queston);
        await dbContext.SaveChangesAsync();;
    }

    public async Task Update(Tag tag)
    {
        await dbContext.Topics
            .Where(t => t.Id == tag.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Name, tag.Name)
                .SetProperty(t => t.FormTemplates, tag.FormTemplates));
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Tags
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }

    public IQueryable<Tag> Find(Expression<Func<Tag, bool>> expression)
    {
        return dbContext.Tags.Where(expression);
    }

    public async Task<ICollection<Tag>> GetByIdsAsync(ICollection<Guid> ids)
    {
        return await dbContext.Tags.Where(t => ids.Contains(t.Id)).ToListAsync();  
    }
}