using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Execution;
using BpmnEngine.Camunda.External;
using Microsoft.Extensions.DependencyInjection;

namespace BpmnEngine.Camunda.Extensions;

public class CamundaWorkerBuilder : ICamundaWorkerBuilder
{
    public CamundaWorkerBuilder(IServiceCollection services, string workerId)
    {
        Services = services;
        WorkerId = workerId;
    }

    public IServiceCollection Services { get; }
    public string WorkerId { get; }

    public ICamundaWorkerBuilder AddEndpointProvider<TProvider>()
        where TProvider : class, IEndpointProvider
    {
        Services.AddSingleton<IEndpointProvider, TProvider>();
        return this;
    }

    public ICamundaWorkerBuilder AddTopicsProvider<TProvider>() where TProvider : class, ITopicsProvider
    {
        Services.AddTransient<ITopicsProvider, TProvider>();
        return this;
    }

    public ICamundaWorkerBuilder AddHandler(ExternalTaskDelegate handler, HandlerMetadata handlerMetadata)
    {
        Guard.NotNull(handler, nameof(handler));
        Guard.NotNull(handlerMetadata, nameof(handlerMetadata));

        var descriptor = new HandlerDescriptor(handler, handlerMetadata);

        Services.AddSingleton(descriptor);
        return this;
    }

    public ICamundaWorkerBuilder ConfigurePipeline(Action<IPipelineBuilder> configureAction)
    {
        Guard.NotNull(configureAction, nameof(configureAction));
        Services.AddSingleton(provider =>
        {
            var externalTaskDelegate = new PipelineBuilder(provider)
                .Also(configureAction)
                .Build(ExternalTaskRouter.RouteAsync);
            return new WorkerHandlerDescriptor(externalTaskDelegate);
        });
        return this;
    }

    public ICamundaWorkerBuilder ConfigureEvents(Action<WorkerEvents> configureAction)
    {
        Services.Configure(configureAction);
        return this;
    }

    internal CamundaWorkerBuilder AddFetchAndLockRequestProvider(
        Func<string, IServiceProvider, IFetchAndLockRequestProvider> factory
    )
    {
        Services.AddSingleton(provider => factory(WorkerId, provider));

        return this;
    }
}