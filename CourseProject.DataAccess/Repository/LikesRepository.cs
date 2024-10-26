using System.Linq.Expressions;
using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataAccess.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class LikesRepository(ApplicationDbContext dbContext) : ILikesRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ICollection<Like>> GetAll()
    {
        return await _dbContext.Likes
            .Include(l => l.FormTemplate)
            .Include(l => l.ApplicationUser)
            .ToListAsync();
    }

    public async Task<Like> GetById(Guid id)
    {
        return await _dbContext.Likes
            .Include(l => l.FormTemplate)
            .Include(l => l.ApplicationUser)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task Create(Like like)
    {
        await _dbContext.Likes.AddAsync(like);
        await _dbContext.SaveChangesAsync(); 
    }

    public async Task Update(Like like)
    {
        await dbContext.Likes
            .Where(l => l.Id == like.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(l => l.ApplicationUser, like.ApplicationUser)
                .SetProperty(l => l.ApplicationUserId, like.ApplicationUserId)
                .SetProperty(l => l.FormTemplate, like.FormTemplate)
                .SetProperty(l => l.FormTemplateId, like.FormTemplateId));
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Likes
            .Where(l => l.Id == id)
            .ExecuteDeleteAsync();
    }

    public IQueryable<Like> Find(Expression<Func<Like, bool>> expression)
    {
        return _dbContext.Likes.Where(expression);
    }

    public async Task<bool> IsLikedByUser(Guid templateId, string userId)
    {
        return await _dbContext.Likes
            .AnyAsync(l => l.FormTemplateId == templateId && l.ApplicationUserId == userId);
    }
}
