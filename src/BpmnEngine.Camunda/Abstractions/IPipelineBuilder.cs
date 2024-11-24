using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Abstractions;

public interface IPipelineBuilder
{
    IServiceProvider ApplicationServices { get; }

    IPipelineBuilder Use(Func<ExternalTaskDelegate, ExternalTaskDelegate> middleware);

    ExternalTaskDelegate Build(ExternalTaskDelegate lastDelegate);
}
