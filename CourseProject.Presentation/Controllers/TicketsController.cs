using CourseProject.Application.Services;
using CourseProject.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

public class TicketsController(JiraService jiraService) : Controller
{
    [HttpGet]
    public IActionResult CreateTicket()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(CreateTicketViewModel model)
    {
        try
        {
            var userEmail = User.Identity.Name; 
            var ticketUrl = await jiraService.CreateTicketAsync(model, userEmail);
            ViewBag.TicketUrl = ticketUrl;
            return View("TicketCreated", ticketUrl);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("CreateTicket");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserTickets()
    {
        try
        {
            var userEmail = User.Identity.Name;
            var tickets = await jiraService.GetTicketsByUserAsync(userEmail);
            return View("UserTickets", tickets);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("UserTickets", new List<TicketViewModel>());
        }
    }
}