using BpmnEngine.Application.Models;
using BpmnEngine.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BpmnEngine.Application.Controllers;

[Route("processes")]
[AllowAnonymous]
public class ProcessController : Controller
{
    private readonly IDecisionService _decisionService;

    public ProcessController(IDecisionService decisionService)
    {
        _decisionService = decisionService;
    }

    [HttpGet("decision/{id:guid}")]
    public async Task<IActionResult> Decision(Guid id)
    {
        var userAction = await _decisionService.GetUserActionByIdAsync(id);

        var (processId, businessKey, formValues) = 
            await _decisionService.GetExecutedProcessByIdAsync(userAction.ProcessInstanceId);

        var model = new DecisionViewModel
        {
            Id = userAction.Id,
            TopicName = userAction.TopicName,
            ProcessInstanceId = processId,
            BusinessKey = businessKey,
            Variables = formValues
        };

        return View(model);
    }

    [HttpPost("reject")]
    public async Task<IActionResult> Reject(DecisionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Decision), new {id = model.Id});
        }

        await _decisionService.RejectMessageAsync(model.BusinessKey, model.TopicName);

        return View("DecisionMade", model);
    }
    
    [HttpPost("accept")]
    public async Task<IActionResult> Accept(DecisionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Decision), new { id = model.Id });
        }

        await _decisionService.AcceptMessageAsync(model.BusinessKey, model.TopicName);

        return View("DecisionMade", model);
    }
}