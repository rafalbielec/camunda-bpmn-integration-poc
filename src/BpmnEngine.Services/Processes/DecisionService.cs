using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Extensions;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Processes.Models;
using BpmnEngine.Storage.Abstractions;

namespace BpmnEngine.Services.Processes;

public class DecisionService : IDecisionService
{
    private readonly IFormsRepository _formsRepository;
    private readonly IUserActionsRepository _userActionsRepository;
    private readonly IMessageClient _client;

    public DecisionService(IFormsRepository formsRepository, IUserActionsRepository userActionsRepository, IMessageClient client)
    {
        _formsRepository = formsRepository;
        _userActionsRepository = userActionsRepository;
        _client = client;
    }

    public async Task<ExecutedProcess> GetExecutedProcessByIdAsync(Guid id)
    {
        var process = await _formsRepository.SelectedExecutedProcessById(id);
        
        var dir = SerializerInstance.DeserializeFromString<IDictionary<string, Variable>>(process.Variables);

        var dictionary = dir.ToDictionary(a => a.Key, b => b.Value.AsStringValue());

        var model = new ExecutedProcess(process.Id, process.BusinessKey, dictionary!);

        return model;
    }

    public async Task<UserAction> GetUserActionByIdAsync(Guid id)
    {
        var action = await _userActionsRepository.SelectUserActionByIdAsync(id);

        return new UserAction(action.Id, action.ExecutedProcessId, action.TopicName);
    }

    private async Task<int> SendMessageToProcessAsync(string businessKey, string message)
    {
        var response = await _client.SendMessageEventAsync(businessKey, message);

        return response.Length;
    }

    public async Task<int> AcceptMessageAsync(string businessKey, string topicName)
    {        
        var message = InterpretTopicAccept(topicName);
        return await SendMessageToProcessAsync(businessKey, message);
    }

    public async Task<int> RejectMessageAsync(string businessKey, string topicName)
    {
        var message = InterpretTopicReject(topicName);
        return await SendMessageToProcessAsync(businessKey, message);
    }

    private static string InterpretTopicAccept(string topicName)
    {
        switch (topicName)
        {
            case ServicesConstants.Topics.ManagerChecks:
                return ServicesConstants.Messages.ManagerApproved;
            case ServicesConstants.Topics.BouDirectorChecks:
                return ServicesConstants.Messages.BouDirectorApproved;
            case ServicesConstants.Topics.Verification:
                return ServicesConstants.Messages.VerificationDone;
            case ServicesConstants.Topics.DirectorChecks:
                return ServicesConstants.Messages.DirectorApproved;
        }

        throw new Exception($"Unknown topic '{topicName}'");
    }

    private static string InterpretTopicReject(string topicName)
    {
        switch (topicName)
        {
            case ServicesConstants.Topics.ManagerChecks:
                return ServicesConstants.Messages.ManagerRejected;
            case ServicesConstants.Topics.BouDirectorChecks:
                return ServicesConstants.Messages.BouDirectorRejected;
            case ServicesConstants.Topics.Verification:
                return ServicesConstants.Messages.VerificationDone;
            case ServicesConstants.Topics.DirectorChecks:
                return ServicesConstants.Messages.DirectorRejected;
        }

        throw new Exception($"Unknown topic '{topicName}'");
    }
}