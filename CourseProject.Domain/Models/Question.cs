using FormBuilder.Domain.Enums;

namespace CourseProject.Domain.Models;

public class Question {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public QuestionType Type { get; set; } // Enum: SingleLine, MultiLine, Integer, etc.
    public bool ShowInSummary { get; set; }
    public FormTemplate FormTemplate { get; set; } 
}