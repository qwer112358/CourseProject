using System.Linq.Expressions;
using CourseProject.DataAccess.Data;
using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DataAccess.Repository;

public class FormsRepository(ApplicationDbContext dbContext) : IFormsRepository
{
    public async Task<ICollection<Form>> GetAll()
    {
        return await dbContext.Forms.ToListAsync();
    }

    public async Task<Form> GetById(Guid id)
    {
        return await GetAllPropertiesFormsQuery()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task Create(Form queston)
    {
        await dbContext.Forms.AddAsync(queston);
        await dbContext.SaveChangesAsync();;
    }

    public async Task Update(Form form)
    {
        await dbContext.Forms
            .Where(t => t.Id == form.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.SubmissionDate, form.SubmissionDate)
                .SetProperty(t => t.Answers, form.Answers));
    }

    public async Task Delete(Guid id)
    {
        await dbContext.Forms
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }

    public IQueryable<Form> Find(Expression<Func<Form, bool>> expression)
    {
        return dbContext.Forms.Where(expression);
    }

    public async Task<ICollection<Form>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await dbContext.Forms.Where(t => ids.Contains(t.Id)).ToListAsync();  
    }

    public async Task<ICollection<Form>> GetByTemplateIdAsync(Guid formTemplateId)
    {
        return await GetAllPropertiesFormsQuery().Where(f => f.FormTemplateId == formTemplateId).ToListAsync();
    }

    public async Task<ICollection<Form>> GetByIdsAsync(ICollection<Guid> ids)
    {
        return await dbContext.Forms.Where(f => ids.Contains(f.Id)).ToListAsync();  
    }
    
    private IQueryable<Form> GetAllPropertiesFormsQuery()
    {
        return dbContext.Forms
            .Include(f => f.Answers)
            .Include(f => f.FormTemplate)
            .Include(f => f.ApplicationUser);
    }
}