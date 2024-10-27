using CloudinaryDotNet.Actions;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
namespace CourseProject.Domain.Abstractions.IServices;

public interface IImagesService
{
    Task<Result<string>> AddImageAsync(IFormFile file);
    Task<Result> DeleteImageAsync(string publicUrl);
}