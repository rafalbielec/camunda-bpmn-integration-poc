using Newtonsoft.Json;

namespace BpmnEngine.Camunda.Client.Responses;

public class ProcessStartResponse
{
    [JsonConstructor]
    public ProcessStartResponse(Guid id, string definitionId, string businessKey)
    {
        Id = id;
        DefinitionId = Guard.NotEmptyAndNotNull(definitionId, nameof(definitionId));
        BusinessKey = Guard.NotEmptyAndNotNull(businessKey, nameof(businessKey));
    }

    [JsonProperty("id")] public Guid Id { get; }
    [JsonProperty("definitionId")] public string DefinitionId { get; }
    [JsonProperty("businessKey")] public string BusinessKey { get; set; }
    [JsonProperty("ended")] public bool Ended { get; set; }
    [JsonProperty("suspended")] public bool Suspended { get; set; }
}
