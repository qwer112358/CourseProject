using CourseProject.Application.ViewModels;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Mappers;

public static class CommentMapper
{
    public static CommentViewModel ToViewModel(this Comment comment)
    {
        return new CommentViewModel
        {
            Id = comment.Id,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            ApplicationUserId = comment.ApplicationUserId,
            ApplicationUserName = comment.ApplicationUser?.Name, 
            FormTemplateId = comment.FormTemplateId
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
