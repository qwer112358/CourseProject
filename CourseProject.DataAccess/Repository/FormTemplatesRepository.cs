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
        return await dbContext.FormTemplates.ToListAsync();
    }
    
    public async Task<FormTemplate> GetById(Guid id)
    {
        return await dbContext.FormTemplates.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task Create(FormTemplate? formTemplate)
    {
        await dbContext.FormTemplates.AddAsync(formTemplate);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task Update(FormTemplate formTemplate)
    {
        await dbContext.FormTemplates
            .Where(x => x.Id == formTemplate.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Title, formTemplate.Title)
                .SetProperty(x => x.Description, formTemplate.Description)
                .SetProperty(x => x.Tags, formTemplate.Tags)
                .SetProperty(x => x.TopicId, formTemplate.TopicId)
                .SetProperty(x => x.Topic, formTemplate.Topic)
                .SetProperty(x => x.ImageUrl, formTemplate.ImageUrl)
                .SetProperty(x => x.Tags, formTemplate.Tags)
                .SetProperty(x => x.IsPublic, formTemplate.IsPublic)
                .SetProperty(x => x.Questions, formTemplate.Questions)
                .SetProperty(x => x.CreatorId, formTemplate.CreatorId)
                .SetProperty(x => x.Creator, formTemplate.Creator)
                .SetProperty(x => x.Likes, formTemplate.Likes)
                .SetProperty(x => x.Comments, formTemplate.Comments));
    }
    
    public async Task Delete(Guid id)
    {
        await dbContext.FormTemplates
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
    
    public IQueryable<FormTemplate> Find(Expression<Func<FormTemplate, bool>> expression)
    {
        return dbContext.FormTemplates.Where(expression);
    }

    public async Task<ICollection<FormTemplate>> GetFormTemplatesByUserId(Guid userId)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
        return user.FormTemplates;
    }
}