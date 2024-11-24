namespace BpmnEngine.Camunda.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class HandlerVariablesAttribute : Attribute
{
    public HandlerVariablesAttribute(params string[] variables)
    {
        Variables = variables;
    }

    public IReadOnlyList<string> Variables { get; }
    public bool LocalVariables { get; set; }

    /// <summary>
    ///     Retrieves all the process variables from Camunda.
    /// </summary>
    public bool AllVariables { get; set; }
}