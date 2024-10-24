using CourseProject.Application.Mappers;
using CourseProject.Application.ViewModels;
using CourseProject.Domain.Abstractions.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

[Route("[controller]")]
public class FormTemplatesController(IFormTemplatesService formTemplatesService) : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormTemplateViewModel>>> Index()
    {
        var formTemplates = await formTemplatesService.GetAllFormTemplates();
        var result = formTemplates.Select(ft => ft.ToViewModel());
        return View(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<FormTemplateViewModel>> GetFormTemplateById(Guid id)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateById(id);
        if (formTemplate is null)
            return NotFound();
        return Ok(formTemplate.ToViewModel());
    }
    
    [HttpPost]
    public async Task<ActionResult<FormTemplateViewModel>> CreateFormTemplate([FromBody] FormTemplateViewModel viewModel)
    {
        var formTemplate = viewModel.ToModel();
        await formTemplatesService.CreateFormTemplate(formTemplate);
        return CreatedAtAction(nameof(GetFormTemplateById), new { id = formTemplate.Id }, formTemplate.ToViewModel());
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFormTemplate(Guid id, [FromBody] FormTemplateViewModel viewModel)
    {
        var formTemplate = await formTemplatesService.GetFormTemplateById(id);
        if (formTemplate is null)
            return NotFound();
        var updatedFormTemplate = viewModel.ToModel();
        await formTemplatesService.UpdateFormTemplate(updatedFormTemplate);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFormTemplate(Guid id)
    {
        await formTemplatesService.DeleteFormTemplate(id);
        return NoContent();
    }
}