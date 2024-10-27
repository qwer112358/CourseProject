using System.Net.Http.Headers;
using System.Text.Json;
using CourseProject.Domain.Abstractions.IServices;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

public class ImagesService : IImagesService
{
    private readonly IConfiguration configuration;
    private readonly HttpClient httpClient;
    private readonly string apiToken;
    private readonly string uploadUrl;
    private readonly string deleteUrl;

    public ImagesService(IConfiguration configuration, HttpClient httpClient)
    {
        this.configuration = configuration;
        this.httpClient = httpClient;
        this.apiToken = configuration["Cloudflare:ApiToken"];
        this.uploadUrl = configuration["Cloudflare:UploadUrl"];
        this.deleteUrl = configuration["Cloudflare:DeleteUrl"];

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
    }

    public async Task<Result<string>> AddImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return Result.Failure<string>("Invalid file");

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var imageBytes = memoryStream.ToArray();

        var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(imageBytes), "file", file.FileName }
        };

        var response = await httpClient.PostAsync(uploadUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(jsonResponse);
            var imageUrl = jsonDocument.RootElement.GetProperty("result").GetProperty("url").GetString();

            return Result.Success(imageUrl);
        }

        return Result.Failure<string>($"Failed to upload image: {response.ReasonPhrase}");
    }

    public async Task<Result> DeleteImageAsync(string publicUrl)
    {
        if (string.IsNullOrEmpty(publicUrl))
            return Result.Failure("Invalid URL");

        var deleteRequestUrl = $"{deleteUrl}/{publicUrl}";

        var response = await httpClient.DeleteAsync(deleteRequestUrl);

        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return Result.Failure($"Failed to delete image: {response.ReasonPhrase}");
    }
}
