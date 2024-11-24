using BpmnEngine.Storage.Abstractions;
using BpmnEngine.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace BpmnEngine.Storage;

public class FormsRepository : IFormsRepository
{
    private readonly string _connectionString;

    public FormsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<RequestFormEntity>> SelectFormsAsync()
    {
        await using var context = new StorageContext(_connectionString);

        return await context.RequestForms.AsNoTracking().OrderBy(a => a.Name).ToArrayAsync();
    }

    public async Task<RequestFormEntity> SelectFormByProcessNameAsync(StorageConstants.ProcessName processName)
    {
        await using var context = new StorageContext(_connectionString);

        return await context.RequestForms.AsNoTracking().FirstAsync(a => a.ProcessName == processName);
    }

    public async Task<ExecutedProcessEntity> SelectedExecutedProcessById(Guid id)
    {
        await using var context = new StorageContext(_connectionString);

        return await context.ExecutedProcess.AsNoTracking().FirstAsync(a => a.Id == id);
    }

    public async Task<bool> ExecutedProcessById(Guid id)
    {
        await using var context = new StorageContext(_connectionString);

        return await context.ExecutedProcess.AsNoTracking().AnyAsync(a => a.Id == id);
    }

    public async Task<int> InsertExecutedProcessAsync(Guid id, 
        string definitionId, 
        string businessKey, 
        StorageConstants.ProcessName processName,
        string jsonVariables)
    {
        await using var context = new StorageContext(_connectionString);

        var requestedFormId = await context
            .RequestForms
            .AsNoTracking()
            .Where(a => a.ProcessName == processName)
            .Select(a => a.Id)
            .FirstAsync();

        var entity = new ExecutedProcessEntity
        {
            Id = id,
            DefinitionId = definitionId,
            BusinessKey = businessKey,
            ProcessName = processName,
            Created = DateTime.UtcNow,
            RequestFormEntityId = requestedFormId,
            Variables = jsonVariables
        };

        await context.ExecutedProcess.AddAsync(entity);

        return await context.SaveChangesAsync();
    }
}