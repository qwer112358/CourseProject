using CourseProject.Application.ViewModels;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Mappers;

public static class LikeMapper
{
    public static LikeViewModel ToViewModel(this Like like)
    {
        return new LikeViewModel
        {
            Id = like.Id,
            FormTemplateId = like.FormTemplateId,
            FormTemplateTitle = like.FormTemplate?.Title,
            ApplicationUserId = like.ApplicationUserId,
            ApplicationUserName = like.ApplicationUser?.Name
        };
    }

    public static Like ToModel(this LikeViewModel viewModel)
    {
        return new Like
        {
            Id = viewModel.Id,
            FormTemplateId = viewModel.FormTemplateId,
            ApplicationUserId = viewModel.ApplicationUserId
        };
    }
}
