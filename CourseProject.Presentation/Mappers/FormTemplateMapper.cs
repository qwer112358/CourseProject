using System.Runtime.CompilerServices;
using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;
using CourseProject.Presentation.ViewModels.FormTemplateViewModels;

namespace CourseProject.Presentation.Mappers;

public static class FormTemplateMapper
{
    public static FormTemplateDetailViewModel ToDetailViewModel(this FormTemplate formTemplate) => new()
    {
        Id = formTemplate.Id,
        Title = formTemplate.Title,
        Description = formTemplate.Description,
        Creator = formTemplate.Creator.ToViewModel(),
        IsPublic = formTemplate.IsPublic,
        ImageUrl = formTemplate.ImageUrl,
        LikeCount = formTemplate.Likes.Count,
        Topic = formTemplate.Topic.ToViewModel(),
        Tags = formTemplate.Tags.Select(t => t.ToViewModel()).ToList(),
        Questions = formTemplate.Questions.Select(q => q.ToViewModel()).ToList(),
        Comments = formTemplate.Comments.Select(c => c.ToViewModel()).ToList(),
        AllowedUsers = formTemplate.AllowedUsers.Select(u => u.ToViewModel()).ToList(),
    };

    public static FormTemplateViewModel ToViewModel(this FormTemplate formTemplate) => new()
    {
        Id = formTemplate.Id,
        Title = formTemplate.Title,
        Description = formTemplate.Description,
        Creator = formTemplate.Creator.ToViewModel(),
        IsPublic = formTemplate.IsPublic,
        ImageUrl = formTemplate.ImageUrl,
        Topic = formTemplate.Topic.ToViewModel(),
        LikeCount = formTemplate.Likes.Count,
        FormCount = formTemplate.Forms.Count,
    };

    public static FormTemplate ToDomain(this FormTemplateRequestViewModel viewModel, ApplicationUser creator,
        ICollection<Tag> tags, Topic topic, ICollection<ApplicationUser> allowedUsers)
    {
        var formTemplate = new FormTemplate
        {
            Id = viewModel.Id,
            Title = viewModel.Title,
            Description = viewModel.Description,
            TopicId = viewModel.TopicId,
            Topic = topic,
            Creator = creator,
            CreatorId = creator.Id,
            ImageUrl = viewModel.ImageUrl,
            IsPublic = viewModel.IsPublic,
            Tags = tags,
            AllowedUsers = allowedUsers
        };
        formTemplate.Questions = viewModel.Questions.Select(q => q.ToDomain(formTemplate.Id, formTemplate)).ToList();
        return formTemplate;
    }


    public static FormTemplateRequestViewModel ToRequestViewModel(this FormTemplate formTemplate) => new()
    {
        Id = formTemplate.Id,
        Title = formTemplate.Title,
        Description = formTemplate.Description,
        ImageUrl = formTemplate.ImageUrl,
        IsPublic = formTemplate.IsPublic,
        TopicId = formTemplate.TopicId,
        CreatorUserName = formTemplate.Creator.UserName,
        SelectedTagIds = formTemplate.Tags.Select(t => t.Id).ToList(),
        Questions = formTemplate.Questions.Select(q => q.ToViewModel()).ToList(),
        AllowedUserIds = formTemplate.AllowedUsers.Select(u => u.Id).ToList()
    };
}