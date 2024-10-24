using FormBuilder.Domain.Enums;

namespace CourseProject.Presentation.ViewModels;

public class QuestionViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public QuestionType Type { get; set; }
}