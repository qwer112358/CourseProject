using FormBuilder.Domain.Enums;

namespace CourseProject.Presentation.ViewModels;

/*public class FormViewModel
{
    public Guid Id { get; set; }
    public Guid FormTemplateId { get; set; }
    public string FormTemplateTitle { get; set; }
    public string ApplicationUserId { get; set; }
    public string ApplicationUserName { get; set; } 
    public DateTime SubmissionDate { get; set; }
    public List<FormAnswerViewModel> Answers { get; set; } // Include answers
}*/

public class FormViewModel
{
    public Guid FormTemplateId { get; set; }
    public string? ApplicationUserId { get; set; }
    public List<FormQuestionViewModel>? Questions { get; set; } = new List<FormQuestionViewModel>();
}

public class FormQuestionViewModel
{
    public Guid QuestionId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public QuestionType Type { get; set; }
    public string? AnswerText { get; set; } // Для SingleLine, MultiLine
    public int? AnswerInteger { get; set; } // Для Integer
    public bool AnswerCheckbox { get; set; } // Для Checkbox
}
