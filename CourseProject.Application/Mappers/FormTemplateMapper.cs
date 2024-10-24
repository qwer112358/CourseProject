using CourseProject.Application.ViewModels;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Mappers;

public static class FormTemplateMapper
{
    public static FormTemplateViewModel ToViewModel(this FormTemplate formTemplate)
    {
        return new FormTemplateViewModel
        {
            Id = formTemplate.Id,
            Title = formTemplate.Title,
            Description = formTemplate.Description,
            TopicId = formTemplate.TopicId,
            TopicName = formTemplate.Topic?.Name,
            ImageUrl = formTemplate.ImageUrl,
            IsPublic = formTemplate.IsPublic,
            Tags = formTemplate.Tags.Select(tag => tag.ToViewModel()).ToList(),
            Questions = formTemplate.Questions.Select(q => q.ToViewModel()).ToList(),
            CreatorName = formTemplate.Creator?.Name, 
            LikeCount = formTemplate.Likes.Count,
            CommentCount = formTemplate.Comments.Count
        };
    }

    public static FormTemplate ToModel(this FormTemplateViewModel viewModel)
    {
        return new FormTemplate
        {
            Id = viewModel.Id,
            Title = viewModel.Title,
            Description = viewModel.Description,
            TopicId = viewModel.TopicId,
            ImageUrl = viewModel.ImageUrl,
            IsPublic = viewModel.IsPublic,
            Tags = viewModel.Tags.Select(tag => tag.ToModel()).ToList(),
            Questions = viewModel.Questions.Select(q => q.ToModel(viewModel.Id)).ToList()
        };
    }
}
