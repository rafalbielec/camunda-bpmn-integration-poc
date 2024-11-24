using System.Net;
using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Client.Requests;
using BpmnEngine.Camunda.Exceptions;
using BpmnEngine.Camunda.External;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Camunda.Results;

public sealed class CompleteResult : IExecutionResult
{
    public IDictionary<string, Variable>? Variables { get; set; }
    public IDictionary<string, Variable>? LocalVariables { get; set; }

    public async Task ExecuteResultAsync(IExternalTaskContext context)
    {
        var externalTask = context.Task;
        var client = context.Client;

        try
        {
            await client.CompleteAsync(externalTask.Id, new CompleteRequest(externalTask.WorkerId)
            {
                Variables = Variables,
                LocalVariables = LocalVariables
            });
        }
        catch (ClientException e) when (e.StatusCode == HttpStatusCode.InternalServerError)
        {
            var logger = context.ServiceProvider.GetService<ILogger<CompleteResult>>();
            logger?.LogWarning(e, "Failed to complete task {TaskId}. Reason: {Reason}",
                externalTask.Id, e.Message
            );
            await client.ReportFailureAsync(externalTask.Id, new ReportFailureRequest(externalTask.WorkerId)
            {
                ErrorMessage = e.ErrorType,
                ErrorDetails = e.ErrorMessage
            });
        }
    }
}