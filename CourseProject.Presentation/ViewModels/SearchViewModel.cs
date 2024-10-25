using CourseProject.Domain.Models;

namespace CourseProject.Presentation.ViewModels;

public class SearchViewModel
{
    public string SearchTerm { get; set; }
    public ICollection<FormTemplate> FormTemplates { get; set; }
}
