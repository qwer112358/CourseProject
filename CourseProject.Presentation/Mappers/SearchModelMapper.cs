using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;

namespace CourseProject.Presentation.Mappers;

public static class SearchModelMapper
{
    public static SearchViewModel ToSearchViewModel(ICollection<FormTemplate> formTemplates, string searchTerm) => new()
    {
        SearchTerm = searchTerm,
        FormTemplates = formTemplates.Select(ft => ft.ToViewModel()).ToList(),
    };
}