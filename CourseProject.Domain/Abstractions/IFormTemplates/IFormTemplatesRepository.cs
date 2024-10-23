using CourseProject.Domain.Models;

namespace CourseProject.Domain;

public interface IFormTemplatesRepository
{
    Task<List<FormTemplate>> Get();
    Task<Guid> Create(FormTemplate formTemplate);
    Task<Guid> Update(FormTemplate formTemplate); // FIX
    Task<Guid> Delete(Guid id);
}