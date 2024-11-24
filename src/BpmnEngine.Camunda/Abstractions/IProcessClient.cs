using BpmnEngine.Camunda.Client.Responses;
using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Abstractions;

public interface IProcessClient
{
    Task<ProcessCountResponse> CountProcessDefinitionsAsync(CancellationToken cancellationToken = default);
    Task<ProcessStartResponse> StartProcessAsync(string processKey, string businessKey,
        Dictionary<string, Variable> variables,
        CancellationToken cancellationToken = default);
}