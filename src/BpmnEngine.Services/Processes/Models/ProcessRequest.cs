using BpmnEngine.Camunda.External;
using BpmnEngine.Storage;

namespace BpmnEngine.Services.Processes.Models;

public record ProcessRequest(
    StorageConstants.ProcessName ProcessName,
    Dictionary<string, Variable> ProcessVariables);

public record ExecutedProcess(Guid ProcessInstanceId, string BusinessKey, Dictionary<string, string> FormValues);
public record UserAction(Guid Id, Guid ProcessInstanceId, string TopicName);
