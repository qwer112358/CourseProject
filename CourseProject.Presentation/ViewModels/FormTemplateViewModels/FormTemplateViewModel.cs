using System.ComponentModel.DataAnnotations;

namespace CourseProject.Presentation.ViewModels.FormTemplateViewModels;

public class FormTemplateViewModel
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
    public int LikeCount { get; set; }
    public int FormCount { get; set; }
}
