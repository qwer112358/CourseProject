using FormBuilder.Domain.Enums;

namespace CourseProject.Domain.Models;

public class Question
{
    public Guid Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } 
    public QuestionType Type { get; set; } // Enum: SingleLine, MultiLine, Integer, Checkbox
    public int Order { get; set; }
    public Guid FormTemplateId { get; set; }
    public FormTemplate FormTemplate { get; set; }
    public ICollection<FormAnswer> FormAnswers { get; set; } = new List<FormAnswer>();
}