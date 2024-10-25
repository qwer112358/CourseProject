using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Services;

public class FormTemplatesService(IFormTemplatesRepository formTemplatesRepository) : IFormTemplatesService
{
    public async Task<ICollection<FormTemplate>> SearchFormTemplatesAsync(string searchTerm)
    {
        return await formTemplatesRepository.SearchAsync(searchTerm);
    }

    public async Task<ICollection<FormTemplate>> GetAllFormTemplates()
    {
        return await formTemplatesRepository.GetAll();
    }

    public async Task<FormTemplate> GetFormTemplateByIdAsync(Guid id)
    {
        return await formTemplatesRepository.GetById(id);
    }

    public async Task CreateFormTemplate(FormTemplate formTemplate)
    {
        await formTemplatesRepository.Create(formTemplate);
    }

    public async Task UpdateFormTemplate(FormTemplate formTemplate)
    {
        await formTemplatesRepository.Update(formTemplate);
    }

    public async Task DeleteFormTemplate(Guid id)
    {
        await formTemplatesRepository.Delete(id);
    }

    public async Task<ICollection<FormTemplate>> GetFormTemplatesByUserId(string userId)
    {
        return await formTemplatesRepository.GetFormTemplatesByUserId(userId);
    }
}