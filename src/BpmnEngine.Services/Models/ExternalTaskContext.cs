using BpmnEngine.Camunda;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services.Processes.Errors;

namespace BpmnEngine.Services.Models;

public class ExternalTaskContext
{
    public ExternalTaskContext(ExternalTask externalTask)
    {
        TaskId = Guid.NewGuid();

        var processInstanceId = Guard.NotEmptyAndNotNull(externalTask.ProcessInstanceId, nameof(externalTask.ProcessInstanceId));

        BusinessKey = Guard.NotEmptyAndNotNull(externalTask.BusinessKey, nameof(externalTask.BusinessKey));
        TopicName = Guard.NotEmptyAndNotNull(externalTask.TopicName, nameof(externalTask.TopicName));
        Variables = externalTask.Variables ?? new Dictionary<string, Variable>();

        ProcessInstanceId = new Guid(processInstanceId);

        if (externalTask.Variables == null)
            throw new MissingVariablesException();

        if (externalTask.Variables.TryGetValue(ServicesConstants.FormHandlingVariables.LastStep, out var step))
            LastStep = step.AsString();
        else
            throw new MissingVariableException(ServicesConstants.FormHandlingVariables.LastStep);

        if (externalTask.Variables.TryGetValue(ServicesConstants.FormHandlingVariables.Position, out var position))
            UserJobPosition = position.AsString();
        else
            throw new MissingVariableException(ServicesConstants.FormHandlingVariables.Position);
        
        if (externalTask.Variables.TryGetValue(ServicesConstants.FormHandlingVariables.UserName, out var user))
            UserName = user.AsString();
        else
            throw new MissingVariableException(ServicesConstants.FormHandlingVariables.UserName);
    }

    public IDictionary<string, Variable> Variables { get; set; }

    public string TopicName { get; set; }
    public Guid TaskId { get; }
    public Guid ProcessInstanceId { get; }
    public string BusinessKey { get; }
    public string LastStep { get; }

    public string UserJobPosition { get; }
    public string UserName { get; }

    public void UpdateLastStep()
    {
        var value = TopicName;

        if (Variables.ContainsKey(ServicesConstants.FormHandlingVariables.LastStep))
            Variables[ServicesConstants.FormHandlingVariables.LastStep] = Variable.String(value);
        else
            Variables.Add(ServicesConstants.FormHandlingVariables.LastStep, Variable.String(value));
    }

    public override string ToString() => $"External Service Task {TaskId:N} for '{TopicName}' in {BusinessKey} / {ProcessInstanceId:N}";
}