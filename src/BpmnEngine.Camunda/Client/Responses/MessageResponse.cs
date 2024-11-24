using Newtonsoft.Json;

namespace BpmnEngine.Camunda.Client.Responses;

public class MessageResponse
{
    [JsonConstructor]
    public MessageResponse(string resultType)
    {
        ResultType = Guard.NotEmptyAndNotNull(resultType, nameof(resultType));
    }

    [JsonProperty("resultType")] public string ResultType { get; set; }
}