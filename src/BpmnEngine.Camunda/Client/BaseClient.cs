using System.Diagnostics.CodeAnalysis;
using BpmnEngine.Camunda.Client.Responses;
using BpmnEngine.Camunda.Exceptions;
using BpmnEngine.Camunda.Extensions;

namespace BpmnEngine.Camunda.Client;

public abstract class BaseClient
{
    protected BaseClient(HttpClient httpClient)
    {
        HttpClientInstance = Guard.NotNull(httpClient, nameof(httpClient));
        ValidateHttpClient(httpClient);
    }

    private HttpClient HttpClientInstance { get; }

    [ExcludeFromCodeCoverage]
    private static void ValidateHttpClient(HttpClient httpClient)
    {
        if (httpClient.BaseAddress == null)
            throw new ArgumentException("BaseAddress must be configured", nameof(httpClient));
    }

    protected async Task<HttpResponseMessage> SendExternalTaskPostRequestAsync<T>(string path, T body,
        CancellationToken cancellationToken)
        where T : notnull
    {
        var basePath = GetBasePath();
        var requestPath = $"{basePath}/external-task/{path.TrimStart('/')}";
        var response = await HttpClientInstance.PostJsonAsync(requestPath, body, cancellationToken);
        return response;
    }

    protected async Task<HttpResponseMessage> SendGetProcessDefinitionAsync(string endpoint, 
        CancellationToken cancellationToken)
    {
        var basePath = GetBasePath();
        var requestPath = $"{basePath}/process-definition/{endpoint.TrimStart('/')}";
        var response = await HttpClientInstance.GetAsync(requestPath, cancellationToken);
        return response;
    }
    
    protected async Task<HttpResponseMessage> SendPostProcessDefinitionAsync<T>(string endpoint, T body,
        CancellationToken cancellationToken) where T : notnull
    {
        var basePath = GetBasePath();
        var requestPath = $"{basePath}/process-definition/{endpoint.TrimStart('/')}";
        var response = await HttpClientInstance.PostJsonAsync(requestPath, body, cancellationToken);
        return response;
    }
    
    protected async Task<HttpResponseMessage> SendMessageAsync<T>(T body, CancellationToken cancellationToken) where T : notnull
    {
        var basePath = GetBasePath();
        var requestPath = $"{basePath}/message";
        var response = await HttpClientInstance.PostJsonAsync(requestPath, body, cancellationToken);
        return response;
    }

    private string GetBasePath()
    {
        return HttpClientInstance.BaseAddress?.AbsolutePath.TrimEnd('/') ?? string.Empty;
    }

    protected static async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode && response.IsJson())
        {
            var errorResponse = await response.ReadJsonAsync<ErrorResponse>();
            response.Content.Dispose();
            throw new ClientException(errorResponse, response.StatusCode);
        }

        response.EnsureSuccessStatusCode();
    }
}