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
        var existingFormTemplate = await GetFormTemplatesQuery()
            .FirstOrDefaultAsync(ft => ft.Id == formTemplate.Id);
        existingFormTemplate!.Tags = formTemplate.Tags;
        existingFormTemplate.Topic = formTemplate.Topic;
        existingFormTemplate.AllowedUsers = formTemplate.AllowedUsers;
        //existingFormTemplate.Questions = formTemplate.Questions;

        await dbContext.FormTemplates
            .Where(ft => ft.Id == formTemplate.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ft => ft.Title, formTemplate.Title)
                .SetProperty(ft => ft.Description, formTemplate.Description)
                .SetProperty(ft => ft.IsPublic, formTemplate.IsPublic)
                .SetProperty(ft => ft.ImageUrl, formTemplate.ImageUrl)
                .SetProperty(ft => ft.DateCreated, formTemplate.DateCreated)
            );
        await dbContext.SaveChangesAsync();
    }




    
    public async Task Delete(Guid id)
    {
        await dbContext.FormTemplates
            .Where(ft => ft.Id == id)
            .ExecuteDeleteAsync();
    }

    public IQueryable<FormTemplate> Find(Expression<Func<FormTemplate, bool>> expression)
    {
        return GetFormTemplatesQuery().Where(expression);
    }

    public async Task<ICollection<FormTemplate>> GetFormTemplatesByUserId(string userId)
    {
        return await GetFormTemplatesQuery()
            .Where(ft => ft.CreatorId == userId)
            .ToListAsync();;
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
            .Include(ft => ft.Likes)
            .Include(ft => ft.Forms)
            .Include(ft => ft.AllowedUsers);
    }
}