namespace CourseProject.Presentation.ViewModels;

public class CommentViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ApplicationUserId { get; set; }
    public string ApplicationUserName { get; set; }
    public Guid FormTemplateId { get; set; }
}
