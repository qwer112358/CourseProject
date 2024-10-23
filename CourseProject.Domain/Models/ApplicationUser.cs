using Microsoft.AspNetCore.Identity;

namespace CourseProject.Domain.Models;

public class ApplicationUser : IdentityUser 
{
    public string? Name { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginDate { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsAdmin { get; set; } 
    //public ICollection<Form> Form { get; set; }
    //public ICollection<FormTemplate> FormTemplate { get; set; }
}