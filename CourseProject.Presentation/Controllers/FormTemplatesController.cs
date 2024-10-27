using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using CourseProject.Presentation.Mappers;
using CourseProject.Presentation.ViewModels;
using CourseProject.Presentation.ViewModels.FormTemplateViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Presentation.Controllers;

[Route("[controller]")]
public class FormTemplatesController(
    IFormTemplatesService formTemplatesService,
    ITagsService tagsService,
    ITopicsService topicsService,
    IAccessService accessService,
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
        if (user is null) return NotFound();
        var formTemplates = await formTemplatesService.GetFormTemplatesByUserId(user.Id);
        var model = SearchModelMapper.ToSearchViewModel(formTemplates, string.Empty);
        return View(model);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        await InitData();
        var users = userManager.Users.ToList();
        
        var userViewModels = users.Select(u => new ApplicationUserViewModel
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email
        }).ToList();
        
        ViewBag.Users = userViewModels;
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
        var allowedUsers = await Task.WhenAll(viewModel.AllowedUserIds.Select(id => userManager.FindByIdAsync(id)));
        var formTemplate = viewModel.ToDomain(creator!, selectedTags, topic, allowedUsers);
        await formTemplatesService.CreateFormTemplate(formTemplate);
        return RedirectToAction("Index");
    }
    
    [HttpGet("GetUsers")]
    public IActionResult GetUsers(string searchTerm)
    {
        var users = userManager.Users
            .Where(u => u.Name.Contains(searchTerm) || u.Email.Contains(searchTerm))
            .Select(u => new { Id = u.Id, Name = u.Name, Email = u.Email })
            .ToList();
        return Json(users);
    }
    
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        var model = formTemplate.ToDetailViewModel();
        return View(model);
    }

    [Authorize]
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate is null || !accessService.HasEditAccess(formTemplate.CreatorId, User))
            return Unauthorized();
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
        if (!accessService.HasEditAccess(creator.Id, User)) return Unauthorized();
        var selectedTags = await tagsService.GetTagsByIdsAsync(viewModel.SelectedTagIds);
        var topic = await topicsService.GetTopicByIdAsync(viewModel.TopicId);
        var allowedUsers = await Task.WhenAll(viewModel.AllowedUserIds.Select(id => userManager.FindByIdAsync(id)));
        var formTemplate = viewModel.ToDomain(creator!, selectedTags, topic, allowedUsers);
        await formTemplatesService.UpdateFormTemplate(formTemplate);
        return RedirectToAction("Index");
    }

    [Authorize]
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate is null || !accessService.HasEditAccess(formTemplate.CreatorId, User))
            return Unauthorized();
        return View(formTemplate);
    }

    [Authorize]
    [HttpPost("DeleteConfirmed/{id}")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate is null || !accessService.HasEditAccess(formTemplate.CreatorId, User))
            return Unauthorized();
        await formTemplatesService.DeleteFormTemplate(id);
        return RedirectToAction("Index");
    }

    private async Task InitData()
    {
        ViewBag.AllTopics = await topicsService.GetAllTopicsAsync();
        ViewBag.Tags = await tagsService.GetAllTagsAsync();
    }
    
    

    /*private void GetDomainProperties(out ApplicationUser user, ICollection<Tag> tags, Topic topic, ICollection<ApplicationUser> allowedUsers)
    {
    }*/
}
