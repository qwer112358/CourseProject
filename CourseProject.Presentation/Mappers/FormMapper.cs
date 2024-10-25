using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;

namespace CourseProject.Presentation.Mappers;
/*
public static class FormMapper
{
    public static FormViewModel ToViewModel(this Form form)
    {
        return new FormViewModel
        {
            Id = form.Id,
            FormTemplateId = form.FormTemplateId,
            FormTemplateTitle = form.FormTemplate?.Title, // Получаем название шаблона
            ApplicationUserId = form.ApplicationUserId,
            ApplicationUserName = form.ApplicationUser?.Name, // Получаем имя пользователя
            SubmissionDate = form.SubmissionDate,
            Answers = form.Answers?.Select(answer => answer.ToViewModel()).ToList() // Маппим ответы
        };
    }

    public static Form ToModel(this FormViewModel viewModel)
    {
        return new Form
        {
            Id = viewModel.Id,
            FormTemplateId = viewModel.FormTemplateId,
            ApplicationUserId = viewModel.ApplicationUserId,
            SubmissionDate = viewModel.SubmissionDate,
            Answers = viewModel.Answers?.Select(answerViewModel => answerViewModel.ToModel()).ToList() // Маппим ответы
        };
    }
}
*/