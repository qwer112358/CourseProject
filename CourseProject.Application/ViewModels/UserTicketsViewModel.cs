namespace CourseProject.Application.ViewModels;

public class UserTicketsViewModel
{
    public List<TicketViewModel> Tickets { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalTickets { get; set; }
}