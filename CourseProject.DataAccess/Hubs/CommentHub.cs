using System.Security.Claims;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace CourseProject.DataAccess.Hubs;
//(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
/*var user = await userManager.FindByNameAsync(userName);
var comment = new Comment
{
    Text = commentText,
    ApplicationUserId = user.Id,
    FormTemplateId = formTemplateId
};
await commentsService.CreateCommentAsync(comment);*/
public class CommentHub(ICommentsService commentsService, UserManager<ApplicationUser> userManager) : Hub
{
    public async Task SendComment(string userName, string commentText, Guid formTemplateId)
    {
        if (string.IsNullOrWhiteSpace(commentText)) return;
        var user = await userManager.FindByNameAsync(userName);
        var comment = new Comment
        {
            Text = commentText,
            ApplicationUserId = user.Id,
            FormTemplateId = formTemplateId
        };
        await commentsService.CreateCommentAsync(comment);
        await Clients.All.SendAsync("ReceiveComment", $"{userName} {(comment.CreatedAt.ToLocalTime())}", commentText);
    }
}
