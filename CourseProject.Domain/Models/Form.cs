namespace CourseProject.Domain.Models;

public class Form 
{
    public Guid Id { get; set; }
    public Guid FormTemplateId { get; set; }
    public FormTemplate FormTemplate { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } 
    public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;
    public ICollection<FormAnswer> Answers { get; set; }
}