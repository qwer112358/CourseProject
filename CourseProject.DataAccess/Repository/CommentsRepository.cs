using System.Linq.Expressions;
using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataAccess.Repository;

public class CommentsRepository(ApplicationDbContext dbContext) : ICommentsRepository
{
    public async Task<ICollection<Comment>> GetAll()
    {
        return await dbContext.Comments.ToListAsync();
    }

    public async Task<Comment> GetById(Guid id)
    {
        return await dbContext.Comments.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task Create(Comment comment)
    {
        await dbContext.Comments.AddAsync(comment);
        await dbContext.SaveChangesAsync();;
    }

    public async Task Update(Comment comment)
    {
        await dbContext.Comments
            .Where(t => t.Id == comment.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Text, comment.Text)
                .SetProperty(c => c.CreatedAt, comment.CreatedAt));
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Questions
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }

    public IQueryable<Comment> Find(Expression<Func<Comment, bool>> expression)
    {
        return dbContext.Comments.Where(expression);
    }

    public async Task<ICollection<Comment>> GetCommentsByUserId(string userId)
    {
        var user = await dbContext.Users
            .Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.Id.Equals(userId));
        return user.Comments;
    }
}