using System.Linq.Expressions;
using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataAccess.Repository;

public class FormTemplatesRepository(ApplicationDbContext dbContext) : IFormTemplatesRepository
{
    public async Task<ICollection<FormTemplate>> GetAll()
    {
        return await GetFormTemplatesQuery().ToListAsync();
    }
    
    public async Task<FormTemplate> GetById(Guid id)
    {
        return await GetFormTemplatesQuery().FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task Create(FormTemplate? queston)
    {
        await dbContext.FormTemplates.AddAsync(queston);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task Update(FormTemplate formTemplate)
    {
        await dbContext.FormTemplates
            .Where(x => x.Id == formTemplate.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Title, formTemplate.Title)
                .SetProperty(x => x.Description, formTemplate.Description)
                .SetProperty(x => x.TopicId, formTemplate.TopicId)
                .SetProperty(x => x.ImageUrl, formTemplate.ImageUrl)
                .SetProperty(x => x.IsPublic, formTemplate.IsPublic)
                .SetProperty(x => x.CreatorId, formTemplate.CreatorId)
            );
        
        var existingTemplate = await dbContext.FormTemplates
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == formTemplate.Id);

        if (existingTemplate != null)
        {
            //existingTemplate.Tags.Clear();
            //existingTemplate.Tags.AddRange(formTemplate.Tags);
            existingTemplate.Questions = formTemplate.Questions;
            existingTemplate.Likes = formTemplate.Likes;
            existingTemplate.Comments = formTemplate.Comments;
        
            await dbContext.SaveChangesAsync();
        }
    }
    
    public async Task Delete(Guid id)
    {
        await dbContext.FormTemplates
            .Where(ft => ft.Id == id)
            .ExecuteDeleteAsync();
    }
    
    public IQueryable<FormTemplate> Find(Expression<Func<FormTemplate, bool>> expression)
    {
        return dbContext.FormTemplates.Where(expression);
    }

    public async Task<ICollection<FormTemplate>> GetFormTemplatesByUserId(string userId)
    {
        var user = await dbContext.Users
            .Include(u => u.FormTemplates)
            .FirstOrDefaultAsync(u => u.Id.Equals(userId));
        return user.FormTemplates;
    }

    public async Task<ICollection<FormTemplate>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return await GetAll();
        var lowerSearchTerm = searchTerm.ToLower();
        return await GetFormTemplatesQuery()
            .Where(ft => ft.Title.Contains(lowerSearchTerm) 
                         || ft.Questions.Any(q => q.Description.Contains(lowerSearchTerm)) 
                         || ft.Comments.Any(c => c.Text.Contains(lowerSearchTerm)))
            .ToListAsync();
    }
    
    private IQueryable<FormTemplate> GetFormTemplatesQuery()
    {
        return dbContext.FormTemplates
            .Include(ft => ft.Topic)
            .Include(ft => ft.Comments)
            .Include(ft => ft.Questions)
            .Include(ft => ft.Creator)
            .Include(ft => ft.Tags)
            .Include(ft => ft.Likes);
    }
}