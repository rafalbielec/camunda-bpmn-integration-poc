using BpmnEngine.Services.Processes.Models;

namespace BpmnEngine.Services.Abstractions;

public interface IBusinessKeyGenerator
{
    string GenerateBusinessKey(ProcessRequest request);
}