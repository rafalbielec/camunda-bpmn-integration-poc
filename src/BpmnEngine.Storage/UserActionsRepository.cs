using BpmnEngine.Storage.Abstractions;
using BpmnEngine.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace BpmnEngine.Storage;

public class UserActionsRepository : IUserActionsRepository
{
    private readonly string _connectionString;

    public UserActionsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<UserActionEntity> SelectUserActionByIdAsync(Guid id)
    {
        await using var context = new StorageContext(_connectionString);

        return await context.UserActions
            .AsNoTracking()
            .FirstAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<UserActionEntity>> SelectUserActionsAsync(Guid processId)
    {
        await using var context = new StorageContext(_connectionString);

        return await context.UserActions
            .AsNoTracking()
            .Where(a => a.ExecutedProcessId == processId)
            .OrderBy(a => a.Created)
            .ToArrayAsync();
    }

    public async Task<int> InsertUserActionAsync(Guid actionId, Guid processId, string topicName,
        CancellationToken cancellationToken)
    {
        await using var context = new StorageContext(_connectionString);

        var entity = new UserActionEntity
        {
            Id = actionId,
            Created = DateTime.UtcNow,
            TopicName = topicName,
            ExecutedProcessId = processId
        };

        try
        {
            await context.UserActions.AddAsync(entity, cancellationToken);

            return await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());

            throw;
        }
    }
}
