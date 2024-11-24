namespace BpmnEngine.Services.Abstractions;

public interface INotificationService
{
    Task<bool> ConfirmSavedProcessId(Guid id);
    Task<bool> SendNotificationAsync(Guid actionId, Guid processId, 
        string topicName,
        string userName,
        CancellationToken cancellationToken);

    bool InformSenderAccepted(string businessKey);
    bool InformSenderRejected(string businessKey);
}