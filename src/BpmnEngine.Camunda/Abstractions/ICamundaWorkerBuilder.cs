using BpmnEngine.Camunda.Execution;
using BpmnEngine.Camunda.External;
using Microsoft.Extensions.DependencyInjection;

namespace BpmnEngine.Camunda.Abstractions;

public interface ICamundaWorkerBuilder
{
    IServiceCollection Services { get; }

    string WorkerId { get; }

    ICamundaWorkerBuilder AddEndpointProvider<TProvider>() where TProvider : class, IEndpointProvider;

    ICamundaWorkerBuilder AddTopicsProvider<TProvider>() where TProvider : class, ITopicsProvider;

    ICamundaWorkerBuilder AddHandler(ExternalTaskDelegate handler, HandlerMetadata handlerMetadata);

    ICamundaWorkerBuilder ConfigurePipeline(Action<IPipelineBuilder> configureAction);

    ICamundaWorkerBuilder ConfigureEvents(Action<WorkerEvents> configureAction);
}
