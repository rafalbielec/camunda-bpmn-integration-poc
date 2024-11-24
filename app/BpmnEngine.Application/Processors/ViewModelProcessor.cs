using System.Security.Principal;
using BpmnEngine.Application.Models;
using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Processes.Models;
using BpmnEngine.Storage;
using BpmnEngine.Storage.Abstractions;

namespace BpmnEngine.Application.Processors;

public class ViewModelProcessor : IViewModelProcessor
{
    private readonly IUsersRepository _repository;
    private readonly IProcessRequestHandlingService _service;
    private readonly IMessageClient _client;
    
    public ViewModelProcessor(
        IUsersRepository repository,
        IProcessRequestHandlingService service, 
        IMessageClient client)
    {
        _repository = repository;
        _service = service;
        _client = client;
    }

    /// <summary>
    ///     Converts HTML form values to process variables.
    /// </summary>
    private static void AddViewModelVariable<T>(T model, IDictionary<string, Variable> variables) where T : BaseViewModel
    {
        switch (model)
        {
            case CarHireViewModel m:
                variables[ServicesConstants.FormHandlingVariables.Destination] = Variable.String(m.Destination ?? string.Empty);
                variables[ServicesConstants.FormHandlingVariables.PhoneNumber] = Variable.String(m.PhoneNumber ?? string.Empty);
                break;
            case RoomBookingViewModel r:
                variables[ServicesConstants.FormHandlingVariables.NumberOfPeople] = Variable.Integer(r.NumberOfPeople);
                variables[ServicesConstants.FormHandlingVariables.Room] = Variable.String(r.Room ?? string.Empty);
                break;
        }
    }

    /// <summary>
    ///     Converts user identity profile to process variables.
    /// </summary>
    private async Task AddUserVariablesAsync(IIdentity userIdentity, IDictionary<string, Variable> variables)
    {
        var user = await _repository.SelectByUserName(userIdentity.Name!);

        if (user != null)
        {
            variables[ServicesConstants.FormHandlingVariables.Position] = Variable.String(user.JobPosition);
            variables[ServicesConstants.FormHandlingVariables.UserName] = Variable.String(user.NormalizedUserName);
        }
        else
            throw new Exception($"Cannot find {userIdentity.Name} in the list of users");
    }

    /// <summary>
    ///     Starts the process in the BPMN engine.
    /// </summary>
    public async Task<ProcessInfoViewModel> ProcessViewModelAsync<T>(T model, IIdentity userIdentity) where T : BaseViewModel
    {
        var variables = new Dictionary<string, Variable>
        {
            [ServicesConstants.FormHandlingVariables.LastStep] = Variable.String(ServicesConstants.FormHandlingVariables.Start)
        };

        await AddUserVariablesAsync(userIdentity, variables);

        AddViewModelVariable(model, variables);

        var processName = MapViewModelToProcessName(model);
        var request = new ProcessRequest(processName, variables);

        var response = await _service.StartProcessAsync(request);

        var processInfo = new ProcessInfoViewModel
        {
            ProcessInstanceId = response.Id,
            BusinessKey = response.BusinessKey
        };

        return processInfo;
    }

    private static StorageConstants.ProcessName MapViewModelToProcessName<T>(T model) where T : BaseViewModel
    {
        var processName = model switch
        {
            CarHireViewModel => StorageConstants.ProcessName.CarHire,
            RoomBookingViewModel => StorageConstants.ProcessName.RoomBooking,
            _ => StorageConstants.ProcessName.Test
        };

        return processName;
    }

    public async Task<MessageInfoViewModel> ProcessMessagesViewModelAsync(MessagesViewModel model)
    {
        var response = await _client.SendMessageEventAsync(model.BusinessKey, model.MessageContent);
        if (response.Any())
        {
            var first = response.First();
            return new MessageInfoViewModel
            {
                BusinessKey = model.BusinessKey,
                MessageContent = model.MessageContent,
                ResultType = first.ResultType
            };
        }

        return new MessageInfoViewModel
        {
            BusinessKey = model.BusinessKey,
            MessageContent = PolishConstants.MissingBusinessKeyError,
            ResultType = string.Empty
        };
    }
}