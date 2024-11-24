using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Client.Requests;
using BpmnEngine.Camunda.Extensions;
using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Client;

public class ExternalTaskClient : BaseClient, IExternalTaskClient
{
    public ExternalTaskClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<ExternalTask>> FetchAndLockAsync(
        FetchAndLockRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Guard.NotNull(request, nameof(request));

        using var response = await SendExternalTaskPostRequestAsync("/fetchAndLock", request, cancellationToken);
        await EnsureSuccessAsync(response);

        var externalTasks = await response.ReadJsonAsync<List<ExternalTask>>();
        return externalTasks;
    }

    public async Task CompleteAsync(
        string taskId, CompleteRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Guard.NotNull(taskId, nameof(taskId));
        Guard.NotNull(request, nameof(request));

        using var response = await SendExternalTaskPostRequestAsync($"/{taskId}/complete", request, cancellationToken);
        await EnsureSuccessAsync(response);
    }

    public async Task ReportFailureAsync(
        string taskId, ReportFailureRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Guard.NotNull(taskId, nameof(taskId));
        Guard.NotNull(request, nameof(request));

        using var response = await SendExternalTaskPostRequestAsync($"/{taskId}/failure", request, cancellationToken);
        await EnsureSuccessAsync(response);
    }

    public async Task ReportBpmnErrorAsync(
        string taskId, BpmnErrorRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Guard.NotNull(taskId, nameof(taskId));
        Guard.NotNull(request, nameof(request));

        using var response = await SendExternalTaskPostRequestAsync($"/{taskId}/bpmnError", request, cancellationToken);
        await EnsureSuccessAsync(response);
    }

    public async Task ExtendLockAsync(
        string taskId, ExtendLockRequest request,
        CancellationToken cancellationToken = default
    )
    {
        Guard.NotNull(taskId, nameof(taskId));
        Guard.NotNull(request, nameof(request));

        using var response = await SendExternalTaskPostRequestAsync($"/{taskId}/extendLock", request, cancellationToken);
        await EnsureSuccessAsync(response);
    }
}
