using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface IQuestionService
{
    Task<ICollection<Question>> GetAllQuestionsAsync();
    Task<Question> GetQuestionByIdAsync(Guid id);
    Task<ICollection<Question>> GetQuestionsByIdsAsync(ICollection<Guid> ids);
    Task<ICollection<Question>> GetQuestionByFormTemplateIdAsync(Guid formTemplateId);
    Task<ICollection<Question>> GetQuestionsByFormTemplateIdsAsync(ICollection<Guid> formTemplateId);
    Task AddQuestionAsync(Question question);
    Task DeleteQuestionByIdAsync(Guid id);
}