namespace CourseProject.Domain.Models;

public class FormTemplate 
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid TopicId { get; set; }
    public Topic Topic { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public bool IsPublic { get; set; }
    public ICollection<Question> Questions { get; set; }
    public Guid CreatorId { get; set; }
    public ApplicationUser Creator { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<Comment> Comments { get; set; }
}