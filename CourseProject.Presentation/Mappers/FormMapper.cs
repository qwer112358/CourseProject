using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;

namespace CourseProject.Presentation.Mappers;

public static class FormMapper
{
    public static FormViewModel ToViewModel(this Form form) => new()
    {
    };
    
    public static Form ToDomain(this FormViewModel viewModel, ApplicationUser user, FormTemplate formTemplate) => new()
    {
        Id = Guid.NewGuid(),
        FormTemplateId = viewModel.FormTemplateId,
        FormTemplate = formTemplate, 
        ApplicationUserId = user.Id,
        ApplicationUser = user,
        SubmissionDate = DateTime.UtcNow,
        Answers = viewModel.Questions.Select(q => q.ToFormAnswer(viewModel.FormTemplateId)).ToList() 

    };
}
