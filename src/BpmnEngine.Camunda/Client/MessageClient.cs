using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Client.Requests;
using BpmnEngine.Camunda.Client.Responses;
using BpmnEngine.Camunda.Extensions;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Camunda.Client;

public class MessageClient : BaseClient, IMessageClient
{
    private readonly ILogger<MessageClient> _logger;

    public MessageClient(HttpClient httpClient, ILogger<MessageClient> logger) : base(httpClient)
    {
        _logger = logger;
    }

    public async Task<MessageResponse[]> SendMessageEventAsync(string businessKey, string message,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Sending message {message} to {businessKey}");

        var request = new MessageRequest(businessKey, message);

        using var response = await SendMessageAsync(request, cancellationToken);
        await EnsureSuccessAsync(response);

        var result = await response.ReadJsonAsync<MessageResponse[]>();
        return result;
    }
}