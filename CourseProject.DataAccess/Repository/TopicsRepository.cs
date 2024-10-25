using System.Linq.Expressions;
using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataAccess.Repository;

public class TopicsRepository(ApplicationDbContext dbContext)  : ITopicsRepository
{
    public async Task<ICollection<Topic>> GetAll()
    {
        return await dbContext.Topics.ToListAsync();
    }

    public async Task<Topic> GetById(Guid id)
    {
        return await dbContext.Topics.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task Create(Topic queston)
    {
        await dbContext.Topics.AddAsync(queston);
        await dbContext.SaveChangesAsync();;
    }

    public async Task Update(Topic topic)
    {
        await dbContext.Topics
            .Where(t => t.Id == topic.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Name, topic.Name)
                .SetProperty(t => t.FormTemplates, topic.FormTemplates));
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Topics
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }

    public IQueryable<Topic> Find(Expression<Func<Topic, bool>> expression)
    {
        return dbContext.Topics.Where(expression);
    }
}