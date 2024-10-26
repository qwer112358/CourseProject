using CourseProject.Domain.Abstractions.IRepositories;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Application.Services;

public class LikesService(ILikesRepository likesRepository) : ILikesService
{
    public async Task ToggleLike(Guid templateId, string userId)
    {
        var isLiked = await likesRepository.IsLikedByUser(templateId, userId);

        if (isLiked)
        {
            var existingLike = await likesRepository.Find(l => l.FormTemplateId == templateId && l.ApplicationUserId == userId).FirstOrDefaultAsync();
            if (existingLike != null)
            {
                await likesRepository.Delete(existingLike.Id);
            }
        }
        else
        {
            var newLike = new Like
            {
                FormTemplateId = templateId,
                ApplicationUserId = userId
            };
            await likesRepository.Create(newLike);
        }
    }

    public async Task<ICollection<Like>> GetLikesByTemplateId(Guid templateId)
    {
        return await likesRepository.Find(l => l.FormTemplateId == templateId).ToListAsync();
    }
}