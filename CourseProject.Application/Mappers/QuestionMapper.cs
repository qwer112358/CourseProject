using CourseProject.Application.ViewModels;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Mappers;

public static class QuestionMapper
{
    public static QuestionViewModel ToViewModel(this Question question)
    {
        return new QuestionViewModel
        {
            Id = question.Id,
            Title = question.Title,
            Description = question.Description,
            Type = question.Type,
        };
    }

    public static Question ToModel(this QuestionViewModel viewModel, Guid formTemplateId)
    {
        return new Question
        {
            Id = viewModel.Id,
            Title = viewModel.Title,
            Description = viewModel.Description,
            FormTemplateId = formTemplateId,
        };
    }
}