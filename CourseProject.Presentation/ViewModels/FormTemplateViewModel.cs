using System.ComponentModel.DataAnnotations;

namespace CourseProject.Presentation.ViewModels;

public class FormTemplateViewModel
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "The Topic field is required.")]
    public Guid TopicId { get; set; }
    
    [Url]
    public string ImageUrl { get; set; }
    public bool IsPublic { get; set; } = true;
    public List<Guid> SelectedTagIds { get; set; } = new List<Guid>();
    /*public string CreatorId { get; set; }
    public string CreatorName { get; set; }*/
}
