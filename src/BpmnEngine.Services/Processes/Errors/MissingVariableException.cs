namespace BpmnEngine.Services.Processes.Errors;

[Serializable]
public class MissingVariableException : Exception
{
    public MissingVariableException(string name) : base($"Variable {name} is missing")
    {
    }
}