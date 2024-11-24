using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Client.Responses;
using BpmnEngine.Camunda.Extensions;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Processes.Models;
using BpmnEngine.Storage;
using BpmnEngine.Storage.Abstractions;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Processes;

public class ProcessRequestHandlingService : IProcessRequestHandlingService
{
    private readonly IBusinessKeyGenerator _businessKeyGenerator;
    private readonly IProcessClient _client;
    private readonly IFormsRepository _repository;
    private readonly ILogger<ProcessRequestHandlingService> _logger;

    public ProcessRequestHandlingService(
        IProcessClient client,
        IFormsRepository repository,
        IBusinessKeyGenerator businessKeyGenerator,
        ILogger<ProcessRequestHandlingService> logger)
    {
        _client = client;
        _repository = repository;
        _businessKeyGenerator = businessKeyGenerator;
        _logger = logger;
    }

    public async Task<ProcessStartResponse> StartProcessAsync(ProcessRequest request)
    {
        string processKey;

        switch (request.ProcessName)
        {
            default:
                processKey = ServicesConstants.ProcessBpmnDiagrams.Test;
                break;
            case StorageConstants.ProcessName.CarHire:
            case StorageConstants.ProcessName.RoomBooking:
                processKey = ServicesConstants.ProcessBpmnDiagrams.FormHandling;
                break;
        }

        var businessKey = _businessKeyGenerator.GenerateBusinessKey(request);

        var result = await _client.StartProcessAsync(processKey, businessKey, request.ProcessVariables);

        var json = SerializerInstance.SerializeToString(request.ProcessVariables);
        await _repository.InsertExecutedProcessAsync(result.Id, 
            result.DefinitionId, result.BusinessKey,
            request.ProcessName, 
            json);

        _logger.LogInformation(
            $"Process Id: {result.Id} Business Key: {result.BusinessKey} Definition: {result.DefinitionId}");

        return result;
    }
}