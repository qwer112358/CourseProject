using FormBuilder.Domain.Enums;

namespace CourseProject.Presentation.ViewModels;

public class FormQuestionViewModel
{
    public Guid QuestionId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
    public QuestionType Type { get; set; }
    public string? AnswerText { get; set; } // Для SingleLine, MultiLine
    public int? AnswerInteger { get; set; } // Для Integer
    public bool AnswerCheckbox { get; set; } // Для Checkbox
}