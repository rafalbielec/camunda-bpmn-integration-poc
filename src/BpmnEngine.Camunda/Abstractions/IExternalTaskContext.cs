using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Abstractions;

public interface IExternalTaskContext
{
    ExternalTask Task { get; }

    IExternalTaskClient Client { get; }

    IServiceProvider ServiceProvider { get; }

    CancellationToken ProcessingAborted { get; }
}
