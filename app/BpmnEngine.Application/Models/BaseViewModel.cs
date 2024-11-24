namespace BpmnEngine.Application.Models;

public class BaseViewModel
{
    public BaseViewModel()
    {
        PageTitle = nameof(BpmnEngine);
    }

    public string PageTitle { get; set; }
}