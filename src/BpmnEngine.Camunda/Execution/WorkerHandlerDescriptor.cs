using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Execution;

public class WorkerHandlerDescriptor
{
    public WorkerHandlerDescriptor(ExternalTaskDelegate externalTaskDelegate)
    {
        ExternalTaskDelegate = Guard.NotNull(externalTaskDelegate, nameof(externalTaskDelegate));
    }

    public ExternalTaskDelegate ExternalTaskDelegate { get; }
}
