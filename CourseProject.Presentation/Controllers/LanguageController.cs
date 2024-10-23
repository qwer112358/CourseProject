using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

public class LanguageController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}