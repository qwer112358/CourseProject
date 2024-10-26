using CourseProject.Domain.Models;

namespace CourseProject.Presentation.ViewModels;

public class FormViewModel
{
    public Guid FormTemplateId { get; set; }
    public string? ApplicationUserId { get; set; }
    public List<FormQuestionViewModel> Questions { get; set; } = new();
}