using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;

namespace CourseProject.Presentation.Mappers;

public static class CommentMapper
{
    public static CommentViewModel ToViewModel(this Comment comment)
    {
        return new CommentViewModel
        {
            Id = comment.Id,
            Text = comment.Text,
            ApplicationUserName = comment.ApplicationUser?.Name,
            CreatedAt = comment.CreatedAt
        };
    }

    public static Comment ToModel(this CommentViewModel viewModel)
    {
        return new Comment
        {
            Id = viewModel.Id,
            Text = viewModel.Text,
            CreatedAt = viewModel.CreatedAt,
            ApplicationUserId = viewModel.ApplicationUserId,
            FormTemplateId = viewModel.FormTemplateId
        };
    }
}
