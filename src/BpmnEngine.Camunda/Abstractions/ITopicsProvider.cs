using BpmnEngine.Camunda.Client.Requests;

namespace BpmnEngine.Camunda.Abstractions;

public interface ITopicsProvider
{
    IReadOnlyCollection<FetchAndLockRequest.Topic> GetTopics();
}
