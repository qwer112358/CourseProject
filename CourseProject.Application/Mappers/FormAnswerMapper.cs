using CourseProject.Application.ViewModels;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Mappers;

public static class FormAnswerMapper
{
    public static FormAnswerViewModel ToViewModel(this FormAnswer answer)
    {
        return new FormAnswerViewModel
        {
            Id = answer.Id,
            FormId = answer.FormId,
            QuestionId = answer.QuestionId,
            AnswerText = answer.AnswerText,
            AnswerInteger = answer.AnswerInteger,
            AnswerCheckbox = answer.AnswerCheckbox
        };
    }

    public static FormAnswer ToModel(this FormAnswerViewModel viewModel)
    {
        return new FormAnswer
        {
            Id = viewModel.Id,
            FormId = viewModel.FormId,
            QuestionId = viewModel.QuestionId,
            AnswerText = viewModel.AnswerText,
            AnswerInteger = viewModel.AnswerInteger,
            AnswerCheckbox = viewModel.AnswerCheckbox
        };
    }
}
