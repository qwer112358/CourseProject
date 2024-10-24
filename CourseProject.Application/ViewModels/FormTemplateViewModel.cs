namespace CourseProject.Application.ViewModels;

public class FormTemplateViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid TopicId { get; set; }
    public string TopicName { get; set; } 
    public string ImageUrl { get; set; }
    public bool IsPublic { get; set; }
    public List<string> TagNames { get; set; } 
    public List<QuestionViewModel> Questions { get; set; } 
    public string CreatorName { get; set; }
    public int LikeCount { get; set; } 
    public int CommentCount { get; set; } 
}