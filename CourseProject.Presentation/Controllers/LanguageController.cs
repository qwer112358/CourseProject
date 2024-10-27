using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;

namespace CourseProject.Presentation.Controllers;

public class LanguageController : Controller
{
    [HttpPost]
    public IActionResult SetLanguage(string culture)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );
        return Redirect(Request.Headers["Referer"].ToString());
    }
}