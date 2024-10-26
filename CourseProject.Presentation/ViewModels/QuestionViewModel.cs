using FormBuilder.Domain.Enums;

namespace CourseProject.Presentation.ViewModels;

public class QuestionViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    public QuestionType Type { get; set; }
}