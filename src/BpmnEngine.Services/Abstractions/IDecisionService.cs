using BpmnEngine.Services.Processes.Models;

namespace BpmnEngine.Services.Abstractions;

public interface IDecisionService
{
    Task<ExecutedProcess> GetExecutedProcessByIdAsync(Guid id);
    Task<UserAction> GetUserActionByIdAsync(Guid id);
    Task<int> AcceptMessageAsync(string businessKey, string topicName);
    Task<int> RejectMessageAsync(string businessKey, string topicName);
}