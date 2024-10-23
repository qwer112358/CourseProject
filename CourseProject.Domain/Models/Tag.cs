namespace CourseProject.Domain.Models;

public class Tag
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<FormTemplate> FormTemplates { get; set; }
}