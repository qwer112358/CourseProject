using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
using CourseProject.Presentation.Mappers;
using CourseProject.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

[Route("[controller]")]
public class FormsController(
    IFormTemplatesService formTemplatesService,
    IFormsService formsService,
    UserManager<ApplicationUser> userManager,
    IQuestionService questionService) : Controller
{
    [Authorize]
    [HttpGet("{formTemplateId}")]
    public async Task<IActionResult> Index(Guid formTemplateId)
    {
        var forms = await formsService.GetFormsByTemplateIdAsync(formTemplateId);
        var questionTasks = forms.Select(form =>
            questionService.GetQuestionByFormTemplateIdAsync(form.FormTemplateId));
        var questionsList = await Task.WhenAll(questionTasks);
        forms.Zip(questionsList, (form, questions) => form.FormTemplate.Questions = questions);
        return View(forms);
    }
    
    [Authorize]
    [HttpGet("Create/{id}")]
    public async Task<IActionResult> Create(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        var viewModel = new FormViewModel
        {
            FormTemplateId = formTemplate.Id,
            Questions = formTemplate.Questions.Select(q => q.ToFormQuestionViewModel()).ToList()
        };
        return View(viewModel);
    }

    [Authorize]
    [HttpPost("Create/{id}")]
    public async Task<IActionResult> Create(FormViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(model.FormTemplateId);
        var user = await userManager.GetUserAsync(User);
        var form = model.ToDomain(user!, formTemplate);
        await formsService.AddFormAsync(form);
        return Redirect("/");
    }
}