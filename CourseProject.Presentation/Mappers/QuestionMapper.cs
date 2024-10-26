using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;

namespace CourseProject.Presentation.Mappers;

public static class QuestionMapper
{
    public static QuestionViewModel ToViewModel(this Question question) => new()
        {
            Id = question.Id,
            Title = question.Title,
            Description = question.Description,
            Order = question.Order,
            Type = question.Type
        };

    public static Question ToDomain(this QuestionViewModel viewModel, Guid formTemplateId) => new Question
        {
            Id = viewModel.Id,
            Title = viewModel.Title,
            Description = viewModel.Description,
            Order = viewModel.Order,
            Type = viewModel.Type,
            FormTemplateId = formTemplateId,
        };
}