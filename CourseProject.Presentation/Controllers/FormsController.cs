using CourseProject.Domain.Abstractions.IServices;
using CourseProject.Domain.Models;
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
        if (forms == null || !forms.Any())
        {
            return NotFound("Формы по указанному шаблону не найдены.");
        }
        
        foreach (var form in forms)
        {
            var questions = await questionService.GetQuestionByFormTemplateIdAsync(form.FormTemplateId);
            form.FormTemplate.Questions = questions;
        }
        
        /*var viewModel = new FormViewModel
        {
            FormTemplateId = templateId,
            Forms = forms.Select(f => new FormViewModel
            {
                Id = f.Id,
                ApplicationUserName = f.ApplicationUser.UserName,
                SubmissionDate = f.SubmissionDate,
                Answers = f.Answers.Select(a => new FormAnswerViewModel
                {
                    QuestionId = a.QuestionId,
                    AnswerText = a.AnswerText,
                    AnswerInteger = a.AnswerInteger,
                    AnswerCheckbox = a.AnswerCheckbox
                }).ToList()
            }).ToList()
        };*/

        return View(forms);
    }
    
    [Authorize]
    [HttpGet("Create/{id}")]
    public async Task<IActionResult> Create(Guid id)
    {
        // Получение шаблона формы по ID
        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(id);
        if (formTemplate == null)
        {
            return NotFound();
        }

        var viewModel = new FormViewModel
        {
            FormTemplateId = formTemplate.Id,
            Questions = formTemplate.Questions.Select(q => new FormQuestionViewModel
            {
                QuestionId = q.Id,
                Title = q.Title,
                Description = q.Description,
                Type = q.Type
            }).ToList()
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost("Create/{id}")]
    public async Task<IActionResult> Create(FormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var formTemplate = await formTemplatesService.GetFormTemplateByIdAsync(model.FormTemplateId);
        if (formTemplate == null)
        {
            return NotFound("Форма с указанным ID не найдена.");
        }

        var user = await userManager.GetUserAsync(User);
        
        var questionIds = model.Questions.Select(q => q.QuestionId).ToList();
        var existingQuestions = await questionService.GetQuestionsByIdsAsync(questionIds);
        /*if (existingQuestions.Count != questionIds.Count)
        {
            return BadRequest("Некоторые вопросы не найдены.");
        }*/

        var form = new Form
        {
            FormTemplateId = model.FormTemplateId,
            ApplicationUserId = user.Id,
            ApplicationUser = user,
            FormTemplate = formTemplate,
            SubmissionDate = DateTime.UtcNow,
            Answers = model.Questions.Select(q => new FormAnswer
            {
                QuestionId = q.QuestionId,
                AnswerText = q.AnswerText,
                AnswerInteger = q.AnswerInteger,
                AnswerCheckbox = q.AnswerCheckbox,
            }).ToList()
        };

        await formsService.AddFormAsync(form);

        return Redirect("/");
    }
}