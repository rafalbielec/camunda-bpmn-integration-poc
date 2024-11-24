using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using BpmnEngine.Camunda.Results;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Models;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics(ServicesConstants.Topics.Accepted, LockDuration = ServicesConstants.DefaultLockDuration)]
[HandlerVariables(AllVariables = true)]
public class InformSenderAcceptedHandler : BaseHandler<InformSenderAcceptedHandler>, IExternalTaskHandler
{
    private readonly INotificationService _service;

    public InformSenderAcceptedHandler(INotificationService service, ILogger<InformSenderAcceptedHandler> logger)
        : base(service, logger)
    {
        _service = service;
    }

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        var context = new ExternalTaskContext(externalTask);

        Logger.LogInformation($"{context} has started");

        _service.InformSenderAccepted(context.BusinessKey);

        await Task.Delay(1, cancellationToken);

        Logger.LogInformation($"Wniosek {context.BusinessKey} został zaakceptowany");

        context.UpdateLastStep();
        return new CompleteResult
        {
            Variables = context.Variables
        };
    }
}