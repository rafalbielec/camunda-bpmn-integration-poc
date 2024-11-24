using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics(ServicesConstants.Topics.DirectorChecks, LockDuration = ServicesConstants.DefaultLockDuration)]
[HandlerVariables(AllVariables = true)]
public class DirectorChecksHandler : BaseHandler<DirectorChecksHandler>, IExternalTaskHandler
{
    public DirectorChecksHandler(INotificationService service, ILogger<DirectorChecksHandler> logger)
        : base(service, logger)
    {
    }

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        return await BaseHandleAsync(externalTask, cancellationToken);
    }
}