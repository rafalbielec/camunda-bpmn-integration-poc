using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Client.Requests;
using BpmnEngine.Camunda.Client.Responses;
using BpmnEngine.Camunda.Extensions;
using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Client;

public class ProcessClient : BaseClient, IProcessClient
{
    public ProcessClient(HttpClient httpClient):base(httpClient)
    {
    }
    
    public async Task<ProcessCountResponse> CountProcessDefinitionsAsync(CancellationToken cancellationToken = default)
    {
        using var response = await SendGetProcessDefinitionAsync("count", cancellationToken);
        await EnsureSuccessAsync(response);

        var result = await response.ReadJsonAsync<ProcessCountResponse>();
        return result;
    }

    public async Task<ProcessStartResponse> StartProcessAsync(string processKey, string businessKey,
        Dictionary<string, Variable> variables,
        CancellationToken cancellationToken = default)
    {
        var request = new ProcessStartRequest(businessKey, variables);
        using var response = await SendPostProcessDefinitionAsync($"key/{processKey}/start", request, cancellationToken);
        await EnsureSuccessAsync(response);

        var result = await response.ReadJsonAsync<ProcessStartResponse>();
        return result;
    }
}