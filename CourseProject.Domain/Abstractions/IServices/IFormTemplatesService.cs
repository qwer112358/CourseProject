using CourseProject.Domain.Abstractions.IRepositories.Common;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface IFormTemplatesService
{
    Task<ICollection<FormTemplate>> SearchFormTemplatesAsync(string searchTerm);
    Task<ICollection<FormTemplate>> GetAllFormTemplates();
    Task<FormTemplate> GetFormTemplateById(Guid id);
    Task CreateFormTemplate(FormTemplate formTemplate);
    Task UpdateFormTemplate(FormTemplate formTemplate);
    Task DeleteFormTemplate(Guid id);
    Task<ICollection<FormTemplate>> GetFormTemplatesByUserId(string userId);
}