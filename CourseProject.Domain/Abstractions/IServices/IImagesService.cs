using CloudinaryDotNet.Actions;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
namespace CourseProject.Domain.Abstractions.IServices;

public interface IImagesService
{
    Task<string> UploadImageAsync(IFormFile? file);
    Task DeleteImageAsync(string publicUrl);
}