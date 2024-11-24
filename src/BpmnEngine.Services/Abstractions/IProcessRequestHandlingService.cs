using BpmnEngine.Camunda.Client.Responses;
using BpmnEngine.Services.Processes.Models;

namespace BpmnEngine.Services.Abstractions;

public interface IProcessRequestHandlingService
{
    Task<ProcessStartResponse> StartProcessAsync(ProcessRequest request);
}