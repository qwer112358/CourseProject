using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

public class CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
    : ControllerBase
{
    [Authorize]
    [HttpPost("AddComment")]
    public async Task<IActionResult> AddComment(Guid formTemplateId, string text)
    {
        if (string.IsNullOrWhiteSpace(text)) 
        {
            ModelState.AddModelError(string.Empty, "Comment cannot be empty.");
            return RedirectToAction("Details", new { id = formTemplateId });
        }
        
        var user = await userManager.GetUserAsync(User);
        var comment = new Comment
        {
            Text = text,
            ApplicationUserId = user.Id,
            FormTemplateId = formTemplateId
        };
        await commentsService.CreateCommentAsync(comment);
        return RedirectToAction("Details", "FormTemplates", new { id = formTemplateId });
    }
}