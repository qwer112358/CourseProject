using CourseProject.Domain.Abstractions.IRepositories.Common;
using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IRepositories;

public interface IFormTemplatesRepository : IGenericRepository<FormTemplate>
{
    Task<ICollection<FormTemplate>> GetFormTemplatesByUserId(string userId);
    Task<ICollection<FormTemplate>> SearchAsync(string searchTerm);
}