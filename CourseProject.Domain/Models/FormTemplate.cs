namespace CourseProject.Domain.Models;

public class FormTemplate 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid TopicId { get; set; }
    public Topic Topic { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPublic { get; set; } = true;
    public string CreatorId { get; set; }
    public ApplicationUser Creator { get; set; }
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Form> Forms { get; set; } = new List<Form>();
}