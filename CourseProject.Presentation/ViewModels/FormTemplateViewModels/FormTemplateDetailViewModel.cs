using System.ComponentModel.DataAnnotations;

namespace CourseProject.Presentation.ViewModels.FormTemplateViewModels;

public class FormTemplateDetailViewModel
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    [Url]
    public string ImageUrl { get; set; }
    public TopicViewModel Topic { get; set; }
 
    public bool IsPublic { get; set; } 
    public ApplicationUserViewModel Creator { get; set; }
    public ICollection<TagViewModel> Tags { get; set; }
    public ICollection<CommentViewModel> Comments { get; set; }
    public ICollection<QuestionViewModel> Questions { get; set; }
    public int LikeCount { get; set; }
}