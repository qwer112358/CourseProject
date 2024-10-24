using CourseProject.Application.ViewModels;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Mappers;

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

    public static Tag ToModel(this TagViewModel viewModel)
    {
        return new Tag
        {
            Id = viewModel.Id,
            Name = viewModel.Name
        };
    }
}
