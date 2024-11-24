using BpmnEngine.Storage.Entities;

namespace BpmnEngine.Storage.Abstractions;

public interface IFormsRepository
{
    Task<IEnumerable<RequestFormEntity>> SelectFormsAsync();
    Task<RequestFormEntity> SelectFormByProcessNameAsync(StorageConstants.ProcessName processName);
    Task<ExecutedProcessEntity> SelectedExecutedProcessById(Guid id);
    Task<bool> ExecutedProcessById(Guid id);
    Task<int> InsertExecutedProcessAsync(
        Guid id,
        string definitionId, 
        string businessKey, 
        StorageConstants.ProcessName processName,
        string jsonVariables);
}