namespace BpmnEngine.Application.Models;

public class ProcessInfoViewModel : BaseViewModel
{
    public string BusinessKey { get; set; }
    public Guid ProcessInstanceId { get; set; }
}