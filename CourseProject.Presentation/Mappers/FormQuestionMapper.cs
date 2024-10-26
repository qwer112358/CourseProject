using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;

namespace CourseProject.Presentation.Mappers;

public static class FormQuestionMapper
{
    public static Question ToDomain(this FormQuestionViewModel viewModel, Guid formTemplateId) => new()
    {
        Id = viewModel.QuestionId,
        Title = viewModel.Title ?? string.Empty, // Можно задать значение по умолчанию, если Title null
        Description = viewModel.Description,
        Type = viewModel.Type,
        Order = viewModel.Order,
        FormTemplateId = formTemplateId,
        FormAnswers = new List<FormAnswer>()
    };

    public static FormQuestionViewModel ToFormQuestionViewModel(this Question question) => new()
    {
        QuestionId = question.Id,
        Title = question.Title,
        Description = question.Description,
        Order = question.Order,
        Type = question.Type,
    };
}