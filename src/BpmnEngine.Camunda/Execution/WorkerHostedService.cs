using BpmnEngine.Camunda.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BpmnEngine.Camunda.Execution;

public sealed class WorkerHostedService : BackgroundService
{
    private readonly int _numberOfWorkers;
    private readonly IServiceProvider _serviceProvider;

    public WorkerHostedService(IServiceProvider serviceProvider, int numberOfWorkers)
    {
        _serviceProvider = Guard.NotNull(serviceProvider, nameof(serviceProvider));
        _numberOfWorkers = Guard.GreaterThanOrEqual(numberOfWorkers,
            CamundaConstants.MinimumParallelExecutors,
            nameof(numberOfWorkers));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var activeTasks = Enumerable.Range(0, _numberOfWorkers)
            .Select(_ => _serviceProvider.GetRequiredService<ICamundaWorker>())
            .Select(worker => worker.RunAsync(stoppingToken))
            .ToList();

        return Task.WhenAll(activeTasks);
    }
}