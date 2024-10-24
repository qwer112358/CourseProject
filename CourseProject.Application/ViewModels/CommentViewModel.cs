namespace CourseProject.Application.ViewModels;

public class CommentViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ApplicationUserId { get; set; }
    public string ApplicationUserName { get; set; } // Display user's name
    public Guid FormTemplateId { get; set; }
}
