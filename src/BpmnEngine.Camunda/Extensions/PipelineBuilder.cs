using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Extensions;

public class PipelineBuilder : IPipelineBuilder
{
    private readonly List<Func<ExternalTaskDelegate, ExternalTaskDelegate>> _middlewareList = new();

    public PipelineBuilder(IServiceProvider serviceProvider)
    {
        ApplicationServices = serviceProvider;
    }

    public IServiceProvider ApplicationServices { get; }

    public IPipelineBuilder Use(Func<ExternalTaskDelegate, ExternalTaskDelegate> middleware)
    {
        _middlewareList.Add(middleware);
        return this;
    }

    public ExternalTaskDelegate Build(ExternalTaskDelegate lastDelegate)
    {
        Guard.NotNull(lastDelegate, nameof(lastDelegate));

        return _middlewareList.AsEnumerable()
            .Reverse()
            .Aggregate(lastDelegate, (current, middleware) => middleware(current));
    }
}