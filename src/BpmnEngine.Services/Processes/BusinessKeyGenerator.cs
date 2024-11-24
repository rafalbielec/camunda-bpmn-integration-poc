using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Processes.Models;

namespace BpmnEngine.Services.Processes;

public class BusinessKeyGenerator : IBusinessKeyGenerator
{
    /// <summary>
    ///     Generates the default Camunda businessKey value to use while running BPMN process diagram.
    /// </summary>
    /// <param name="request">Process details to run</param>
    /// <returns>Camunda businessKey</returns>
    public string GenerateBusinessKey(ProcessRequest request)
    {
        var key = $"{Guid.NewGuid():N}_{request.ProcessName}_{DateTime.UtcNow:yyyy_MM_dd_HH_mm_ss}";

        return key;
    }
}