using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace BpmnEngine.Camunda.Extensions;

public sealed class SerializerInstance
{
    private static readonly JsonSerializerSettings SerializerSettings = MakeSerializerSettings();
    public static JsonSerializer Serializer { get; } = JsonSerializer.Create(SerializerSettings);

    public static string SerializeToString<T>(T model)
    {
        using var writer = new StringWriter();
        using var jsonWriter = new JsonTextWriter(writer);

        Serializer.Serialize(jsonWriter, model);
        return writer.ToString();
    }

    public static T DeserializeFromString<T>(string json)
    {
        using var sr = new StringReader(json);
        using var jsonTextReader = new JsonTextReader(sr);

        return Serializer.Deserialize<T>(jsonTextReader);
    }

    private static JsonSerializerSettings MakeSerializerSettings()
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = false,
                    OverrideSpecifiedNames = true
                }
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        settings.Converters.Add(new StringEnumConverter());

        return settings;
    }
}