namespace BpmnEngine.Application.Models;

public class MessageInfoViewModel : BaseViewModel
{
    public string BusinessKey { get; set; }
    public string MessageContent { get; set; }
    public string ResultType { get; set; }
}