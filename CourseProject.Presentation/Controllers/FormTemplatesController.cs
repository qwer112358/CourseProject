using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using CourseProject.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

[Route("[controller]")]
public class FormTemplatesController(
    IFormTemplatesService formTemplatesService,
    ITagsService tagsService,
    ITopicsService topicsService,
    UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var formTemplates = await formTemplatesService.GetAllFormTemplates();
        return View(formTemplates);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateById(id);
        if (formTemplate == null)
        {
            return NotFound();
        }

        return Ok(formTemplate);
    }

    [HttpGet("Create")]
    public async Task<ActionResult> Create()
    {
        ViewData["AllTopics"] = await topicsService.GetAllTopicsAsync();
        ViewData["Tags"] = await tagsService.GetAllTagsAsync();
        return View();
    }
    
    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(FormTemplateViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await userManager.GetUserAsync(User);
    
        var formTemplate = new FormTemplate
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            Topic = await topicsService.GetTopicByIdAsync(viewModel.TopicId),
            Creator = user,
            CreatorId = user.Id,
            ImageUrl = viewModel.ImageUrl,
            IsPublic = viewModel.IsPublic,
            Tags = await tagsService.GetTagsByIdsAsync(viewModel.SelectedTagIds)
        };
        await formTemplatesService.CreateFormTemplate(formTemplate);
        return CreatedAtAction(nameof(GetById), new { id = formTemplate.Id }, formTemplate);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] FormTemplate formTemplate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingFormTemplate = await formTemplatesService.GetFormTemplateById(id);
        if (existingFormTemplate == null)
        {
            return NotFound();
        }

        formTemplate.Id = id;
        await formTemplatesService.UpdateFormTemplate(formTemplate);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateById(id);
        if (formTemplate == null)
        {
            return NotFound();
        }

        await formTemplatesService.DeleteFormTemplate(id);
        return NoContent();
    }
}