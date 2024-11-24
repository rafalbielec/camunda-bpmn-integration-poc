using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Client.Requests;

namespace BpmnEngine.Camunda.Execution;

public sealed class StaticTopicsProvider : ITopicsProvider
{
    private readonly IReadOnlyList<FetchAndLockRequest.Topic> _topics;

    public StaticTopicsProvider(IEnumerable<HandlerDescriptor> handlerDescriptors)
    {
        _topics = handlerDescriptors.SelectMany(ConvertDescriptorToTopic).ToList();
    }

    public IReadOnlyCollection<FetchAndLockRequest.Topic> GetTopics() => _topics;
    private static IEnumerable<FetchAndLockRequest.Topic> ConvertDescriptorToTopic(HandlerDescriptor descriptor)
    {
        return descriptor.Metadata.TopicNames
            .Select(topicName => MapToTopicRequest(descriptor.Metadata, topicName));
    }

    private static FetchAndLockRequest.Topic MapToTopicRequest(HandlerMetadata metadata, string topicName) =>
        new(topicName, metadata.LockDuration)
        {
            LocalVariables = metadata.LocalVariables,
            Variables = metadata.Variables,
            ProcessDefinitionIdIn = metadata.ProcessDefinitionIds,
            ProcessDefinitionKeyIn = metadata.ProcessDefinitionKeys,
            ProcessVariables = metadata.ProcessVariables,
            TenantIdIn = metadata.TenantIds,
            DeserializeValues = metadata.DeserializeValues,
            IncludeExtensionProperties = metadata.IncludeExtensionProperties
        };
}