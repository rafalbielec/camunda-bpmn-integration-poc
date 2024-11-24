using BpmnEngine.Storage.Entities;

namespace BpmnEngine.Storage.Abstractions;

public interface IUserActionsRepository
{
    Task<UserActionEntity> SelectUserActionByIdAsync(Guid id);
    Task<IEnumerable<UserActionEntity>> SelectUserActionsAsync(Guid processId);
    Task<int> InsertUserActionAsync(Guid actionId, Guid processId, string topicName,
        CancellationToken cancellationToken);
}