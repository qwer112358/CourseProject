using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Services;

public class FormsService(IFormsRepository formsRepository) : IFormsService
{
    public async Task<ICollection<Form>> GetAllFormsAsync()
    {
        return await formsRepository.GetAll();
    }

    public async Task<Form> GetFormByIdAsync(Guid id)
    {
        return await formsRepository.GetById(id);
    }

    public async Task<ICollection<Form>> GetFormsByIdsAsync(ICollection<Guid> ids)
    {
        return await formsRepository.GetByIdsAsync(ids);
    }

    public async Task<ICollection<Form>> GetFormsByTemplateIdAsync(Guid formTemplateId)
    {
        return await formsRepository.GetByTemplateIdAsync(formTemplateId);
    }

    public async Task AddFormAsync(Form Form)
    {
        await formsRepository.Create(Form);
    }

    public async Task DeleteFormByIdAsync(Guid id)
    {
        await formsRepository.Delete(id);
    }
}