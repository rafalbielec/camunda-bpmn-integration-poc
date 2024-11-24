using BpmnEngine.Camunda.Abstractions;

namespace BpmnEngine.Camunda.External;

public delegate Task ExternalTaskDelegate(IExternalTaskContext context);
