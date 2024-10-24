namespace CourseProject.Presentation.ViewModels;

public class FormAnswerViewModel
{
    public Guid Id { get; set; }
    public Guid FormId { get; set; }
    public Guid QuestionId { get; set; }
    public string AnswerText { get; set; } 
    public int? AnswerInteger { get; set; }
    public bool? AnswerCheckbox { get; set; }
}