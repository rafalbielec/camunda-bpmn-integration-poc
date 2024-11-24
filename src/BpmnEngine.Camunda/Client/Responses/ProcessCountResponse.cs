using Newtonsoft.Json;

namespace BpmnEngine.Camunda.Client.Responses;

public class ProcessCountResponse
{
    [JsonProperty("count")]
    public long Count { get; set; }
}