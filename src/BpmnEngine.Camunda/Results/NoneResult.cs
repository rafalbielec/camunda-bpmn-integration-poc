using BpmnEngine.Camunda.Abstractions;

namespace BpmnEngine.Camunda.Results;

public class NoneResult : IExecutionResult
{
    public Task ExecuteResultAsync(IExternalTaskContext context) => Task.CompletedTask;
}
