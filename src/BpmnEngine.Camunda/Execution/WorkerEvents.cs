using System.Diagnostics.CodeAnalysis;

namespace BpmnEngine.Camunda.Execution;

[ExcludeFromCodeCoverage]
public class WorkerEvents
{
    public Func<IServiceProvider, CancellationToken, Task> OnBeforeFetchAndLock { get; } = DefaultOnBeforeFetchAndLock;
    public Func<IServiceProvider, CancellationToken, Task> OnFailedFetchAndLock { get; } = DefaultOnFailedFetchAndLock;
    public Func<IServiceProvider, CancellationToken, Task> OnAfterProcessingAllTasks { get; } = DefaultOnAfterProcessingAllTasks;

    private static Task DefaultOnBeforeFetchAndLock(IServiceProvider provider, CancellationToken ct) => Task.CompletedTask;
    private static Task DefaultOnFailedFetchAndLock(IServiceProvider provider, CancellationToken ct) => Task.Delay(10_000, ct);
    private static Task DefaultOnAfterProcessingAllTasks(IServiceProvider provider, CancellationToken ct) => Task.CompletedTask;
}