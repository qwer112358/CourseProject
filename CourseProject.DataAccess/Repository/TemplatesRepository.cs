using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions;
using CourseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataAccess.Repository;

public class TemplatesRepository(ApplicationDbContext dbContext) : IEntitiesRepository<FormTemplate>
{
    public async Task<List<FormTemplate>> Get()
    {
        return await dbContext.FormTemplates.ToListAsync();
    }

    public async Task<Guid> Create(FormTemplate formTemplate)
    {
        await dbContext.FormTemplates.AddAsync(formTemplate);
        await dbContext.SaveChangesAsync();
        return formTemplate.Id;
    }

    public async Task<Guid> Update(FormTemplate formTemplate)
    {
        await dbContext.FormTemplates
            .Where(ft => ft.Id == formTemplate.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ft => ft.Title, formTemplate.Title)
                .SetProperty(ft => ft.Description, formTemplate.Description)
                .SetProperty(ft => ft.Tags, formTemplate.Tags));
        return formTemplate.Id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await dbContext.FormTemplates
            .Where(ft => ft.Id == id)
            .ExecuteDeleteAsync();
        return id;
    }
}