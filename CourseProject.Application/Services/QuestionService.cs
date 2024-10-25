using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Services;

public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
{
    public async Task<ICollection<Question>> GetAllQuestionsAsync()
    {
        return await questionRepository.GetAll();
    }

    public async Task<Question> GetQuestionByIdAsync(Guid id)
    {
        return await questionRepository.GetById(id);
    }

    public async Task AddQuestionAsync(Question question)
    {
        await questionRepository.Create(question);
    }

    public async Task DeleteQuestionByIdAsync(Guid id)
    {
        await questionRepository.Delete(id);
    }
}