using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Abstractions;

public interface IExternalTaskHandler
{
    Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken);
}
