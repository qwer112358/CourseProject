using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;

namespace CourseProject.Application.Services;

public class CommentsService(ICommentsRepository commentsRepository) : ICommentsService
{
    public async Task<ICollection<Comment>> GetAllCommentsAsync()
    {
        return await commentsRepository.GetAll();
    }

    public async Task<Comment> GetCommentByIdAsync(Guid id)
    {
        return await commentsRepository.GetById(id);
    }

    public async Task CreateCommentAsync(Comment comment)
    {
        await commentsRepository.Create(comment);
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        await commentsRepository.Update(comment);
    }

    public async Task DeleteCommentAsync(Guid id)
    {
        await commentsRepository.Delete(id);
    }

    public async Task<ICollection<Comment>> GetCommentsByUserIdAsync(string userId)
    {
        return await commentsRepository.GetCommentsByUserId(userId);
    }
}