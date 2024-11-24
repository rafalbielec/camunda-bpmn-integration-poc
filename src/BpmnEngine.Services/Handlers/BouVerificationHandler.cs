using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics(ServicesConstants.Topics.Verification, LockDuration = ServicesConstants.DefaultLockDuration)]
[HandlerVariables(AllVariables = true)]
public class BouVerificationHandler : BaseHandler<BouVerificationHandler>, IExternalTaskHandler
{
    public BouVerificationHandler(INotificationService service, ILogger<BouVerificationHandler> logger)
        : base(service, logger)
    {
    }

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        return await BaseHandleAsync(externalTask, cancellationToken);
    }
}