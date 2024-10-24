using Microsoft.AspNetCore.Identity;

namespace CourseProject.Domain.Models;

public class ApplicationUser : IdentityUser 
{
    public string Name { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginDate { get; set; } = DateTime.UtcNow;
    public bool IsBlocked { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<Form> Forms { get; set; } = new List<Form>();
    public ICollection<FormTemplate> FormTemplates { get; set; } = new List<FormTemplate>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
}