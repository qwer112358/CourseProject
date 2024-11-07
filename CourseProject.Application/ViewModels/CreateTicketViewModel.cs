namespace CourseProject.Application.ViewModels;

public class CreateTicketViewModel
{
    public string Summary { get; set; }
    public string Priority { get; set; }
    public string PageUrl { get; set; }
    public string Template { get; set; } = string.Empty;
}