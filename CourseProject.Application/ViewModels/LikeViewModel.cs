namespace CourseProject.Application.ViewModels;

public class LikeViewModel
{
    public Guid Id { get; set; }
    public Guid FormTemplateId { get; set; }
    public string FormTemplateTitle { get; set; } 
    public string ApplicationUserId { get; set; }
    public string ApplicationUserName { get; set; } 
}
