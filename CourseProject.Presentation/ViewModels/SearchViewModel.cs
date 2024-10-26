using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels.FormTemplateViewModels;

namespace CourseProject.Presentation.ViewModels;

public class SearchViewModel
{
    public string SearchTerm { get; set; } = string.Empty;
    public ICollection<FormTemplateViewModel> FormTemplates { get; set; }
}
