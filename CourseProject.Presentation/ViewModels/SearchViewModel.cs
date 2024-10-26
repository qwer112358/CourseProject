using CourseProject.Domain.Models;

namespace CourseProject.Presentation.ViewModels;

public class SearchViewModel
{
    public string SearchTerm { get; set; } = string.Empty;
    public ICollection<FormTemplate> FormTemplates { get; set; }
}
