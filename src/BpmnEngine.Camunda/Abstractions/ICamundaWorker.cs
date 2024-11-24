namespace BpmnEngine.Camunda.Abstractions;

public interface ICamundaWorker
{
    Task RunAsync(CancellationToken cancellationToken);
}
