namespace BpmnEngine.Application.Models;

public class FormsViewModel : BaseViewModel
{
    public FormsViewModel()
    {
        Forms = new List<FormLinkViewModel>();
    }

    public List<FormLinkViewModel> Forms { get; }
}