using CourseProject.Application.Mappers;
using CourseProject.Application.ViewModels;
using CourseProject.Domain.Abstractions.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormTemplatesApiController(IFormTemplatesService formTemplatesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormTemplateViewModel>>> GetAllFormTemplates()
    {
        var formTemplates = await formTemplatesService.GetAllFormTemplates();
        var result = formTemplates.Select(ft => ft.ToViewModel());
        return Ok(result);
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
