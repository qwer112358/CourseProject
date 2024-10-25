using CourseProject.Domain.Models;

namespace CourseProject.Domain.Abstractions.IServices;

public interface ICommentsService
{
    Task<ICollection<Comment>> GetAllCommentsAsync();
    Task<Comment> GetCommentByIdAsync(Guid id);
    Task CreateCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(Guid id);
    Task<ICollection<Comment>> GetCommentsByUserIdAsync(string userId);
}