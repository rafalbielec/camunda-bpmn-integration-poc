using BpmnEngine.Camunda.Client.Responses;

namespace BpmnEngine.Camunda.Abstractions;

public interface IMessageClient
{
    Task<MessageResponse[]> SendMessageEventAsync(string businessKey, string message, CancellationToken cancellationToken = default);
}