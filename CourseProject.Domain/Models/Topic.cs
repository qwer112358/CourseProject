using System.Collections;

namespace CourseProject.Domain.Models;

public class Topic
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public ICollection<FormTemplate> FormTemplates { get; set; } = new List<FormTemplate>();
}