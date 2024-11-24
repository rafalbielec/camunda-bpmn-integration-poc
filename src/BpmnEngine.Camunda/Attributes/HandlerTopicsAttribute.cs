namespace BpmnEngine.Camunda.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class HandlerTopicsAttribute : Attribute
{
    private int _lockDuration = CamundaConstants.MinimumLockDuration;

    public HandlerTopicsAttribute(params string[] topicNames)
    {
        Guard.NotNull(topicNames, nameof(topicNames));

        TopicNames = topicNames.ToList();
    }

    public IReadOnlyList<string> TopicNames { get; }

    public int LockDuration
    {
        get => _lockDuration;
        set => _lockDuration = Guard.GreaterThanOrEqual(value, CamundaConstants.MinimumLockDuration, nameof(LockDuration));
    }

    public bool IncludeExtensionProperties { get; set; }
}