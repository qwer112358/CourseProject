using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;

namespace CourseProject.Presentation.Mappers;

public static class TagMapper
{
   
    public static TagViewModel ToViewModel(this Tag tag)
    {
        return new TagViewModel
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }
//
    public static Tag ToDomain(this TagViewModel viewModel)
    {
        return new Tag
        {
            Id = viewModel.Id,
            Name = viewModel.Name
        };
    }
}
