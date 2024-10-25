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
    ICommentsService commentsService,
    IQuestionService questionService,
    UserManager<ApplicationUser> userManager)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string searchTerm)
    {
        var formTemplates = await formTemplatesService.SearchFormTemplatesAsync(searchTerm);
        
        var viewModel = new SearchViewModel
        {
            SearchTerm = searchTerm,
            FormTemplates = formTemplates
        };

        return View(viewModel);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate == null)
        {
            return NotFound();
        }

        return Ok(formTemplate);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        ViewBag.AllTopics = await topicsService.GetAllTopicsAsync();
        ViewBag.Tags = await tagsService.GetAllTagsAsync();
        return View();
    }
    
    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(FormTemplateViewModel viewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

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
            Tags = await tagsService.GetTagsByIdsAsync(viewModel.SelectedTagIds),
            Questions = viewModel.Questions.Select(q => new Question
            {
                Title = q.Title,
                Description = q.Description,
                Type = q.Type,
                Order = q.Order
                }).ToList()
        };
        await formTemplatesService.CreateFormTemplate(formTemplate);
        return RedirectToAction("Index");
    }
    
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate == null)
        {
            return NotFound();
        }

        return View(formTemplate);
    }
    
    [Authorize]
    [HttpPost("AddComment")]
    public async Task<IActionResult> AddComment(Guid formTemplateId, string text)
    {
        if (string.IsNullOrWhiteSpace(text)) 
        {
            ModelState.AddModelError(string.Empty, "Comment cannot be empty.");
            return RedirectToAction("Details", new { id = formTemplateId });
        }

        var user = await userManager.GetUserAsync(User);
        var comment = new Comment
        {
            Text = text,
            ApplicationUserId = user.Id,
            FormTemplateId = formTemplateId
        };

        await commentsService.CreateCommentAsync(comment);

        return RedirectToAction("Details", new { id = formTemplateId });
    }

    [Authorize]
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate == null || formTemplate.CreatorId != userManager.GetUserId(User))
        {
            return Unauthorized();
        }

        
        var viewModel = new FormTemplateViewModel
        {
            Title = formTemplate.Title,
            Description = formTemplate.Description,
            TopicId = formTemplate.TopicId,
            ImageUrl = formTemplate.ImageUrl,
            IsPublic = formTemplate.IsPublic,
            SelectedTagIds = formTemplate.Tags.Select(tag => tag.Id).ToList(),
            Questions = formTemplate.Questions.Select(q => new QuestionViewModel
            {
                Id = q.Id,
                Title = q.Title,
                Description = q.Description,
                Type = q.Type,
            }).ToList()
        };

        ViewData["AllTopics"] = await topicsService.GetAllTopicsAsync();
        ViewData["Tags"] = await tagsService.GetAllTagsAsync();

        return View(viewModel);
    }

    [Authorize]
    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id, FormTemplateViewModel viewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingFormTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (existingFormTemplate == null || existingFormTemplate.CreatorId != userManager.GetUserId(User))
        {
            return Unauthorized();
        }

        existingFormTemplate.Title = viewModel.Title;
        existingFormTemplate.Description = viewModel.Description;
        existingFormTemplate.Topic = await topicsService.GetTopicByIdAsync(viewModel.TopicId);
        existingFormTemplate.ImageUrl = viewModel.ImageUrl;
        existingFormTemplate.IsPublic = viewModel.IsPublic;
        existingFormTemplate.Tags = await tagsService.GetTagsByIdsAsync(viewModel.SelectedTagIds);
        existingFormTemplate.Questions = viewModel.Questions.Select(q => new Question
        {
            Id = q.Id,
            Title = q.Title,
            Description = q.Description,
            Type = q.Type,
            Order = q.Order,
            FormTemplateId = existingFormTemplate.Id
        }).ToList();

        await formTemplatesService.UpdateFormTemplate(existingFormTemplate);

        return RedirectToAction("Index");
    }


    [Authorize]
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate == null || formTemplate.CreatorId != userManager.GetUserId(User))
        {
            return Unauthorized();
        }

        return View(formTemplate);
    }

    [Authorize]
    [HttpPost("DeleteConfirmed/{id}")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate == null || formTemplate.CreatorId != userManager.GetUserId(User))
        {
            return Unauthorized();
        }

        await formTemplatesService.DeleteFormTemplate(id);
        return RedirectToAction("Index");
    }
}
