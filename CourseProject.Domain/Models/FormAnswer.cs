namespace CourseProject.Domain.Models;

public class FormAnswer {
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
    public string AnswerText { get; set; }
}