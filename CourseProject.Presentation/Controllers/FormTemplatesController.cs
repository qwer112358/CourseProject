using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using CourseProject.Presentation.Mappers;
using CourseProject.Presentation.ViewModels;
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
    IAccessService accessService,
    UserManager<ApplicationUser> userManager)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string searchTerm)
    {
        var formTemplates = await formTemplatesService.SearchFormTemplatesAsync(searchTerm);
        if (User.Identity.IsAuthenticated)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var accessibleTemplates = formTemplates
                .Where(ft => ft.IsPublic || ft.AllowedUsers.Select(u => u.Id).Contains(user.Id) || ft.CreatorId == user.Id)
                .ToList();
            
            var viewModel = SearchModelMapper.ToSearchViewModel(accessibleTemplates, searchTerm);
            return View(viewModel);
        }
        else
        {
            var publicTemplates = formTemplates
                .Where(ft => ft.IsPublic)
                .ToList();
            var viewModel = SearchModelMapper.ToSearchViewModel(publicTemplates, searchTerm);
            return View(viewModel);
        }
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
    
    [HttpGet("Search")]
    public async Task<IActionResult> Search(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) RedirectToAction("Index", "FormTemplates", new SearchViewModel());
        var formTemplates = await formTemplatesService.SearchFormTemplatesAsync(searchTerm);
        var viewModel = SearchModelMapper.ToSearchViewModel(formTemplates, searchTerm);
        return RedirectToAction("Index", "FormTemplates", viewModel);
    }
    
    private async Task InitData()
    {
        ViewBag.AllTopics = await topicsService.GetAllTopicsAsync();
        ViewBag.Tags = await tagsService.GetAllTagsAsync();
        ViewBag.Users = userManager.Users.Select(u => u.ToViewModel()).ToList();;
    }
}
