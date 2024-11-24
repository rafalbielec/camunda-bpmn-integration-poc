namespace BpmnEngine.Application.Models;

public class DecisionViewModel : BaseViewModel
{
    public DecisionViewModel()
    {
        Variables = new Dictionary<string, string>();
    }

    public Guid Id { get; set; }
    public string TopicName { get; set; }
    public Guid ProcessInstanceId { get; set; }
    public string BusinessKey { get; set; }
    public bool Accepted { get; set; }
    public IDictionary<string, string> Variables { get; set; }
}