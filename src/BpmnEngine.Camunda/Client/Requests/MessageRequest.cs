using Newtonsoft.Json;

namespace BpmnEngine.Camunda.Client.Requests;

public class MessageRequest
{
    [JsonConstructor]
    public MessageRequest(string businessKey, string name)
    {
        Name = name;
        BusinessKey = businessKey;
        ResultEnabled = true;
    }

    [JsonProperty("businessKey")] public string BusinessKey { get; set; }
    [JsonProperty("messageName")] public string Name { get; set; }
    [JsonProperty("resultEnabled")] public bool ResultEnabled { get; set; }
}