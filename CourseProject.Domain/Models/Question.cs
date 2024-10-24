using FormBuilder.Domain.Enums;

namespace CourseProject.Domain.Models;

public class Question {
    public Guid Id { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; } // Enum: SingleLine, MultiLine, Integer, etc.
    public FormTemplate FormTemplate { get; set; } 
}