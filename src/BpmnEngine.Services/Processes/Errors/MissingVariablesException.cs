namespace BpmnEngine.Services.Processes.Errors;

[Serializable]
public class MissingVariablesException : Exception
{
    public MissingVariablesException() : base("No process variables found")
    {
    }
}