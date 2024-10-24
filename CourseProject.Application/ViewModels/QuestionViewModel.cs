using FormBuilder.Domain.Enums;

namespace CourseProject.Application.ViewModels;

public class QuestionViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public QuestionType Type { get; set; }
}