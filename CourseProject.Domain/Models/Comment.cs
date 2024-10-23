namespace CourseProject.Domain.Models;

public class Comment {
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } 
    public FormTemplate FormTemplate { get; set; } 
}