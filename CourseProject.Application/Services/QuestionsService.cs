using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Services;

public class QuestionsService(IQuestionRepository questionRepository) : IQuestionService
{
    public async Task<ICollection<Question>> GetAllQuestionsAsync()
    {
        return await questionRepository.GetAll();
    }

    public async Task<Question> GetQuestionByIdAsync(Guid id)
    {
        return await questionRepository.GetById(id);
    }

    public async Task<ICollection<Question>> GetQuestionsByIdsAsync(ICollection<Guid> ids)
    {
        return await questionRepository.GetByIdsAsync(ids);
    }

    public async Task<ICollection<Question>> GetQuestionByFormTemplateIdAsync(Guid formTemplateId)
    {
        return await questionRepository.GetByFormTemplateIdAsync(formTemplateId);
    }

    public async Task<ICollection<Question>> GetQuestionsByFormTemplateIdsAsync(ICollection<Guid> formTemplateId)
    {
        return await questionRepository.GetByFormTemplateIdsAsync(formTemplateId);
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