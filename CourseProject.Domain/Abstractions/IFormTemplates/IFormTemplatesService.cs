using CourseProject.Domain.Models;

namespace CourseProject.Domain;

public interface IFormTemplatesService
{
    Task<List<FormTemplate>> GetAllTemplates();
    Task<Guid> CreateTemplate(FormTemplate formTemplate);
    Task<Guid> UpdateTemplate(FormTemplate formTemplate); // FIX
    Task<Guid> DeleteTemplate(Guid id);
}