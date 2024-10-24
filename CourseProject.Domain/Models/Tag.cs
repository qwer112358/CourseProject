namespace CourseProject.Domain.Models;

public class Tag
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public ICollection<FormTemplate> FormTemplates { get; set; } = new List<FormTemplate>();
}