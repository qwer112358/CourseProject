using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

public class LikesController(ILikesService likesService, UserManager<ApplicationUser> userManager) : ControllerBase
{
    [Authorize]
    [HttpPost("ToggleLike/{templateId}")]
    public async Task<IActionResult> ToggleLike(Guid templateId)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        await likesService.ToggleLike(templateId, user.Id);
        return RedirectToAction("Index", "FormTemplates", new { id = templateId });
    }

    [HttpGet("GetLikesCount/{templateId}")]
    public async Task<IActionResult> GetLikesCount(Guid templateId)
    {
        var likes = await likesService.GetLikesByTemplateId(templateId);
        return Ok(likes.Count);
    }
}