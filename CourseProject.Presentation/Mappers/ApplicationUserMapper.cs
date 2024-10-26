using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;

namespace CourseProject.Presentation.Mappers;

public static class ApplicationUserMapper
{
    public static ApplicationUserViewModel ToViewModel(this ApplicationUser user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };

    public static ApplicationUser ToDomain(this ApplicationUserViewModel viewModel) => new()
    {
        Id = viewModel.Id,
        Name = viewModel.Name,
        Email = viewModel.Email
    };
}