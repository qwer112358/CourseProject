namespace CourseProject.Domain.Models;

public class Comment 
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } 
    public Guid FormTemplateId { get; set; }
    public FormTemplate FormTemplate { get; set; } 
}