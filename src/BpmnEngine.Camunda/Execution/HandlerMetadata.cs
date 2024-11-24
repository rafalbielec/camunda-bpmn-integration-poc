namespace BpmnEngine.Camunda.Execution;

public class HandlerMetadata
{
    public HandlerMetadata(IReadOnlyList<string> topicNames, int lockDuration = CamundaConstants.MinimumLockDuration)
    {
        TopicNames = Guard.NotNull(topicNames, nameof(topicNames));
        LockDuration = Guard.GreaterThanOrEqual(lockDuration, CamundaConstants.MinimumLockDuration, nameof(lockDuration));
    }

    public IReadOnlyList<string> TopicNames { get; }
    public int LockDuration { get; }
    public bool LocalVariables { get; set; }
    public bool DeserializeValues { get; set; }
    public bool IncludeExtensionProperties { get; set; }
    public IReadOnlyList<string>? Variables { get; set; }
    public IReadOnlyList<string>? ProcessDefinitionIds { get; set; }
    public IReadOnlyList<string>? ProcessDefinitionKeys { get; set; }
    public IReadOnlyDictionary<string, string>? ProcessVariables { get; set; }
    public IReadOnlyList<string>? TenantIds { get; set; }
}
