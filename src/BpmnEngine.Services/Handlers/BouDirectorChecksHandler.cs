using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics(ServicesConstants.Topics.BouDirectorChecks, LockDuration = ServicesConstants.DefaultLockDuration)]
[HandlerVariables(AllVariables = true)]
public class BouDirectorChecksHandler : BaseHandler<BouDirectorChecksHandler>, IExternalTaskHandler
{
    public BouDirectorChecksHandler(INotificationService service, ILogger<BouDirectorChecksHandler> logger)
        : base(service, logger)
    {
    }

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        return await BaseHandleAsync(externalTask, cancellationToken);
    }
}