namespace CourseProject.Domain.Models;

public class FormAnswer
{
    public Guid Id { get; set; } 
    public Guid FormId { get; set; } 
    public Form Form { get; set; }
    public Guid QuestionId { get; set; } 
    public Question Question { get; set; }
    
    public string AnswerText { get; set; } // (for SingleLine, MultiLine)
    public int? AnswerInteger { get; set; } // (for Integer)
    public bool? AnswerCheckbox { get; set; } // (for Checkbox)
}