using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Abstractions;

public interface IEndpointProvider
{
    ExternalTaskDelegate GetEndpointDelegate(ExternalTask externalTask);
}
