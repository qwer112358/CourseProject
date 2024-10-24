using System.Security.Claims;
using CourseProject.Application.ViewModels;
using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormTemplatesApiController(IFormTemplatesService formTemplatesService) : ControllerBase
{
     [HttpGet]
    public async Task<ActionResult<ICollection<FormTemplateViewModel>>> Index()
    {
        var formTemplates = await formTemplatesService.GetAllFormTemplates();
        var viewModels = formTemplates.Select(ft => new FormTemplateViewModel
        {
            Id = ft.Id,
            Title = ft.Title,
            Description = ft.Description,
            TopicId = ft.TopicId,
            TopicName = ft.Topic?.Name,
            ImageUrl = ft.ImageUrl,
            IsPublic = ft.IsPublic,
            TagNames = ft.Tags?.Select(t => t.Name).ToList(),
            Questions = ft.Questions?.Select(q => new QuestionViewModel
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type
            }).ToList() ?? new List<QuestionViewModel>(),
            CreatorName = ft.Creator?.Name,
            LikeCount = ft.Likes?.Count ?? 0,
            CommentCount = ft.Comments?.Count ?? 0
        }).ToList();

        return Ok(viewModels);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FormTemplateViewModel>> GetById(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateById(id);
        if (formTemplate is null)
            return NotFound();

        var viewModel = new FormTemplateViewModel
        {
            Id = formTemplate.Id,
            Title = formTemplate.Title,
            Description = formTemplate.Description,
            TopicId = formTemplate.TopicId,
            TopicName = formTemplate.Topic?.Name,
            ImageUrl = formTemplate.ImageUrl,
            IsPublic = formTemplate.IsPublic,
            TagNames = formTemplate.Tags?.Select(t => t.Name).ToList(),
            Questions = formTemplate.Questions?.Select(q => new QuestionViewModel
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type
            }).ToList(),
            CreatorName = formTemplate.Creator?.Name,
            LikeCount = formTemplate.Likes?.Count ?? 0,
            CommentCount = formTemplate.Comments?.Count ?? 0
        };

        return Ok(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(FormTemplateViewModel model)
    {
        var formTemplate = new FormTemplate
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Description = model.Description,
            TopicId = model.TopicId,
            ImageUrl = model.ImageUrl,
            IsPublic = model.IsPublic,
            CreatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        };

        await formTemplatesService.CreateFormTemplate(formTemplate);
        return CreatedAtAction(nameof(GetById), new { id = formTemplate.Id }, model);
    }
    
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, FormTemplateViewModel model)
    {
        var existingTemplate = await formTemplatesService.GetFormTemplateById(id);
        if (existingTemplate is null)
        {
            return NotFound();
        }

        existingTemplate.Title = model.Title;
        existingTemplate.Description = model.Description;
        existingTemplate.TopicId = model.TopicId;
        existingTemplate.ImageUrl = model.ImageUrl;
        existingTemplate.IsPublic = model.IsPublic;

        await formTemplatesService.UpdateFormTemplate(existingTemplate);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await formTemplatesService.DeleteFormTemplate(id);
        return NoContent();
    }
}
