using CourseProject.Domain.Abstractions.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController(IImagesService imagesService) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile? file)
    {
        if (file is null || file.Length == 0) return BadRequest("File not selected");
        var url = await imagesService.UploadImageAsync(file);
        return Ok(new { imageUrl = url });
    }

    [HttpDelete("delete/{keyName}")]
    public async Task<IActionResult> DeleteImage(string keyName)
    {
        await imagesService.DeleteImageAsync(keyName);
        return Ok();
    }
}