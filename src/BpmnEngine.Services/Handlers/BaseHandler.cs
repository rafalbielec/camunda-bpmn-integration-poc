using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.External;
using BpmnEngine.Camunda.Results;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Models;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

public abstract class BaseHandler<T>
{
    private readonly INotificationService _notificationService;
    protected readonly ILogger<T> Logger;

    protected BaseHandler(INotificationService notificationService, ILogger<T> logger)
    {
        _notificationService = notificationService;
        Logger = logger;
    }

    protected async Task<IExecutionResult> BaseHandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        var context = new ExternalTaskContext(externalTask);

        Logger.LogInformation($"{context} has started for {context.ProcessInstanceId:N}");

        const int delay = 100;
        for (var i = 0; i < ServicesConstants.DefaultLockDuration/delay; i++)
        {
            if (!await _notificationService.ConfirmSavedProcessId(context.ProcessInstanceId))
                await Task.Delay(delay, cancellationToken);
        }

        var saved = await _notificationService.SendNotificationAsync(
            context.TaskId, 
            context.ProcessInstanceId, 
            context.TopicName, 
            context.UserName,
            cancellationToken);

        if (saved)
        {
            Logger.LogInformation($"User action {context.TaskId} has been saved for '{context.TopicName}'");
        }

        Logger.LogInformation($"External Service Task for '{context.TopicName}' in {context.BusinessKey} has ended");

        context.UpdateLastStep();
        return new CompleteResult
        {
            Variables = context.Variables
        };
    }
}