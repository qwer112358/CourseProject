namespace CourseProject.Domain.Models;

public class Like
{ 
    public Guid Id { get; set; }
    public Guid FormTemplateId { get; set; }
    public FormTemplate FormTemplate { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}