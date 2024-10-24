namespace CourseProject.Presentation.ViewModels;

public class FormViewModel
{
    public Guid Id { get; set; }
    public Guid FormTemplateId { get; set; }
    public string FormTemplateTitle { get; set; }
    public string ApplicationUserId { get; set; }
    public string ApplicationUserName { get; set; } 
    public DateTime SubmissionDate { get; set; }
    public List<FormAnswerViewModel> Answers { get; set; } // Include answers
}
