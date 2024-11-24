using BpmnEngine.Camunda.External;
using Newtonsoft.Json;

namespace BpmnEngine.Camunda.Client.Requests;

public class ProcessStartRequest
{
    public ProcessStartRequest(string businessKey, IDictionary<string, Variable>? variables = null)
    {
        BusinessKey = businessKey;
        Variables = variables;
    }

    [JsonProperty("businessKey")] public string BusinessKey { get; set; }
    [JsonProperty("variables")] public IDictionary<string, Variable>? Variables { get; set; }
}