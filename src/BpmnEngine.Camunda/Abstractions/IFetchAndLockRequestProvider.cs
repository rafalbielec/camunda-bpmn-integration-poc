using BpmnEngine.Camunda.Client.Requests;

namespace BpmnEngine.Camunda.Abstractions;

public interface IFetchAndLockRequestProvider
{
    /// <summary>
    /// This method is called in the worker before each "fetch and lock" operation
    /// </summary>
    FetchAndLockRequest GetRequest();
}
