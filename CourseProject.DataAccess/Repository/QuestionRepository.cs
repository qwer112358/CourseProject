using System.Linq.Expressions;
using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataAccess.Repository;

public class QuestionRepository(ApplicationDbContext dbContext) : IQuestionRepository
{
    public async Task<ICollection<Question>> GetAll()
    {
        return await dbContext.Questions.ToListAsync();
    }

    public async Task<Question> GetById(Guid id)
    {
        return await dbContext.Questions.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task Create(Question queston)
    {
        await dbContext.Questions.AddAsync(queston);
        await dbContext.SaveChangesAsync();;
    }

    public async Task Update(Question queston)
    {
        await dbContext.Questions
            .Where(t => t.Id == queston.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Title, queston.Title)
                .SetProperty(t => t.Description, queston.Description));
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Questions
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }

    public IQueryable<Question> Find(Expression<Func<Question, bool>> expression)
    {
        return dbContext.Questions.Where(expression);
    }
}