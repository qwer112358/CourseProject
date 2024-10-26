using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;
using FormBuilder.Domain.Enums;

namespace CourseProject.Presentation.Mappers;

public static class FormAnswerMapper
{
    public static FormAnswer ToFormAnswer(this FormQuestionViewModel viewModel, Guid formId)
    {
        return new FormAnswer
        {
            Id = Guid.NewGuid(),
            FormId = formId,
            QuestionId = viewModel.QuestionId,
            AnswerText = viewModel.AnswerText,
            AnswerInteger = viewModel.AnswerInteger,
            AnswerCheckbox = viewModel.Type == QuestionType.Checkbox 
                ? viewModel.AnswerCheckbox 
                : (bool?)null 
        };
    }

    public static FormQuestionViewModel ToViewModel(this FormAnswer answer)
    {
        return new FormQuestionViewModel
        {
            QuestionId = answer.QuestionId,
            Title = answer.Question?.Title, 
            Description = answer.Question?.Description, 
            Order = answer.Question.Order,
            Type = answer.Question.Type,
            AnswerText = answer.AnswerText,
            AnswerInteger = answer.AnswerInteger,
            AnswerCheckbox = answer.AnswerCheckbox ?? false 
        };
    }
}

