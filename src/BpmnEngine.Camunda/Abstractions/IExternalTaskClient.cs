using BpmnEngine.Camunda.Client.Requests;
using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Abstractions;

public interface IExternalTaskClient
{
    Task<List<ExternalTask>> FetchAndLockAsync(
        FetchAndLockRequest request,
        CancellationToken cancellationToken = default
    );

    Task CompleteAsync(
        string taskId, CompleteRequest request,
        CancellationToken cancellationToken = default
    );

    Task ReportFailureAsync(
        string taskId, ReportFailureRequest request,
        CancellationToken cancellationToken = default
    );

    Task ReportBpmnErrorAsync(
        string taskId, BpmnErrorRequest request,
        CancellationToken cancellationToken = default
    );

    Task ExtendLockAsync(
        string taskId, ExtendLockRequest request,
        CancellationToken cancellationToken = default
    );
}
