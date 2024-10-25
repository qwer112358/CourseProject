using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface IQuestionService
{
    Task<ICollection<Question>> GetAllQuestionsAsync();
    Task<Question> GetQuestionByIdAsync(Guid id);
    Task AddQuestionAsync(Question question);
    Task DeleteQuestionByIdAsync(Guid id);
}