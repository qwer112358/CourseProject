using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface IFormsService
{
    Task<ICollection<Form>> GetAllFormsAsync();
    Task<Form> GetFormByIdAsync(Guid id);
    Task<ICollection<Form>> GetFormsByIdsAsync(ICollection<Guid> ids);
    Task<ICollection<Form>> GetFormsByTemplateIdAsync(Guid formTemplateId);
    Task AddFormAsync(Form Form);
    Task DeleteFormByIdAsync(Guid id);
}