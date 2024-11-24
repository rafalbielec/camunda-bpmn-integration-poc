namespace BpmnEngine.Camunda;

public sealed class CamundaConstants
{
    internal const int MinimumLockDuration = 5_000;
    internal const int MinimumParallelExecutors = 1;

    public const string WorkerName = nameof(BpmnEngine);
    public const string EngineRestAddress = $"{nameof(Camunda)}:{nameof(EngineRestAddress)}";
}
