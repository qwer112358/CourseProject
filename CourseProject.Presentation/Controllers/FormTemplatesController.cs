using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using CourseProject.Presentation.Mappers;
using CourseProject.Presentation.ViewModels.FormTemplateViewModels;
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
    UserManager<ApplicationUser> userManager)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string searchTerm)
    {
        var formTemplates = await formTemplatesService.SearchFormTemplatesAsync(searchTerm);
        var viewModel = SearchModelMapper.ToSearchViewModel(formTemplates, searchTerm);
        return View(viewModel);
    }
    
    [HttpGet("GetByUserName/{userName}")]
    public async Task<IActionResult> GetByUserName(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null) return NotFound();
        var formTemplates = await formTemplatesService.GetFormTemplatesByUserId(user.Id);
        //if (formTemplates is null || !formTemplates.Any()) return NotFound();

        var model = SearchModelMapper.ToSearchViewModel(formTemplates, string.Empty);
        return View(model);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        await InitData();
        return View();
    }
    
    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(FormTemplateRequestViewModel viewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var creator =  await userManager.FindByNameAsync(viewModel.CreatorUserName);
        var selectedTags = await tagsService.GetTagsByIdsAsync(viewModel.SelectedTagIds);
        var topic = await topicsService.GetTopicByIdAsync(viewModel.TopicId);
        var formTemplate = viewModel.ToDomain(creator!, selectedTags, topic);
        await formTemplatesService.CreateFormTemplate(formTemplate);
        return RedirectToAction("Index");
    }
    
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        var model = formTemplate.ToDetailViewModel();
        return View(model);
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
        if (formTemplate is null || formTemplate.CreatorId != userManager.GetUserId(User)) return Unauthorized();
        await InitData();
        var viewModel = formTemplate.ToRequestViewModel();
        return View(viewModel);
    }

    [Authorize]
    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id, FormTemplateRequestViewModel viewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var creator =  await userManager.FindByNameAsync(viewModel.CreatorUserName);
        var selectedTags = await tagsService.GetTagsByIdsAsync(viewModel.SelectedTagIds);
        var topic = await topicsService.GetTopicByIdAsync(viewModel.TopicId);
        var formTemplate = viewModel.ToDomain(creator!, selectedTags, topic);
        await formTemplatesService.UpdateFormTemplate(formTemplate);
        return RedirectToAction("Index");
    }

    [Authorize]
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate is null || formTemplate.CreatorId != userManager.GetUserId(User)) return Unauthorized();
        return View(formTemplate);
    }

    [Authorize]
    [HttpPost("DeleteConfirmed/{id}")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate is null || formTemplate.CreatorId != userManager.GetUserId(User)) return Unauthorized();
        await formTemplatesService.DeleteFormTemplate(id);
        return RedirectToAction("Index");
    }

    private async Task InitData()
    {
        ViewBag.AllTopics = await topicsService.GetAllTopicsAsync();
        ViewBag.Tags = await tagsService.GetAllTagsAsync();
    }
}
