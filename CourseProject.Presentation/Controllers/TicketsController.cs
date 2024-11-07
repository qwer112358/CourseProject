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
            var ticketUrl = await jiraService.CreateTicketAsync(model);
            return View("TicketCreated", ticketUrl);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("CreateTicket");
        }
    }
}
