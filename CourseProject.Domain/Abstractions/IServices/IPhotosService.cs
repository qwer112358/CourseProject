using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
namespace CourseProject.Domain.Abstractions.IServices;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicUrl);
}