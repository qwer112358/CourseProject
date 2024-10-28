using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CourseProject.Presentation.ViewModels.FormTemplateViewModels;

public class FormTemplateRequestViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; } = string.Empty;
    [Url]
    public string? ImageUrl { get; set; }
    public bool IsPublic { get; set; }
    public Guid TopicId { get; set; }
    public string CreatorUserName { get; set; }
    public List<Guid> SelectedTagIds { get; set; } = new List<Guid>();
    [Required(ErrorMessage = "Add questions")]
    public List<QuestionViewModel> Questions { get; set; }
    public List<string> AllowedUserIds { get; set; } = new();
}