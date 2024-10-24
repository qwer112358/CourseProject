using CourseProject.Domain.Abstractions.IRepositories.Common;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface IFormTemplatesService
{
    Task<ICollection<FormTemplate>> GetAllFormTemplates();
    Task<FormTemplate> GetFormTemplateById(Guid id);
    Task CreateFormTemplate(FormTemplate collection);
    Task UpdateFormTemplate(FormTemplate collection);
    Task DeleteFormTemplate(Guid id);
    Task<ICollection<FormTemplate>> GetFormTemplatesByUserId(Guid userId);
}