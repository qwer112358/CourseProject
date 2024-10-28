using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using CourseProject.Domain.Abstractions.IServices;
using Microsoft.AspNetCore.Http;

namespace CourseProject.Application.Services;

public class ImagesService : IImagesService
{
    private readonly string _bucketName = "images";
    private readonly string _accessKey = "a079a21eba17ddbc81b49f5caf0e0c45";
    private readonly string _secretKey = "0a024bda86d4dd404fb7d65046654e0a2a1871ded836dafac0f50077b1bbb688";
    private readonly string _r2Endpoint = "https://058f861565d127669b780025609f1891.r2.cloudflarestorage.com";
    private readonly string _r2Dev = "https://pub-d6017daddbcc4afe91a5b070dfbf9a39.r2.dev";
    private readonly IAmazonS3 _s3Client;
    
    public ImagesService()
    {
        var config = new AmazonS3Config
        {
            ServiceURL = _r2Endpoint,
            UseHttp = true,
            ForcePathStyle = true
        };
        var credentials = new BasicAWSCredentials(_accessKey, _secretKey);
        _s3Client = new AmazonS3Client(credentials, config);
    }

    public async Task<string> UploadImageAsync(IFormFile? file)
    {
        var keyName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(Path.GetTempPath(), keyName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var uploadRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = keyName,
            InputStream = fileStream,
            ContentType = "image/jpeg",
            DisablePayloadSigning = true
        };
        await _s3Client.PutObjectAsync(uploadRequest);
        File.Delete(filePath);
        return $"{_r2Dev}/{keyName}";
    }

    public async Task DeleteImageAsync(string keyName)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = keyName,
        };
        await _s3Client.DeleteObjectAsync(deleteRequest);
    }
}