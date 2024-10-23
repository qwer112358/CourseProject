namespace CourseProject.Domain.Models;

public class Like
{ 
    public Guid Id { get; set; }
    public Guid TemplateId { get; set; }
    public FormTemplate FormTemplate { get; set; }
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}