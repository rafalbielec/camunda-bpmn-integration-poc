using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BpmnEngine.Application.Controllers;

[Route("api")]
[AllowAnonymous]
public class ApiController : ControllerBase
{
    private readonly IProcessClient _client;
    
    public ApiController(IProcessClient client)
    {
        _client = client;
    }

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync()
    {
        var result = await _client.CountProcessDefinitionsAsync();

        return Ok(result);
    }

    [HttpGet("start")]
    public async Task<IActionResult> StartAsync()
    {
        var variables = new Dictionary<string, Variable>
        {
            [ServicesConstants.FormHandlingVariables.LastStep] = Variable.String(ServicesConstants.FormHandlingVariables.Start)
        };

        var businessKey = Guid.NewGuid().ToString("N");
        var result = await _client.StartProcessAsync(
            ServicesConstants.ProcessBpmnDiagrams.Test,
            businessKey, 
            variables);

        return Ok(result);
    }
}